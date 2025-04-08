using back_end.Services;
using Microsoft.Extensions.Configuration;

namespace Test.BackEnd
{
    public class PasswordServiceTests
    {
        private readonly Dictionary<string, string> settings = new Dictionary<string, string>
            {
                { "CommonPasswordsPath", "Data\\common-passwords.txt" }
            };

        private readonly IConfiguration config;

        private readonly IPasswordService _validator;

        public PasswordServiceTests()
        {
            config = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            _validator = new PasswordService(config);
        }

        [Fact]
        public void PasswordTooShort_ReturnsInvalid()
        {
            var result = _validator.IsPasswordValid("As1!"); // 4 chars

            Assert.False(result.IsValid);
            Assert.Contains("between 7 and 14 characters", result.Message);
        }

        [Fact]
        public void PasswordTooLong_ReturnsInvalid()
        {
            var result = _validator.IsPasswordValid("As1!As1!As1!As1!"); // 16 chars

            Assert.False(result.IsValid);
            Assert.Contains("between 7 and 14 characters", result.Message);
        }

        [Fact]
        public void PasswordWithoutNumber_ReturnsInvalid()
        {
            var result = _validator.IsPasswordValid("Abcdef!#");

            Assert.False(result.IsValid);
            Assert.Contains("at least one number", result.Message);
        }

        [Fact]
        public void PasswordWithoutSpecialChar_ReturnsInvalid()
        {
            var result = _validator.IsPasswordValid("Abcdef12");

            Assert.False(result.IsValid);
            Assert.Contains("at least one special character", result.Message);
        }

        [Fact]
        public void PasswordWithInvalidChar_ReturnsInvalid()
        {
            var result = _validator.IsPasswordValid("Abc123@!");

            Assert.False(result.IsValid);
            Assert.Contains("can only contain letters, numbers", result.Message);
        }

        [Fact]
        public void ValidPassword_ReturnsValid()
        {
            var result = _validator.IsPasswordValid("Pass123#");

            Assert.True(result.IsValid);
            Assert.Equal("Password is valid.", result.Message);
        }

        [Fact]
        public void CommonPassword_FindsCommon()
        {
            var result = _validator.IsPasswordCommon("password123!");

            Assert.True(result);
        }

        [Fact]
        public void CommonPassword_FindsNotCommon()
        {
            var result = _validator.IsPasswordCommon("p#ssw0rd");

            Assert.False(result);
        }
    }
}
