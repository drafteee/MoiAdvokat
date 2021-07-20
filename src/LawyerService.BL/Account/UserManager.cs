﻿using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LawyerService.BL.Interfaces;
using LawyerService.DataAccess.Interfaces;
using LawyerService.Entities;
using LawyerService.ViewModel;
using Microsoft.AspNetCore.Identity;
using LawyerService.Entities.Identity;
using LawyerService.BL.Interfaces.Account;
using LawyerService.ViewModel.Common;
using LawyerService.ViewModel.Account;
using System;
using LawyerService.Resources;
using LawyerService.DataAccess;
using LawyerService.BL.Helpers;

namespace LawyerService.BL.Account
{
    public class UserManager : BaseManager, IUserManager
    {
        private readonly LawyerDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<LawyerVM> _validator;
        private readonly ILocalisationManager _localisationManager; 
        private readonly IUserAccessor _userAccessor; 
        private readonly UserManager<User> _userManager;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly JwtGenerator _jwtGenerator;

        public UserManager(LawyerDbContext context, IMapper mapper, IValidator<LawyerVM> validator, ILocalisationManager localisationManager, IUserAccessor userAccessor, UserManager<User> userManager, PasswordHasher<User> passwordHasher, JwtGenerator jwtGenerator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _localisationManager = localisationManager;
            _userAccessor = userAccessor;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<RequestResult> CreateUserAsync(UserVM userVM, string password)
        {
            var result = new RequestResult(false, string.Empty);
            try
            {
                User user = _mapper.Map<User>(userVM);
                user.PasswordHash = _passwordHasher.HashPassword(user, password);
                
                var res = await _userManager.CreateAsync(user);

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
                User user = await _context.Users.Where(x => x.UserName.Equals(userName)).FirstOrDefaultAsync();

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
        #region Private Methods


        #endregion
    }
}