using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using back_end.Controllers;
using back_end.Models;
using back_end.Services;

namespace Test.BackEnd;

public class PasswordControllerTests
{
    private readonly Mock<IPasswordService> _mockPasswordService = new();
    private readonly Mock<ILogger<PasswordController>> _mockLogger = new();

    private PasswordController CreateController() =>
        new PasswordController(_mockLogger.Object, _mockPasswordService.Object);

    [Fact]
    public void SetPassword_ReturnsBadRequest_WhenRequestIsNull()
    {
        var controller = CreateController();

        var result = controller.SetPassword(null);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void SetPassword_ReturnsBadRequest_WhenPasswordIsEmpty()
    {
        var controller = CreateController();
        var request = new PasswordChangeRequest { Password = "   " };

        var result = controller.SetPassword(request);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void SetPassword_ReturnsBadRequest_WhenPasswordIsInvalid()
    {
        _mockPasswordService
            .Setup(s => s.IsPasswordValid("bad"))
            .Returns(new PasswordValidationResponse(false, "Too short"));

        var controller = CreateController();
        var request = new PasswordChangeRequest { Password = "bad" };

        var result = controller.SetPassword(request);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void SetPassword_ReturnsBadRequest_WhenPasswordIsCommon()
    {
        _mockPasswordService
            .Setup(s => s.IsPasswordValid("GoodPass1!"))
            .Returns(new PasswordValidationResponse(true, "Valid"));

        _mockPasswordService
            .Setup(s => s.IsPasswordCommon("GoodPass1!"))
            .Returns(true);

        var controller = CreateController();
        var request = new PasswordChangeRequest { Password = "GoodPass1!" };

        var result = controller.SetPassword(request);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void SetPassword_ReturnsOk_WhenPasswordIsValidAndUncommon()
    {
        _mockPasswordService
            .Setup(s => s.IsPasswordValid("GreatPass1!"))
            .Returns(new PasswordValidationResponse(true, "Valid"));

        _mockPasswordService
            .Setup(s => s.IsPasswordCommon("GreatPass1!"))
            .Returns(false);

        var controller = CreateController();
        var request = new PasswordChangeRequest { Password = "GreatPass1!" };

        var result = controller.SetPassword(request);

        Assert.IsType<OkResult>(result);
    }
}
