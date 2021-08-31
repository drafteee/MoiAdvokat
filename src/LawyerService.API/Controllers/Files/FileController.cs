using LawyerService.BL.Interfaces.Files;
using LawyerService.ViewModel.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerService.API.Controllers.Files
{
    [Route("api/[controller]/[action]")]
    public class FileController : ControllerBase
    {
        private const long MaxFileSize = 100L * 1024L * 1024L; // 100MB

        private readonly IFileManager _fileManager;

        public FileController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpPost]
        [DisableFormValueModelBinding]
        [DisableRequestSizeLimit]
        [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
        [AllowAnonymous]
        public async Task<List<long>> Post([FromForm] UploadFileVM vm)
        {
            return await _fileManager.UploadFiles(vm);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<bool> DeleteFiles([FromBody] )
        //{
        //}
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DisableFormValueModelBindingAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var factories = context.ValueProviderFactories;
            factories.RemoveType<FormValueProviderFactory>();
            factories.RemoveType<FormFileValueProviderFactory>();
            factories.RemoveType<JQueryFormValueProviderFactory>();
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }
}
