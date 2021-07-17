using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawyerService.BL.Interfaces;
using LawyerService.DataAccess.Interfaces;
using Xunit;
using LawyerService.Test.Extensions;
using LawyerService.Entities;
using MockQueryable.Moq;
using System.Threading;

namespace LawyerService.Test
{
    public class LawyerManagerTest : IClassFixture<BaseTestFixture>
    {

        #region Private fields

        private readonly BaseTestFixture _baseTestFixture;
        private readonly Mock<IUow> _uowMock;

        #endregion

        public LawyerManagerTest(BaseTestFixture baseTestFixture)
        {
            _baseTestFixture = baseTestFixture;
            _baseTestFixture.SetupBaseServiceScopeFactory();
            _uowMock = new Mock<IUow>();
        }

        [Fact]
        public async Task GetAllAsyncTest()
        {
            var data = GetInitialData();

            _uowMock.Setup(_ => _.Lawyer.GetQueryable())
               .Returns(data.BuildMock().Object);

            var service = GetService();

            var result = await service.GetAllAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetByIdAsyncTest()
        {
            var data = GetInitialData();

            _uowMock.Setup(_ => _.Lawyer.GetQueryable())
               .Returns(data.BuildMock().Object);

            var service = GetService();

            var result = await service.GetByIDAsync(3);
            Assert.NotNull(result);
        }


        #region Private methods

        ILawyerManager GetService()
        {
            var factory = _baseTestFixture.GetNewServiceScopeFactory(services =>
            {
                services.SwapService(_ => _uowMock.Object);
            });

            var scope = factory.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<ILawyerManager>();

            return service;
        }

        IQueryable<Lawyer> GetInitialData()
        {
            return Enumerable.Range(1, 5).Select(i => new Lawyer
            {
                Id = i,

            }).ToList().AsQueryable();
        }
        #endregion
    }
}
