namespace doma1project.Models
{
    public class LoginUser
    {

        public string UserName { get; set; }

        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Token { get; set; }

        public DateTime TokenCreated { get; set; }

        public DateTime TokenExpired { get; set; }

        public string Role { get; set; }
    }
}
