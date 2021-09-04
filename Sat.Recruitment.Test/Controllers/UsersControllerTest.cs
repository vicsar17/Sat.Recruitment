using AutoMapper;
using Moq;
using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.DataAccess.Models;
using Sat.Recruitment.Domain.Infrastructure.ErrorHandler;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Domain.Repositories;
using Sat.Recruitment.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xunit;

namespace Sat.Recruitment.Test.Controllers
{
    public class UsersControllerTest : IClassFixture<TestFixture<Startup>>
    {
        public UsersControllerTest(TestFixture<Startup> fixture)
        {
            //Arrange
            var entities = new List<User>
        {
            new User
            {
                Name = "Juan",
                Email = "juan@gmail.com",
                Address = "Cuba Avenue",
                Phone = "+507 66667777",
                UserType = "Normal",
                Money = 90
            }
        };

            Repository = new Mock<IBaseRepository<User>>();

            Repository.Setup(x => x.GetAll())
                .ReturnsAsync(entities);

            Repository.Setup(x => x.Insert(It.IsAny<User>()))
                .Callback((User entity) => entities.Add(entity));

            Repository.Setup(x => x.Update(It.IsAny<User>()))
                .Callback((User entity) => entities[entities.FindIndex(x => x.Id == entity.Id)] = entity);           

            var imapper = (IMapper)fixture.Server.Host.Services.GetService(typeof(IMapper));
            var ierrorHandler = (IErrorHandler)fixture.Server.Host.Services.GetService(typeof(IErrorHandler));

            //SERVICES CONFIGURATIONS
            var baseService = new BaseService<User>(Repository.Object);
            Service = new UserService(baseService, imapper, ierrorHandler);
            Controller = new UsersController(Service, ierrorHandler, imapper);
        }

        private Mock<IBaseRepository<User>> Repository { get; }

        private IUserService Service { get; }

        private UsersController Controller { get; }

        [Fact]
        public void GetUsersController()
        {
            //Act
            var result = Controller.Get().Result;

            // Assert
            Repository.Verify(x => x.GetAll(), Times.AtMostOnce);
            Assert.Equal(3,result.Count());
        }

        [Theory]
        [InlineData("Justin", "justin@gmail.com", "Address a", "+507 22224888", "Normal",151)]
        public async void CreateUser(string name, string email, string address, string phone, string userType, decimal money)
        {
            //Arrange
            var entity = new UserResponseModel
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = money
            };
            //Act
            var postResult = await Controller.Post(entity);
            //Assert           
            Assert.True(postResult.IsSuccess);
            Assert.Equal("User created.", postResult.Message);

            //Database Assert
            //Only works when the AddOrUpdate() method is uncommented in the controller.
            //var recordsQty = 2;//Expected value: quantity number of records that contains the BD adding this new record.
            //var result = Controller.Get().Result;
            //Repository.Verify(x => x.Insert(It.IsAny<User>()), Times.Once);
            //Assert.Equal(recordsQty, result.Count());

        }

        [Fact]
        public async void CreateDuplicatedUser()
        {
            //Arrange
            var entity = new UserResponseModel
            {
                Name = "Justin",
                Email = "agustina@gmail.com",
                Address = "Address a",
                Phone = "+507 22224888",
                UserType = "Normal",
                Money = 151
            };
            //Act
            var postResult = await Controller.Post(entity);
            //Assert

            Assert.False(postResult.IsSuccess);
            Assert.Equal("The user is duplicated", postResult.Errors);

        }

        [Theory]
        [InlineData("Justin", "justin@gmail.com", "Address a", "+507 22224888", "Normal", 151)]
        public async void ValidateRequestData(string name, string email, string address, string phone, string userType, decimal money)
        {
            //Arrange
            var entity = new UserResponseModel
            {
                Name = null,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = money
            };
            //Act
            var postResult = await Controller.Post(entity);
            //Assert

            Assert.False(postResult.IsSuccess);
            Assert.Equal("The name is required.", postResult.Errors);
            Assert.NotEqual("The email is required.", postResult.Errors);
            Assert.NotEqual("The phone is required.", postResult.Errors);
            Assert.NotEqual("The address is required.", postResult.Errors);
            Assert.NotEqual("The usertype is required.", postResult.Errors);
            Assert.NotEqual("The money is required.", postResult.Errors);
        }

    }
}
