using AutoMapper;
using FluentValidation;
using LawyerService.BL.Interfaces;
using LawyerService.BL.Interfaces.Account;
using LawyerService.BL.Interfaces.Addresses;
using LawyerService.DataAccess;
using LawyerService.DataAccess.Interfaces;
using LawyerService.Entities.Chat;
using LawyerService.Entities.Identity;
using LawyerService.Entities.Lawyer;
using LawyerService.ViewModel.Chat;
using LawyerService.ViewModel.Common;
using LawyerService.ViewModel.Lawyers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerService.BL
{
    public class ChatManager : IChatManager
    {
        private readonly LawyerDbContext _context;
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public ChatManager(LawyerDbContext context, IUow uow, IMapper mapper, IUserAccessor userAccessor)
        {
            _context = context;
            _uow = uow;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<IEnumerable<MessageVM>> GetMessagesByOrder(long orderId)
        {
          var messages=  await _uow.Messages.GetQueryable().Where(x => x.OrderId == orderId).OrderBy(x => x.CreatedOn).ToListAsync();
            return _mapper.Map<List<Message>,List<MessageVM>>(messages);
        }

        public async Task<RequestResult> SendMessage(MessageVM messageVM)
        {
            var result = new RequestResult(false, string.Empty);
            string userName = _userAccessor.GetCurrentUsername();
            User userfrom = await _context.Users.Where(u => u.UserName == userName)
                   .FirstOrDefaultAsync();
            User userTo = await _uow.Orders.GetQueryable()
                .Include(x => x.Lawyer)
                .ThenInclude(x => x.User)
                .Where(x => x.Id == messageVM.OrderId).Select(x=> userfrom.Id== x.UserId?x.Lawyer.User:x.User).FirstOrDefaultAsync();


            if (userfrom != null && userTo!=null)
            {
                var message = _mapper.Map<MessageVM, Message>(messageVM);
                message.UserFromId = userfrom.Id;
                message.UserFromName = userfrom.UserName;
                message.UserToId = userTo.Id;
                message.UserToName = userTo.UserName;
                message.CreatedOn = DateTime.Now;
                _uow.Messages.Add(message);
                await _uow.SaveAsync();
                result.Success = true;
                result.Output = message;
            }
            return result;
        }
    }
}
