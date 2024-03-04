namespace doma1project.Models
{
    public class RegisterUser
    {

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword{ get; set; }
    }
}
