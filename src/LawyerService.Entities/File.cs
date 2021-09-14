using System;

namespace LawyerService.Entities
{
    /// <summary>
    /// Объект файла
    /// </summary>
    public class File : BaseEntity
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Расширения файла
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// Содержимое файла
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// Размер файла
        /// </summary>
        public long FileLength { get; set; }

        /// <summary>
        /// Дата загрузки файла
        /// </summary>
        public DateTimeOffset DateLoad { get; set; }
    }
}
