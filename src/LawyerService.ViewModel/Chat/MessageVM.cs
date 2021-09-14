using LawyerService.ViewModel.Account;
using LawyerService.ViewModel.Address;
using LawyerService.ViewModel.Base;
using System;

namespace LawyerService.ViewModel.Chat
{
    public class MessageVM : BaseVM
    {
        public string UserFromName { get; set; }

        public string UserFromId { get; set; }

        public UserVM UserFrom { get; set; }

        public string UserToName { get; set; }

        public string UserToId { get; set; }

        public UserVM UserTo { get; set; }

        public string MessageContent { get; set; }

        public DateTime CreatedOn { get; set; }
        public bool Read { get; set; }

        public long? FileId { get; set; }
        public long? OrderId { get; set; }
    }
}
