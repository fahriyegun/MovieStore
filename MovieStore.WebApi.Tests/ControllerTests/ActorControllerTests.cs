using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieStore.WebApi.Controllers;
using MovieStore.WebApi.Interfaces;
using MovieStore.WebApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MovieStore.WebApi.Tests.ControllerTests
{
    public class ActorControllerTests
    {
        private readonly Mock<IActor> _mockRepo;
        private readonly ActorController _actorController;
        private List<ActorViewModel> _actorList;

        public ActorControllerTests()
        {
            _mockRepo = new Mock<IActor>();
            _actorController = new ActorController(_mockRepo.Object);
            _actorList = DummyDataGenerator.ActorList;

        }

        [Fact]
        public void GetAll_ActionExecute_ReturnOkResultWithAllActors()
        {
            _mockRepo.Setup(x => x.GetAll()).Returns(_actorList);

            var result = _actorController.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actorsResult = Assert.IsAssignableFrom<List<ActorViewModel>>(okResult.Value);
            Assert.Equal(2, actorsResult.ToList().Count);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GetById_IdLessOrEqualThanZero_ThrowException(int id)
        {
            _mockRepo.Setup(x=> x.GetById()).Returns(new ActorViewModel());
            _mockRepo.Setup(x => x.ActorId).Returns(id);

            var exception = Assert.Throws<ValidationException>(() => _actorController.GetById(id));
            Assert.Contains("'Actor Id' must be greater than '0'", exception.Message);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(77)]
        public void GetById_IdIsNotFound_ReturnNotFoundResult(int id)
        {
            _mockRepo.Setup(x => x.GetById()).Returns(new ActorViewModel());
            _mockRepo.Setup(x => x.ActorId).Returns(id);

            var result = _actorController.GetById(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_IdValid_ReturnOkResult(int id)
        {
            var actor = _actorList.First();
            _mockRepo.Setup(x => x.GetById()).Returns(actor);
            _mockRepo.Setup(x => x.ActorId).Returns(id);

            var result = _actorController.GetById(id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var actorResult = Assert.IsType<ActorViewModel>(okResult.Value);
            Assert.Equal(actor.Movies.Count, actorResult.Movies.Count);
            Assert.Equal(actor.FullName, actorResult.FullName);
        }

        [Fact]
        public void Add_ActionExecutes_ReturnOkResultWithId()
        {
            ActorCreateModel model = new ActorCreateModel
            {
                Name = "testUserName",
                Surname = "testUserSurname",
                Movies = new List<MovieActorCreateModel> { new MovieActorCreateModel { Id = 1 } }
            };

            int expectedNewActorId = 1;

            _mockRepo.Setup(x => x.Add()).Returns(expectedNewActorId);
            _mockRepo.Setup(x => x.ActorCreateModel).Returns(model);

            var result = _actorController.Add(model);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var newActorId = okObjectResult.Value;
            Assert.Equal(expectedNewActorId, newActorId);

            _mockRepo.Verify(x => x.Add(), Times.Once);


        }
        [Fact]
        public void Update_InvalidInput_ThrowException()
        {          
            ActorUpdateModel model = new ActorUpdateModel
            {
                Id = 0,
                Name ="xy",
                Surname ="xy"
            };

            _mockRepo.Setup(x => x.Update());
            _mockRepo.Setup(x => x.ActorUpdateModel).Returns(model);

            var exception = Assert.Throws<ValidationException>(() => _actorController.Update(model));
            Assert.Contains("ActorUpdateModel.Id: 'Actor Update Model Id' must be greater than '0'", exception.Message);
            Assert.Contains("ActorUpdateModel.Name: The length of 'Actor Update Model Name' must be at least 3 characters.", exception.Message);
            Assert.Contains("ActorUpdateModel.Surname: The length of 'Actor Update Model Surname' must be at least 3 characters.", exception.Message);
        }

        [Fact]
        public void Update_ValidInput_ReturnNoContent()
        {
            ActorUpdateModel model = new ActorUpdateModel
            {
                Id = 7,
                Name = "xyz",
                Surname = "xyz"
            };

            _mockRepo.Setup(x => x.Update());
            _mockRepo.Setup(x => x.ActorUpdateModel).Returns(model);

            var result = _actorController.Update(model);
            _mockRepo.Verify(x => x.Update(), Times.Once);
            Assert.IsType<NoContentResult>(result);
        }


        [Theory]
        [InlineData(1)]
        public void Delete_ActionExecute_ReturnNoContent(int id)
        {
            _mockRepo.Setup(x => x.ActorId).Returns(id);
            _mockRepo.Setup(x => x.Delete());

            var noContentResult = _actorController.Delete(id);

            _mockRepo.Verify(x => x.Delete(), Times.Once);

            Assert.IsType<NoContentResult>(noContentResult);
        }

    }
}
