using System;
using System.ComponentModel.DataAnnotations;
using TestLab.Utils;

namespace TestLab.Entities
{
    public enum AccountState 
    {
        User,
        Redactor,
        Administrator,
        Deleted,
    }

    public sealed class Account
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ProfileImage { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; }

        public AccountState State { get; set; }

        private Account() { }

        public static Account Create(string name, string login, string email, string password) 
        {
            return new Account
            {
                Login = login,
                Password = password,
                Name = name,
                Description = "",
                Phone = "",
                Email = email,
                Address = "",
                ProfileImage = Config.Account.DefaultProfileImage,
                BirthDate = DateTime.Now,
                RegistrationDate = DateTime.Now,
                State = AccountState.User,
            };
        }

        public bool HavePermission(AccountState permission) 
        {
            return State >= permission;
        }
    }
}
