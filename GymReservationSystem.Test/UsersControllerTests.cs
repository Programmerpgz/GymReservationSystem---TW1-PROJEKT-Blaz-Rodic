using FluentAssertions;
using GymReservationSystem.Application.DTOs;
using GymReservationSystem.Application.Interface;
using GymReservationSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace GymReservationSystem.Tests.Controllers;

public class UsersControllerTests
{
    private readonly Mock<IUserService> _mockService;
    private readonly Mock<ILogger<UsersController>> _mockLogger;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _mockService = new Mock<IUserService>();
        _mockLogger = new Mock<ILogger<UsersController>>();
        _controller = new UsersController(_mockService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkWithList()
    {
        //Arrange
        var data = new List<UserDto> 
        { 
            new UserDto 
            { 
                Id = 1, 
                FirstName = "Test" 
            } 
        };
        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(data);

        //Act
        var result = await _controller.GetAll();

        //Assert
        var ok = result.Should().BeOfType<OkObjectResult>().Subject;
        ok.Value.Should().BeEquivalentTo(data);
    }

    [Fact]
    public async Task GetById_Exists_ReturnsOk()
    {
        //Arrange
        var dto = new UserDto
        { 
            Id = 1, 
            FirstName = "Test" 
        };
        _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(dto);

        //Act
        var result = await _controller.GetById(1);

        //Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetById_NotExists_ReturnsNotFound()
    {
        //Arrange
        _mockService.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((UserDto?)null);

        //Act
        var result = await _controller.GetById(99);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction()
    {
        //Arrange
        var dto = new UserDto 
        { 
            FirstName = "Novi", 
            LastName = "Korisnik",
            Email = "novi@test.com", 
            Phone = "091", 
            IsActive = true 
        };
        var created = new UserDto 
        { 
            Id = 1, 
            FirstName = "Novi", 
            LastName = "Korisnik", 
            Email = "novi@test.com", 
            Phone = "091", 
            IsActive = true 
        };
        _mockService.Setup(s => s.CreateAsync(dto)).ReturnsAsync(created);

        //Act
        var result = await _controller.Create(dto);

        //Assert
        result.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task Update_ReturnsOk()
    {
        //Arrange
        var dto = new UserDto 
        { 
            Id = 1, 
            FirstName = "Updated" 
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