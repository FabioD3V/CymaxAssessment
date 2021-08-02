using CymaxAssessmentAPI.Controllers;
using CymaxAssessmentAPI.Services;
using CymaxAssessmentAPI.Tests.FakeContent;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CymaxAssessmentAPI.Tests
{
    public class CartonControllerTest
    {
        ICartonService _cartonService;
        CartonController _cartonController;

        public CartonControllerTest()
        {
            _cartonService = new FakeCartonService();
            _cartonController = new CartonController(_cartonService);
        }

        [Fact]
        public void ClientRequestFromCompanyAPI1_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var clientRequest = FakeClientRequestCollection.GetFakeRequestAPI1();

            // Act
            var okResult = _cartonController.GetCartonOfferAPI1(clientRequest);
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void ClientRequestFromCompanyAPI2_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var clientRequest = FakeClientRequestCollection.GetFakeRequestAPI2();

            // Act
            var okResult = _cartonController.GetCartonOfferAPI2(clientRequest);
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void ClientRequestFromCompanyAPI31_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var clientRequest = FakeClientRequestCollection.GetFakeRequestAPI3();

            // Act
            var okResult = _cartonController.GetCartonOfferAPI3(clientRequest);
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }
    }
}
