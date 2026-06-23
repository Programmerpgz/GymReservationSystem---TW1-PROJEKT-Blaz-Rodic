using FluentAssertions;
using GymReservationSystem.Application.DTOs;
using GymReservationSystem.Application.Interface;
using GymReservationSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace GymReservationSystem.Tests.Controllers;

public class RolesControllerTests
{
    private readonly Mock<IRoleService> _mockService;
    private readonly Mock<ILogger<RolesController>> _mockLogger;
    private readonly RolesController _controller;

    public RolesControllerTests()
    {
        _mockService = new Mock<IRoleService>();
        _mockLogger = new Mock<ILogger<RolesController>>();
        _controller = new RolesController(_mockService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        //Arrange
        var data = new List<RoleDto> 
        { 
            new RoleDto 
            { 
                Id = 1, 
                Name = "Admin" 
            } 
        };
        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(data);

        //Act
        var result = await _controller.GetAll();

        //Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetById_Exists_ReturnsOk()
    {
        //Arrange
        _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(new RoleDto 
        { 
            Id = 1, 
            Name = "Admin" 
        });

        //Act
        var result = await _controller.GetById(1);

        //Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetById_NotExists_ReturnsNotFound()
    {
        //Arrange
        _mockService.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((RoleDto?)null);

        //Act
        var result = await _controller.GetById(99);

        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction()
    {
        //Arrange
        var dto = new RoleDto { Name = "Test" };
        _mockService.Setup(s => s.CreateAsync(dto)).ReturnsAsync(new RoleDto 
        { 
            Id = 3, 
            Name = "Test" 
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
        var dto = new RoleDto 
        { 
            Id = 1, 
            Name = "Updated" 
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