using AlexApp.Application.Dto;
using AlexApp.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexApp.Application.Services.Contracts
{
    public interface IUserService
    {
        UserDto Get(int id);
        UserDto Get(string username);
        PageInfo<UserDto> GetRange(int page, int pageSize, UserFilter filter);
        //void Update(int id, UserUpdate update);
        //void ChangePassword(int id, PasswordUpdate update);
        bool CheckUser(string username, string password);
        bool UsernameFree(string username);
        void RegisterNewUser(string username, string title, string password, string email);
    }
}
