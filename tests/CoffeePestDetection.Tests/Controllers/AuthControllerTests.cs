using CoffeePestDetection.API.Controllers;
using CoffeePestDetection.Application.Features.Auth.DTOs;
using CoffeePestDetection.Application.Interfaces;
using CoffeePestDetection.Tests.Helpers;

namespace CoffeePestDetection.Tests.Controllers;

public class AuthControllerTests : ControllerTestBase
{
    private readonly Mock<IAuthService> _authServiceMock;

    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _authServiceMock = new Mock<IAuthService>();

        _controller = new AuthController(_authServiceMock.Object);
    }

    #region Login

    [Fact]
    public async Task Login_WhenCredentialsAreValid_ShouldReturnOk()
    {
        // Arrange

        var request = new LoginRequestDto
        {
            Email = "admin@test.com",
            Password = "Admin123*"
        };

        var expected = new LoginResponseDto
        {
            Token = "jwt-token",
            Expiration = DateTime.UtcNow.AddHours(8),
            FullName = "Administrador"
        };

        _authServiceMock
            .Setup(x => x.LoginAsync(request))
            .ReturnsAsync(expected);

        // Act

        IActionResult result = await _controller.Login(request);

        // Assert

        var response = GetApiResponse<LoginResponseDto>(result);

        response.Success.Should().BeTrue();

        response.Message.Should().Be("Autenticación exitosa");

        var data = GetResponseData<LoginResponseDto>(result);

        data.FullName.Should().Be(expected.FullName);
        data.Token.Should().Be(expected.Token);
        data.Expiration.Should().Be(expected.Expiration);

        _authServiceMock.Verify(
            x => x.LoginAsync(request),
            Times.Once);

        _authServiceMock.VerifyNoOtherCalls();
    }

    #endregion

    #region Register

    [Fact]
    public async Task Register_WhenRequestIsValid_ShouldReturnOk()
    {
        // Arrange

        var request = new RegisterRequestDto
        {
            FullName = "Juan Escorcia",
            Email = "juan@test.com",
            Password = "Admin123*",
            Role = "Admin",
            OrganizationId = Guid.NewGuid()
        };

        var expected = new RegisterResponseDto
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Email = request.Email,
            Role = request.Role!
        };

        _authServiceMock
            .Setup(x => x.RegisterAsync(request))
            .ReturnsAsync(expected);

        // Act

        IActionResult result = await _controller.Register(request);

        // Assert

        var response = GetApiResponse<RegisterResponseDto>(result);

        response.Success.Should().BeTrue();

        response.Message.Should()
            .Be("Usuario registrado correctamente.");

        var data = GetResponseData<RegisterResponseDto>(result);

        data.Id.Should().Be(expected.Id);
        data.FullName.Should().Be(expected.FullName);
        data.Email.Should().Be(expected.Email);
        data.Role.Should().Be(expected.Role);

        _authServiceMock.Verify(
            x => x.RegisterAsync(request),
            Times.Once);

        _authServiceMock.VerifyNoOtherCalls();
    }

    #endregion
}
