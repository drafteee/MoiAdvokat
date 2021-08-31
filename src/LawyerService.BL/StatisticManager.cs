using AutoMapper;
using LawyerService.BL.Interfaces;
using LawyerService.BL.Interfaces.Account;
using LawyerService.DataAccess;
using LawyerService.DataAccess.Interfaces;
using LawyerService.Entities.Identity;
using LawyerService.Entities.Lawyer;
using LawyerService.Entities.Order;
using LawyerService.ViewModel.Statistic;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerService.BL
{
    public class StatisticManager : IStatisticManager
    {
        private IUow _uow;
        private UserManager<User> _userManager;
        public StatisticManager(IUow uow, IMapper mapper, ILocalizationManager localizationManager, IUserAccessor userAccessor, UserManager<User> userManager, IServiceProvider serviceProvider)
        {
            _uow = uow;
            _userManager = userManager;
        }
        public async Task<HomeStatisticVM> GetHomeStatistic()
        {
            var countClients = _userManager.Users.Count();
            var countLawyers = _uow.Set<Lawyer>().Count();

            var orderSpecializations = _uow.Set<OrderSpecialization>().GroupBy(x => x.Specialization.Name).Select(g => new StatisticSpecialization { Name = g.Key, Count = g.Count() });
            var orders = _uow.Set<Order>().GroupBy(x => x.CreatedOn.Date).Select(g => new StatisticOrder { Date = g.Key, Count = g.Count() });

            return new HomeStatisticVM
            {
                CountClients = countClients,
                CountLawyers = countLawyers,
                ListOrders = orders.ToList(),
                ListSpecialization = orderSpecializations.ToList()
            };
        }
    }
}
