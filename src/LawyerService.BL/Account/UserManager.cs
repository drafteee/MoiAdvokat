using AutoMapper;
using FluentValidation;
using LawyerService.BL.Helpers;
using LawyerService.BL.Interfaces;
using LawyerService.BL.Interfaces.Account;
using LawyerService.DataAccess;
using LawyerService.Entities.Identity;
using LawyerService.Entities.Transactions;
using LawyerService.Resources;
using LawyerService.ViewModel.Account;
using LawyerService.ViewModel.Common;
using LawyerService.ViewModel.Lawyers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerService.BL.Account
{
    public class UserManager : IUserManager
    {
        private readonly LawyerDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<LawyerVM> _validator;
        private readonly ILocalizationManager _localisationManager; 
        private readonly IUserAccessor _userAccessor; 
        private readonly UserManager<User> _userManager;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly JwtGenerator _jwtGenerator;
        private readonly RoleManager<Role> _roleManager;

        public UserManager(LawyerDbContext context, IMapper mapper, IValidator<LawyerVM> validator, ILocalizationManager localizationManager, IUserAccessor userAccessor, UserManager<User> userManager, PasswordHasher<User> passwordHasher, JwtGenerator jwtGenerator, RoleManager<Role> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _localisationManager = localizationManager;
            _userAccessor = userAccessor;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
            _roleManager = roleManager;
        }

        public async Task<RequestResult> AssignRoleToUserAsync(string userName, string role)
        {
            var request = new RequestResult(false, string.Empty);
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                    throw new Exception("Нет пользователя с данным именем");
                var result = await _userManager.AddToRoleAsync(user, role);
                request.Success = result.Succeeded;
                request.Message = string.Join(", ", result.Errors?.Select(e => e.Description));
            }
            catch (Exception)
            {
                request.Message = string.Format(_localisationManager.GetString(LocalisationSections.User, "AssignRoleToUserNotFound"), userName);
            }
            return request;
        }

        public async Task<RequestResult> CreateRoleAsync(string role)
        {
            var result = await _roleManager.CreateAsync(new Role { Name = role });
            return new RequestResult(result.Succeeded, string.Join(", ", result.Errors?.Select(e => e.Description)));
        }

        public async Task<RequestResult> CreateUserAsync(UserVM userVM, string password)
        {
            var result = new RequestResult(false, string.Empty);
            try
            {
                User user = _mapper.Map<User>(userVM);
                user.PasswordHash = _passwordHasher.HashPassword(user, password);
                
                var res = await _userManager.CreateAsync(user);

                var userBalance = new UserBalance()
                {
                    Amount = 0,
                    User = user,
                    ProcentIn = 0,
                    ProcentOut = 0
                };
                _context.UserBalances.Add(userBalance);
                await _context.SaveChangesAsync();
                result.Output = res;
                result.Success = true;
            }
            catch(Exception e)
            {
                result.Message = _localisationManager.GetString(LocalisationSections.User, "UserCreateException") +":"+ e.Message;
            }
            return result;
        }

        public async Task<RequestResult> LoginAsync(string userName, string password, string ip, string userAgent)
        {
            var result = new RequestResult(false, string.Empty);
            try
            {
                User user = await _context.Users.Where(x => x.UserName.Equals(userName))
                    .Include(x=>x.Functions)
                    .FirstOrDefaultAsync();

                if (user.LockoutEnd != null)
                {
                    if (user.LockoutEnd.Value.Date == DateTime.MaxValue.Date)
                    {
                        result.Message = _localisationManager.GetString(LocalisationSections.User, "BlockedForExceedLoginAttempts") ;
                        return result;
                    }
                    else if (user.LockoutEnd > DateTime.Now)
                    {
                        result.Message = string.Format(_localisationManager.GetString(LocalisationSections.User, "BlockedForExceedLoginAttempts"), 15);
                        return result;
                    }
                    else
                    {
                        user.AccessFailedCount = 0;
                        user.LockoutEnd = null;
                    }
                }

                if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Failed)
                {
                    if (user.UserName != "admin")
                    {
                        user.AccessFailedCount += 1;
                        var limit = 5;

                        if (user.AccessFailedCount == limit)
                        {
                            var time = 15;
                            user.LockoutEnd = DateTime.Now.AddMinutes(time);
                        }
                        await _context.SaveChangesAsync();
                    }
                    result.Message = _localisationManager.GetString(LocalisationSections.User, "InvalidLoginOrPassword");
                    return result;
                }
                else
                {
                    if (user.AccessFailedCount != 0) user.AccessFailedCount = 0;
                }

                var roles = await _userManager.GetRolesAsync(user);
                var refreshToken = _jwtGenerator.GenerateRefreshToken();
                var token = new RefreshToken
                {
                    User = user,
                    Value = refreshToken,
                    IP = ip,
                    UserAgent = userAgent,
                    Date = DateTime.Now
                };
                _context.RefreshTokens.Add(token);
                await _context.SaveChangesAsync();

                UserVM userVM = _mapper.Map<UserVM>(user);
                userVM.Token = _jwtGenerator.CreateTokenWithRoles(user, roles.ToList());
                userVM.Roles = roles;

                result.Output = userVM;
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Message = _localisationManager.GetString(LocalisationSections.Common, "CommonException") + ":" + e.Message;
            }
            return result;
        }

        public async Task<RequestResult> RefreshUserDataAsync()
        {
            var result = new RequestResult(false, string.Empty);
            try
            {
                string userName = _userAccessor.GetCurrentUsername();

                if (userName == null)
                    return null;

                User user = await _context.Users.Where(u => u.UserName == userName)
                     .Include(u => u.Functions)
                    .FirstOrDefaultAsync();

                var roles = await _userManager.GetRolesAsync(user);
                UserVM userVM = _mapper.Map<UserVM>(user);
                result.Output = userVM;
                result.Success = true;
                return result;
            }
            catch (Exception e)
            {
                return new RequestResult(false, e.Message);
            }
        }

        public async Task<RequestResult> LogoutAsync(long SessionId)
        {
            try
            {
                var userId = _context.Users.Where(u => u.UserName == _userAccessor.GetCurrentUsername()).Select(u => u.Id).FirstOrDefault();
                var refreshToken = _context.RefreshTokens.Where(rt => rt.UserId == userId && rt.Id == SessionId).FirstOrDefault();
                _context.RefreshTokens.Remove(refreshToken);
                await _context.SaveChangesAsync();
                return new RequestResult();
            }
            catch (Exception e)
            {
                //Даем возможность пользователю выйти из кабинета, не критично, если не удалось удалить RefreshToken;
                return new RequestResult(true, e.Message);
            }
        }

        public async Task<RequestResult> RefreshAsync(string token, string ip, string userAgent, long sessionId)
        {
            var result = new RequestResult(false, string.Empty);
            try
            {
                try
                {
                    _jwtGenerator.ValidateToken(token);
                    return null;
                }
                catch (Exception e)
                {
                    if (e.Message == "Token expired")
                    {
                        var userName = _jwtGenerator.GetUserameFromExpiredToken(token);

                        if (userName == null)
                            return new RequestResult(false, "Invalid RefreshToken") { 
                            ErrorCode= (int)System.Net.HttpStatusCode.Forbidden,
                            };

                        var user = await _userManager.FindByNameAsync(userName);
                        var userRefreshToken = _context.RefreshTokens.Where(rt => rt.User.Id == user.Id && rt.Id == sessionId).FirstOrDefault();

                        //if (!user.PhoneNumberConfirmed)
                        //    throw new RestException(System.Net.HttpStatusCode.Forbidden, _localizer["PhoneNotConfirmed"].Value);

                        if (userRefreshToken != null)
                        {
                            var newRefreshToken = _jwtGenerator.GenerateRefreshToken();
                            userRefreshToken.Value = newRefreshToken;
                            _context.RefreshTokens.Update(userRefreshToken);
                            await _context.SaveChangesAsync();

                            var roles = await _userManager.GetRolesAsync(user);
                            UserVM userVM = new UserVM
                            {
                                Token = _jwtGenerator.CreateTokenWithRoles(user, roles.ToList()),
                                RefreshToken = newRefreshToken,
                                UserName = user.UserName,
                                SessionId = userRefreshToken.Id
                            };

                            result.Success = true;
                            result.Output = userVM;
                            return result;
                        }
                        else
                        {
                            return new RequestResult(false, "Invalid RefreshToken")
                            {
                                ErrorCode = (int)System.Net.HttpStatusCode.Forbidden,
                            };
                        }
                    }
                    return new RequestResult(false, e.Message);
                }
            }
            catch (Exception e)
            {
                return new RequestResult(false, e.Message);
            }

        }

        public async Task<RequestResult> GetActiveSessionsAsync(string UserId, long sessionId)
        {
            var result = new RequestResult(false, string.Empty);
            try
            {
                User user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());
                var userRoles = await _userManager.GetRolesAsync(user);

                var sessions = _context.RefreshTokens.Where(rt => rt.UserId.Equals(UserId)).OrderByDescending(e => e.Date).ToListAsync();

                var res = _mapper.Map<List<ActiveSessionVM>>(sessions);
                res.Where(x => x.Id == sessionId).FirstOrDefault().IsCurrent = true;
                result.Success = true;
                result.Output = res;
                return result;
            }
            catch (Exception e)
            {
                return new RequestResult(false, e.Message);
            }
        }

        public async Task<RequestResult> RemoveSessionAsync(long sessionId)
        {
            try
            {
                User user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());
                var userRoles = await _userManager.GetRolesAsync(user);

                var refreshToken = _context.RefreshTokens.Where(rt => rt.Id == sessionId).FirstOrDefault();
                if (refreshToken == null)
                    return new RequestResult();
           
                _context.RefreshTokens.Remove(refreshToken);
                return new RequestResult( await _context.SaveChangesAsync() > 0, "Result");
            }
            catch (Exception e)
            {
                return new RequestResult(false, e.Message);
            }
        }

        public async Task<RequestResult> GetUserFunctionsAsync(string id)
        {
            var  functions = await _context.Users.Where(x => x.Id.Equals(id)).Include(x=>x.Functions).Select(y=>y.Functions.Select(y=>y.Name)).FirstOrDefaultAsync();
            return new RequestResult()
            {
                Success = true,
                Output = functions
            };
        }

        public async Task<RequestResult> GetUserRolesAsync(string id)
        {
            var user = await _context.Users.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            var userRoles = await _userManager.GetRolesAsync(user);
            return new RequestResult()
            {
                Success = true,
                Output = userRoles
            };
        }

        #region Private Methods


        #endregion
    }
}
