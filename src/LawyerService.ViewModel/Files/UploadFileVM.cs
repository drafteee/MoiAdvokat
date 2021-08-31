using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace LawyerService.ViewModel.Files
{
    public class UploadFileVM
    {
        public List<IFormFile> Files { get; set; }
    }
}
