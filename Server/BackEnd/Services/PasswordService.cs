using System.Text;
using System.Text.RegularExpressions;
using back_end.Models;

namespace back_end.Services;

public class PasswordService : IPasswordService
{
    private readonly string _commonPasswordsFilePath;

    public PasswordService(IConfiguration configuration)
    {
        // Retrieve the file path from appsettings.json
        _commonPasswordsFilePath = configuration["CommonPasswordsPath"]
            ?? throw new ArgumentNullException("CommonPasswordsPath is not configured.");
    }

    public PasswordValidationResponse IsPasswordValid(string password)
    {
        StringBuilder message = new StringBuilder();

        // Check if the password length is between 7 and 14 characters
        bool length = Regex.IsMatch(password, @"^(?=.{7,14}$)");
        if (!length)
        {
            message.Append("Password must be between 7 and 14 characters long. \n");
        }

        // Check if the password contains at least one number
        bool hasNumber = Regex.IsMatch(password, @"(?=.*[0-9])");
        if (!hasNumber)
        {
            message.Append("Password must contain at least one number. \n");
        }

        // Check if the password contains at least one special character (!, £, $, ^, *, #)
        bool hasSpecialChar = Regex.IsMatch(password, @"(?=.*[!£$^*#])");
        if (!hasSpecialChar)
        {
            message.Append("Password must contain at least one special character (!, £, $, ^, *, #). \n");
        }

        // Check if the password contains only allowed characters (letters, numbers, and the specified special characters)
        bool allowedChars = Regex.IsMatch(password, @"^[a-zA-Z0-9!£$^*#]*$");
        if (!allowedChars)
        {
            message.Append("Password can only contain letters, numbers, and the following special characters: !, £, $, ^, *, #. ");
        }

        bool isValid = length && hasNumber && hasSpecialChar && allowedChars;

        if(isValid)
        {
            message.Clear();
            message.Append("Password is valid.");
        }

        return new PasswordValidationResponse(isValid, message.ToString());

    }

    public bool IsPasswordCommon(string? password)
    {
        if(string.IsNullOrWhiteSpace(password))
        {
            return false;
        }

        HashSet<string> result = File.ReadLines(_commonPasswordsFilePath)
            .Select(line => line.Trim())
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        return result.Contains(password);
    }
}