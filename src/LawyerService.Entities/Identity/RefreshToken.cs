using LawyerService.Interfaces.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LawyerService.Entities.Identity
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class RefreshToken : BaseEntity
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public Guid UserId { get; set; }
        public User User { get; set; }
        /// <summary>
        /// Значение токена
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTimeOffset Date { get; set; }
        /// <summary>
        /// IP-адрес
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// UserAgent
        /// </summary>
        public string UserAgent { get; set; }
    }
}
