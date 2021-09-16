using LawyerService.BL.Interfaces.Files;
using LawyerService.DataAccess.Interfaces;
using LawyerService.ViewModel.Errors;
using LawyerService.ViewModel.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LawyerService.BL.Files
{
    public class FileManager : ControllerBase, IFileManager
    {
        private readonly IUow _uow;

        public FileManager(IUow uow)
        {
            _uow = uow;
        }

        public async Task<FileStreamResult> DownloadFile(long id)
        {
            var file = await _uow.Set<Entities.File>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (file == null)
            {
                throw new RestException(System.Net.HttpStatusCode.NotFound, "Файл не найден.");
            }

            MemoryStream ms = new MemoryStream();
            ms.Write(file.Content, 0, (int)file.FileLength);
            ms.Position = 0;

            new FileExtensionContentTypeProvider().TryGetContentType(file.FileName + file.FileExtension, out string contentType);

            return File(ms, contentType, file.FileName + file.FileExtension);
        }

        public async Task<List<long>> UploadFiles(UploadFileVM vm)
        {
            try
            {
                List<Entities.File> savedFiles = new();
                foreach (var file in vm.Files)
                {
                    using StreamContent sc = new(file.OpenReadStream());

                    savedFiles.Add(new Entities.File
                    {
                        FileName = Path.GetFileNameWithoutExtension(file.FileName),
                        FileExtension = Path.GetExtension(file.FileName),
                        Content = await sc.ReadAsByteArrayAsync(),
                        FileLength = file.Length,
                        DateLoad = DateTime.Now
                    });
                }

                _uow.Set<Entities.File>().AddRange(savedFiles);

                if (await _uow.SaveAsync() > 0)
                    return savedFiles.Select(x => x.Id).ToList();

                throw new RestException(System.Net.HttpStatusCode.InternalServerError);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
