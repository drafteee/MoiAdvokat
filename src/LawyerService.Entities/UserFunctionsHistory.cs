using LawyerService.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities
{
    /// <summary>
    /// Аудит действий пользователя
    /// </summary>
    public class UserFunctionsHistory : BaseEntity
    {
        /// <summary>
        /// FK на User
        /// </summary>
        public long UserId { get; set; }
        public User User { get; set; }

        /// <summary>
        /// FK на Functions
        /// </summary>
        public long FunctionId { get; set; }
        //public Function Function { get; set; }

        /// <summary>
        /// Время, когда было произведено действие пользователем
        /// </summary>
        public DateTimeOffset ActionDate { get; set; }

        /// <summary>
        /// Параметры, которые были при вызове функции
        /// </summary>
        public string ParamsBefore { get; set; }

        /// <summary>
        /// Успешно ли прошла функция(без ошибок)
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}
