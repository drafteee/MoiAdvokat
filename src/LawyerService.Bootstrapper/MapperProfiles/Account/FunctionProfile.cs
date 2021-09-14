using AutoMapper;
using LawyerService.Entities.Identity;
using LawyerService.ViewModel;
using LawyerService.ViewModel.Account;
using System.Linq;

namespace LawyerService.Bootstrapper.MapperProfiles
{
    public class FunctionProfile : Profile
    {
        public FunctionProfile()
        {
            CreateMap<Function, FunctionVM>().ReverseMap();
        }
    }
}
