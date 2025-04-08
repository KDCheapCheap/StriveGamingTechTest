using back_end.Models;
using back_end.Services;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers;

[ApiController]
[Route("[controller]")]
public class PasswordController : ControllerBase
{
    private readonly ILogger<PasswordController> _logger;
    private readonly IPasswordService _passwordService;
    
    public PasswordController(ILogger<PasswordController> logger, IPasswordService passwordService)
    {
        _logger = logger;
        _passwordService = passwordService;
    }

    [HttpPost("change")]
    public IActionResult SetPassword(PasswordChangeRequest request)
    {
        _logger.LogInformation("Received password change request ");

        if (request == null)
        {
            _logger.LogWarning("Password change request was null");
            return BadRequest(new { Message = "Request body cannot be null." });
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            _logger.LogWarning("Password is null or whitespace");
            return BadRequest(new { Message = "Password cannot be empty." });
        }

        PasswordValidationResponse validationResponse = _passwordService.IsPasswordValid(request.Password);

        if (!validationResponse.IsValid)
        {
            _logger.LogWarning("Password failed complexity requirements. Password: {Password}", request.Password);
            return BadRequest(new { validationResponse.Message });
        }

        if (_passwordService.IsPasswordCommon(request.Password))
        {
            _logger.LogWarning("Password is too common. Password: {Password}", request.Password);
            return BadRequest(new { Message = "Password is too common." });
        }

        return Ok();
    }
}