using LawyerService.ViewModel.Statistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerService.BL.Interfaces
{
    public interface IStatisticManager
    {
        Task<HomeStatisticVM> GetHomeStatistic();
    }
}
