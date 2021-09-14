using AutoMapper;
using LawyerService.Entities.Identity;
using LawyerService.ViewModel;
using LawyerService.ViewModel.Account;
using System.Linq;

namespace LawyerService.Bootstrapper.MapperProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleVM>()
                .ForMember(x => x.Functions, y => y.MapFrom(z => z.RoleFunctions != null ? z.RoleFunctions.Select(x => x.Function):null))
                .ReverseMap();
        }
    }
}
