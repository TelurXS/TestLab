using TestLab.Utils;

namespace TestLab.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string ProfileImage { get; set; }

        public static Account Create(string name, string login, string password) 
        {
            return new Account
            {
                Login = login,
                Password = new Hasher().Hash(password),
                Name = name,
                ProfileImage = "~/img/logo.png",
            };
        }
    }
}
