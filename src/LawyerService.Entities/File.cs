using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities
{
    //Объект файла
    public class File : BaseEntity
    {
        //Имя файла
        public string FileName { get; set; }

        //Расширения файла
        public string FileExtension { get; set; }

        //содержимое файла
        public byte[] Content { get; set; }

        //Размер файла
        public long FileLength { get; set; }

        //Дата загрузки файла
        public DateTimeOffset DateLoad { get; set; }
    }
}
