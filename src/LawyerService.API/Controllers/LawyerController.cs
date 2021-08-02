using LawyerService.BL.Interfaces;
using LawyerService.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LawyerController : ControllerBase
    {
        private readonly ILawyerManager _lawyerManager;

        public LawyerController(ILawyerManager lawyerManager)
        {
            _lawyerManager = lawyerManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public Task<List<LawyerVM>> GetAll()
        {
            return _lawyerManager.GetAllAsync();
        }

        [HttpGet]
        public Task<LawyerVM> GetByID([FromQuery] int lawyerId)
        {
            return _lawyerManager.GetByIdAsync(lawyerId);
        }

        [AllowAnonymous]
        [HttpPost]
        public Task<bool> CreateOrUpdate([FromBody] LawyerVM lawyer)
        {
            return _lawyerManager.CreateOrUpdateAsync(lawyer);
        }

        [AllowAnonymous]
        [HttpPost]
        public Task<bool> CreateOrUpdateMany([FromBody] List<LawyerVM> lawyers)
        {
            return _lawyerManager.CreateOrUpdateManyAsync(lawyers);
        }
    }
}
