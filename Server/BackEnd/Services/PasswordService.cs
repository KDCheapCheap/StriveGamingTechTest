namespace back_end.Services;

public class PasswordService : IPasswordService
{
    public bool IsPasswordInvalid(string password)
    {
        return false;
    }
    
    public bool IsPasswordCommon(string? password)
    {
        return false;
    }
}