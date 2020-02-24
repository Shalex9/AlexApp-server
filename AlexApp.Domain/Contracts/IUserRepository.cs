using AlexApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexApp.Domain.Contracts
{
    public interface IUserRepository
    {
        User Get(int id);
        User GetByUsername(string username);
    }
}
