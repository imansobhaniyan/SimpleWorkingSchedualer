using SimpleWorkingSchedualer.StorageModels;

namespace SimpleWorkingSchedualer.Models.Login
{
    public class LoginResult
    {
        private LoginResult(string token, int userRole)
        {
            Success = !string.IsNullOrWhiteSpace(token);
            Token = token;
            UserRole = userRole;
        }

        public bool Success { get; private set; }

        public string Token { get; private set; }

        public int UserRole { get; set; }

        public static LoginResult FailedResult()
        {
            return new LoginResult(null, 0);
        }

        public static LoginResult SuccessResult(string token, User.UserRole userRole)
        {
            return new LoginResult(token, (int)userRole);
        }
    }
}
