using AutoMapper;
using LawyerService.Entities.Identity;
using LawyerService.ViewModel;
using LawyerService.ViewModel.Account;
using System.Linq;

namespace LawyerService.Bootstrapper.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserVM>()
                .ForMember(x => x.Functions, y => y.MapFrom(z => z.Functions != null ? z.Functions.Select(x => x.Name):null))
                .ReverseMap();
            CreateMap<RefreshToken, ActiveSessionVM>();
            CreateMap<User, RegisteredUserDataVM>().ReverseMap();

        }
    }
}
