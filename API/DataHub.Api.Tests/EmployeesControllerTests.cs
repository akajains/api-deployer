using System;
using System.Collections.Generic;
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
    public class EmployeesControllerTests
    {
        [Test]
        public async Task GetEmployeesReturnsWithCollection()
        {
            // Arrange
            var locationUri = "http://localhost/api/employees";
            var routeData = new HttpRouteData(new HttpRoute(), new HttpRouteValueDictionary());
            var id = "123";
            var employee = new Employee { Id = id, PositionId = "456"};
            var employees = new List<Employee> { employee };
            var mockRepository = new Mock<IDataHubEfRepository>();
            mockRepository.Setup(x => x.GetEmployeesAsync(null, null, null, null, null, null, e => e.Manager, e => e.OrgUnit)).ReturnsAsync(employees);
            var controller = CreateController(mockRepository, locationUri, routeData);

            // Act
            var actionResult = await controller.GetAsync();
            var contentResult = actionResult as OkNegotiatedContentResult<List<Dtos.Employee>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(id, contentResult.Content[0].Id);
        }

        [Test]
        public async Task GetReturnsNotFound()
        {
            // Arrange
            var locationUri = "http://localhost/api/employees/123";
            var routeData = new HttpRouteData(new HttpRoute(), new HttpRouteValueDictionary { { "id", 123 } });
            var id = "123";
            var mockRepository = new Mock<IDataHubEfRepository>();
            mockRepository.Setup(x => x.GetEmployeeAsync(id, e => e.Manager, e => e.OrgUnit)).ReturnsAsync((Employee)null);

            var controller = CreateController(mockRepository, locationUri, routeData);

            // Act
            var actionResult = await controller.GetAsync(int.Parse(id));

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(actionResult);
        }

        [Test]
        public async Task GetReturnsWithSameId()
        {
            // Arrange
            var locationUri = "http://localhost/api/employees/123";
            var routeData = new HttpRouteData(new HttpRoute(), new HttpRouteValueDictionary {{"id", 123}});
            var id = "123";
            var employee = new Employee {Id = id};
            var mockRepository = new Mock<IDataHubEfRepository>();
            mockRepository.Setup(x => x.GetEmployeeAsync(id, e => e.Manager, e => e.OrgUnit)).ReturnsAsync(employee);
            var controller = CreateController(mockRepository, locationUri, routeData);

            // Act
            var actionResult = await controller.GetAsync(int.Parse(id));
            var contentResult = actionResult as OkNegotiatedContentResult<Dtos.Employee>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(id, contentResult.Content.Id);
        }

        private static EmployeesController CreateController(IMock<IDataHubEfRepository> mockRepository, string locationUri, IHttpRouteData routeData)
        {
            var controller = new EmployeesController(mockRepository.Object)
            {
                Request = new HttpRequestMessage
                {
                    RequestUri = new Uri(locationUri)
                },
                Configuration = new HttpConfiguration()
            };

            controller.Configuration.MapHttpAttributeRoutes();

            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = routeData ?? new HttpRouteData(new HttpRoute(), new HttpRouteValueDictionary());

            //controller.RequestContext.RouteData = new HttpRouteData(
            //    route: new HttpRoute(),
            //    values: new HttpRouteValueDictionary { { "controller", "employees" }, { "id", 123 } });
                //values: new HttpRouteValueDictionary { { "id", 123 } });

            controller.Configuration.EnsureInitialized();

            return controller;
        }
    }
}
