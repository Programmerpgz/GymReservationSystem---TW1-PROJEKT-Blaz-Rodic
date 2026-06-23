using FluentAssertions;
using GymReservationSystem.Application.DTOs;
using GymReservationSystem.Application.Interface;
using GymReservationSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace GymReservationSystem.Tests.Controllers;

public class TrainingsControllerTests
{
    private readonly Mock<ITrainingService> _mockService;
    private readonly Mock<ILogger<TrainingsController>> _mockLogger;
    private readonly TrainingsController _controller;

    public TrainingsControllerTests()
    {
        _mockService = new Mock<ITrainingService>();
        _mockLogger = new Mock<ILogger<TrainingsController>>();
        _controller = new TrainingsController(_mockService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        //Arrange
        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<TrainingDto>());

        //Act
        var result = await _controller.GetAll();

        //Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetById_NotExists_ReturnsNotFound()
    {
        //Arrange
        _mockService.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((TrainingDto?)null);

        //Act
        var result = await _controller.GetById(99);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction()
    {
        //Arrange
        var dto = new TrainingDto 
        { 
            Name = "Yoga", 
            TrainerId = 1, 
            RoomId = 1 
        };
        _mockService.Setup(s => s.CreateAsync(dto)).ReturnsAsync(new TrainingDto 
        { 
            Id = 1, 
            Name = "Yoga", 
            TrainerId = 1, 
            RoomId = 1 
        });

        //Act
        var result = await _controller.Create(dto);

        //Assert
        result.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task Update_ReturnsOk()
    {
        //Arrange
        var dto = new TrainingDto 
        { 
            Id = 1, 
            Name = "Yoga Updated" 
        };
        _mockService.Setup(s => s.UpdateAsync(1, dto)).ReturnsAsync(dto);

        //Act
        var result = await _controller.Update(1, dto);

        //Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Delete_Exists_ReturnsNoContent()
    {
        //Arrange
        _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

        //Act
        var result = await _controller.Delete(1);

        //Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Delete_NotExists_ReturnsNotFound()
    {
        //Arrange
        _mockService.Setup(s => s.DeleteAsync(99)).ReturnsAsync(false);

        //Act
        var result = await _controller.Delete(99);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}