using System;
using TestLab.Entities;

namespace TestLab.Models
{
    public class ProfileViewModel
    {
        public Account Account { get; set; }

        public string Message { get; set; }
        public bool HasMessage => string.IsNullOrEmpty(Message) is false;


        public int Id => Account.Id;
        public string Login => Account.Login;
        public string Password => Account.Password;
        public string Name => Account.Name;
        public string Description => Account.Description;
        public string Phone => Account.Phone;
        public string Email => Account.Email;
        public string Address => Account.Address;
        public string ProfileImage => Account.ProfileImage;

        public DateTime BirthDate => Account.BirthDate;
        public DateTime RegistrationDate => Account.RegistrationDate;

        public AccountState State => Account.State;

    }
}