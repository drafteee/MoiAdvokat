using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LawyerService.BL.Interfaces;
using LawyerService.ViewModel;

namespace LawyerService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LawyerController : ControllerBase
    {
        private readonly ILawyerManager _lawyerManager;

        public LawyerController(ILawyerManager lawyerManager)
        {
            this._lawyerManager = lawyerManager;
        }

        [HttpGet]
        public Task<ICollection<LawyerVM>> GetAll()
        {
            return _lawyerManager.GetAllAsync();
        }

        [HttpGet]
        public Task<LawyerVM> GetByID([FromQuery] int lawyerId)
        {
            return _lawyerManager.GetByIDAsync(lawyerId);
        }

    }
}
