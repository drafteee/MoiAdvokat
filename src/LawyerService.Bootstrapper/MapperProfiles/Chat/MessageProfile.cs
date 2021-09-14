using AutoMapper;
using LawyerService.Entities.Chat;
using LawyerService.ViewModel.Chat;

namespace LawyerService.Bootstrapper.MapperProfiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageVM>()
                .ReverseMap();
        }
    }
}
