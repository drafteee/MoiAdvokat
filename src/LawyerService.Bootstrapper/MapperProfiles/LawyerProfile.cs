using AutoMapper;
using LawyerService.Entities;
using LawyerService.ViewModel;

namespace LawyerService.Bootstrapper.MapperProfiles
{
    public class LawyerProfile : Profile
    {
        public LawyerProfile()
        {
            CreateMap<Lawyer, LawyerVM>().ReverseMap();
        }
    }
}
