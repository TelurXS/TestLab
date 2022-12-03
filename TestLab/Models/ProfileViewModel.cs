using System;
using System.Collections.Generic;
using TestLab.Entities;
using TestLab.Entities.Projects;

namespace TestLab.Models
{
    public class ProfileViewModel : MessageViewModel
    {
        public Account Account { get; set; }

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

        public IEnumerable<Project> Projects { get; set; }
    }
}