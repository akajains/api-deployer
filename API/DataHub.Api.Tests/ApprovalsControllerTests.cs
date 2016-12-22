using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using DataHub.Api.Controllers;
using DataHub.Data;
using DataHub.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace DataHub.Api.Tests
{
    [TestFixture]
    public class ApprovalsControllerTests
    {
        [Test]
        public async Task GetReturnsApprovalNotFound()
        {
            // Arrange
            var id = new Guid();
            var mockRepository = new Mock<IDataHubEfRepository>();
            mockRepository.Setup(x => x.GetApprovalAsync(id)).ReturnsAsync((Approval)null);

            var controller = CreateController(mockRepository);

            // Act
            var actionResult = await controller.GetAsync(id);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(actionResult);
        }

        [Test]
        public async Task GetReturnsApprovalWithSameId()
        {
            // Arrange
            var id = new Guid();
            var mockRepository = new Mock<IDataHubEfRepository>();
            mockRepository.Setup(x => x.GetApprovalAsync(id)).Returns(Task.FromResult(new Approval {Id = id}));

            var controller = CreateController(mockRepository);

            // Act
            var actionResult = await controller.GetAsync(id);
            var contentResult = actionResult as OkNegotiatedContentResult<Dtos.Approval>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(id, contentResult.Content.Id);
        }

        private static ApprovalsController CreateController(IMock<IDataHubEfRepository> mockRepository)
        {
            const string locationUrl = "http://localhost/api/approvals";

            var controller = new ApprovalsController(mockRepository.Object)
            {
                Request = new HttpRequestMessage
                {
                    RequestUri = new Uri(locationUrl)
                },
                Configuration = new HttpConfiguration()
            };

            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "approvals" } });

            return controller;
        }
    }
}
