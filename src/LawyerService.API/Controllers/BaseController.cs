using LawyerService.BL.Interfaces;
using LawyerService.Entities;
using LawyerService.ViewModel.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public abstract class BaseController<Manager, T, TVM> : ControllerBase
        where Manager : IBaseManager<T, TVM>
        where T : BaseEntity
        where TVM : BaseVM
    {
        protected readonly Manager _manager;

        protected BaseController(Manager manager)
        {
            _manager = manager;
        }

        [AllowAnonymous]
        [HttpGet]
        public Task<List<TVM>> GetAllCurrent()
        {
            return _manager.GetAllAsync();
        }

        [AllowAnonymous]
        [HttpGet]
        public Task<List<TVM>> GetAll()
        {
            return _manager.GetAllAsync(true);
        }

        [AllowAnonymous]
        [HttpGet]
        public Task<TVM> GetByIdCurrent([FromQuery] long id)
        {
            return _manager.GetByIdAsync(id);
        }

        [AllowAnonymous]
        [HttpGet]
        public Task<TVM> GetById([FromQuery] long id)
        {
            return _manager.GetByIdAsync(id, true);
        }

        [AllowAnonymous]
        [HttpPost]
        public Task<bool> CreateOrUpdate([FromBody] TVM body)
        {
            return _manager.CreateOrUpdateAsync(body);
        }

        [AllowAnonymous]
        [HttpPost]
        public Task<bool> CreateOrUpdateMany([FromBody] List<TVM> body)
        {
            return _manager.CreateOrUpdateManyAsync(body);
        }

        [AllowAnonymous]
        [HttpPost]
        public virtual Task<bool> Delete([FromBody] TVM body)
        {
            return _manager.DeleteByIdAsync(body.Id);
        }

        [AllowAnonymous]
        [HttpPost]
        public Task<bool> Restore([FromBody] TVM body)
        {
            return _manager.RestoreByIdAsync(body.Id);
        }
    }
}
