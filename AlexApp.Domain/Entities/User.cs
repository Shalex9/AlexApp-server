using System;
using System.Collections.Generic;
using System.Text;

namespace AlexApp.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Title { get; private set; }
        public string Password { get; private set; }
        public string Email { get; set; }

        public static User CreateNew(string name, string title, string password, string email)
        {
            var user = new User();
            user.ChangeName(name);
            user.ChangeTitle(title);
            user.ChangePassword(null, password);
            user.Email = email;

            return user;
        }

        public void ChangeName(string name)
        {
            if (name.Length < 2)
            {
                throw new ArgumentException("Name needs at least 2 characters");
            }
            Name = name;
        }

        public void ChangeTitle(string title)
        {
            if (title.Length < 3)
            {
                throw new ArgumentException("Title needs at least 3 characters");
            }
            Title = title;
        }

        public void ChangePassword(string oldPassword, string newPassword)
        {
            if (oldPassword != Password)
            {
                throw new ArgumentException("Mismatch of old password and password");
            }
            Password = newPassword;
        }
    }
}
