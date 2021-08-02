using LawyerService.Interfaces.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LawyerService.Entities
{
    public abstract class BaseEntity : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Дата и время создания записи
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Удалена ли запись
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Дата и время удаления записи
        /// </summary>
        public DateTime? DeletedOn { get; set; }
    }
}
