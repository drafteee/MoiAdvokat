using LawyerService.Entities.Identity;
using System;

namespace LawyerService.Entities.Chat
{
    public class Message : BaseEntity
    {
        public string UserFromName { get; set; }

        public string UserFromId { get; set; }

        public User UserFrom { get; set; }

        public string UserToName { get; set; }

        public string UserToId { get; set; }

        public User UserTo { get; set; }

        public string MessageContent { get; set; }

        public bool Read { get; set; }

        public long? FileId { get; set; }
        public long? OrderId { get; set; }
    }
}
