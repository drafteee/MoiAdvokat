using LawyerService.BL.Interfaces;
using LawyerService.ViewModel.Statistic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticManager _statisticManager;

        public StatisticController(IStatisticManager statisticManager)
        {
            _statisticManager = statisticManager;
        }

        [HttpGet]
        public async Task<HomeStatisticVM> GetHomeStatistic()
        {
            return await _statisticManager.GetHomeStatistic();
        }
    }
}
