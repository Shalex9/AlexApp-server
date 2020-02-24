using AlexApp.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexApp.Application.Services.Contracts
{
    public interface IUserService
    {
        UserDto Get(int id);
        UserDto Get(string username);
        //void Update(int id, UserUpdate update);
        //void ChangePassword(int id, PasswordUpdate update);
        bool CheckUser(string username, string password);
    }
}
