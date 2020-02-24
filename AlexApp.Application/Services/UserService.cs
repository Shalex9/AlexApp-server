using AutoMapper;
using AlexApp.Application.Dto;
using AlexApp.Application.Services.Contracts;
using AlexApp.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AlexApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDto Get(int id)
        {
            return Mapper.Map<UserDto>(_userRepository.Get(id));
        }

        public UserDto Get(string username)
        {
            return Mapper.Map<UserDto>(_userRepository.GetByUsername(username));
        }

        //public void Update(int id, UserUpdate update)
        //{
        //    var user = _userRepository.Get(id);

        //    var userByUsername = _userRepository.GetByUsername(update.Name);
        //    if (userByUsername != null && userByUsername.Id != user.Id)
        //    {
        //        throw new ArgumentException("Name is already exist");
        //    }

        //    user.ChangeName(update.Name);
        //    user.ChangeTitle(update.Title);
        //    user.Email = update.Email;
        //    _userRepository.Save(user);
        //}

        //public void ChangePassword(int id, PasswordUpdate update)
        //{
        //    if (update.NewPassword != update.ConfirmPassword)
        //    {
        //        throw new ArgumentException("Mismatch of new password and confirm password");
        //    }

        //    var user = _userRepository.Get(id);
        //    var hashNewPassword = _passwordService.CreatePasswordHash(update.NewPassword);
        //    user.ChangePassword(user.Password, hashNewPassword);
        //    _userRepository.Save(user);
        //}

        public bool CheckUser(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);
            if (user == null)
            {
                return false;
            }

            return CheckPassword(password, user.Password);
        }

        public bool CheckPassword(string password, string passwordHash)
        {
            return Hash(password) == passwordHash;
        }

        public string CreatePasswordHash(string password)
        {
            return Hash(password);
        }


        private string Hash(string password)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(password + "5baa61e4"));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
