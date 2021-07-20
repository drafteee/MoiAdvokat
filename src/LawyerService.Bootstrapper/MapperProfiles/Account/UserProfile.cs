using AutoMapper;
using LawyerService.Entities.Identity;
using LawyerService.ViewModel;
using LawyerService.ViewModel.Account;

namespace LawyerService.Bootstrapper.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserVM>().ReverseMap();
        }
    }
}
