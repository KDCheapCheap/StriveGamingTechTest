namespace back_end.Models
{
    public class PasswordValidationResponse
    {
        public bool IsValid { get; set; }
        public string Message { get; set; } = string.Empty;

        public PasswordValidationResponse()
        {
            IsValid = false;
            Message = string.Empty;
        }
        
        public PasswordValidationResponse(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }
    }
}
