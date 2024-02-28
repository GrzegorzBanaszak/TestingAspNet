using CloudeCustomers.API.Controllers;
using CloudeCustomers.API.Models;
using CloudeCustomers.API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudeCustomers.UnitTests.Systems.Controllers
{
    public class TestUsersController
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode200()
        {
            //Arrange
            var mockUsersService = new Mock<IUsersService>();

            mockUsersService.Setup(service => service.GetAllUsers()).ReturnsAsync(new List<User>());

            var sut = new UsersController(mockUsersService.Object);

            //Act

            var result = (OkObjectResult)await sut.Get();

            //Assert

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnSuccess_InvokesUserServiceExactlyOnce()
        {
            //Arrange
            var mockUsersService = new Mock<IUsersService>();

            mockUsersService.Setup(service => service.GetAllUsers()).ReturnsAsync(new List<User>());

            var sut = new UsersController(mockUsersService.Object);

            //Act

            var result = await sut.Get();

            //Assert
            mockUsersService.Verify(
                service => service.GetAllUsers(),
                Times.Once()
            );
        }
    }
}