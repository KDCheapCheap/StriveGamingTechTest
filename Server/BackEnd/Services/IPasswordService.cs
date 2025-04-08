namespace back_end.Services;

public interface IPasswordService
{
    public bool IsPasswordInvalid(string password);
    public bool IsPasswordCommon(string password);
}