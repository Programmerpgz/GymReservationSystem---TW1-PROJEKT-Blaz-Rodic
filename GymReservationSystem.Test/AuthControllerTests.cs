using FluentAssertions;
using GymReservationSystem.Application.DTOs;
using GymReservationSystem.Application.Interface;
using GymReservationSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace GymReservationSystem.Tests.Controllers;

public class AuthControllerTests
{
    private readonly Mock<IAuthService> _mockService;
    private readonly Mock<ILogger<AuthController>> _mockLogger;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _mockService = new Mock<IAuthService>();
        _mockLogger = new Mock<ILogger<AuthController>>();
        _controller = new AuthController(_mockService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsOk()
    {
        //Arrange
        var dto = new LoginDto 
        { 
            Email = "admin@gym.com", 
            Password = "Admin123!" 
        };
        var response = new AuthResponseDto 
        { 
            Token = "jwt-token", 
            Email = "admin@gym.com", 
            Role = "Admin" 
        };
        _mockService.Setup(s => s.LoginAsync(dto)).ReturnsAsync(response);

        //Act
        var result = await _controller.Login(dto);

        //Assert
        var ok = result.Should().BeOfType<OkObjectResult>().Subject;
        ok.Value.Should().BeEquivalentTo(response);
    }

    [Fact]
    public async Task Login_InvalidCredentials_ThrowsException()
    {
        //Arrange
        var dto = new LoginDto 
        { 
            Email = "bad@test.com", 
            Password = "wrong" 
        };
        _mockService.Setup(s => s.LoginAsync(dto)).ThrowsAsync(new Exception("Invalid email or password"));

        //Act
        var result = await _controller.Login(dto);

        //Assert
        result.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(500);
    }

    [Fact]
    public async Task Register_ValidData_ReturnsOk()
    {
        //Arrange
        var dto = new RegisterDto 
        { 
            FirstName = "Novi", 
            LastName = "Korisnik", 
            Email = "novi@test.com", 
            Password = "Password123!", 
            Phone = "091" 
        };
        var response = new AuthResponseDto 
        { 
            Token = "jwt-token", 
            Email = "novi@test.com", 
            Role = "User" 
        };
        _mockService.Setup(s => s.RegisterAsync(dto)).ReturnsAsync(response);

        //Act
        var result = await _controller.Register(dto);

        //Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Register_DuplicateEmail_ThrowsException()
    {
        //Arrange
        var dto = new RegisterDto 
        { 
            Email = "admin@gym.com", 
            Password = "Password123!" 
        };
        _mockService.Setup(s => s.RegisterAsync(dto)).ThrowsAsync(new Exception("Email already exists"));

        //Act
        var result = await _controller.Register(dto);

        //Assert
        result.Should().BeOfType<ObjectResult>().Which.StatusCode.Should().Be(500);
    }
}