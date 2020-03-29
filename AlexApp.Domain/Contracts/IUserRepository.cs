using AlexApp.Domain.Entities;
using AlexApp.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexApp.Domain.Contracts
{
    public interface IUserRepository
    {
        User Get(int id);
        User GetByUsername(string username);
        (IEnumerable<User> items, int count) GetRange(int page, int pageSize, UserFilter filter);
        void RegisterNewUser(User user);
    }
}
