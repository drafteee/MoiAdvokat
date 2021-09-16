using LawyerService.ViewModel.Files;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LawyerService.BL.Interfaces.Files
{
    public interface IFileManager
    {
        Task<List<long>> UploadFiles(UploadFileVM vm);
        Task<FileStreamResult> DownloadFile(long id);
    }
}
