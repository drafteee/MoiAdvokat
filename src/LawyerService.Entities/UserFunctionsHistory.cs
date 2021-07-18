using System;
using System.Collections.Generic;
using System.Text;

namespace LawyerService.Entities
{
    //Аудит действий пользователя
    public class UserFunctionsHistory : BaseEntity
    {
        //FK на User
        public long UserId { get; set; }

        //FK на Functions
        public long FunctionId { get; set; }

        //Время, когда было произведено действие пользователем
        public DateTimeOffset ActionDate { get; set; }

        //Параметры, которые были при вызове функции
        public string ParamsBefore { get; set; }

        //Успешно ли прошла функция(без ошибок)
        public bool IsSuccess { get; set; }
    }
}
