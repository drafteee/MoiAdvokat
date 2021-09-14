using AutoMapper;
using LawyerService.Entities.Lawyer;
using LawyerService.ViewModel.Lawyers;

namespace LawyerService.Bootstrapper.MapperProfiles.Lawyers
{
    public class SpecializationProfile : Profile
    {
        public SpecializationProfile()
        {
            CreateMap<Specialization, SpecializationVM>().ReverseMap();
        }
    }
}
