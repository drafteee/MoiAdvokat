using LawyerService.BL.Interfaces.Files;
using LawyerService.DataAccess.Interfaces;
using LawyerService.ViewModel.Errors;
using LawyerService.ViewModel.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LawyerService.BL.Files
{
    public class FileManager : IFileManager
    {
        private readonly IUow _uow;

        public FileManager(IUow uow)
        {
            _uow = uow;
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
