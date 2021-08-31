using System.Collections.Generic;

namespace LawyerService.ViewModel.Files
{
    public class AttachFileVM
    {
        public long EntityId { get; set; }
        public List<long> FilesIds { get; set; }
    }
}
