using AutoMapper;
using System;
using Xunit;

namespace LawyerService.Test
{
    public class AutomapperMappingTests
    {
        private readonly IMapper _mapper;

        public AutomapperMappingTests()
        {
            _mapper = new MapperConfiguration(cfg => { 
                cfg.AddMaps(typeof(LawyerService.Bootstrapper.Bootstrapper).Assembly); 
            }).CreateMapper();
        }

        [Fact]
        public void TestMappingProfiles() =>_mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}
