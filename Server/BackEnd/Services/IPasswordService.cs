using back_end.Models;

namespace back_end.Services;

public interface IPasswordService
{
    PasswordValidationResponse IsPasswordValid(string password);
    public bool IsPasswordCommon(string password);
}