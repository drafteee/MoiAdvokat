using LawyerService.BL.Interfaces.Lawyers;
using LawyerService.Entities.Lawyer;
using LawyerService.ViewModel.Files;
using LawyerService.ViewModel.Lawyers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerService.API.Controllers.Lawyers
{
    public class LawyerController : BaseController<ILawyerManager, Lawyer, LawyerVM>
    {
        public LawyerController(ILawyerManager manager) : base(manager)
        {
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<bool> UploadCertificate([FromBody] AttachFileVM vm)
        {
            return await _manager.UploadCertificate(vm);
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<bool> CheckIfCertificateExists([FromHeader] LawyerVM vm)
        {
            return await _manager.CheckIfCertificateExists(vm);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<bool> ConfirmLawyer([FromBody] LawyerConfirmationVM vm)
        {
            return await _manager.ConfirmLawyer(vm);
        }
    }
}
