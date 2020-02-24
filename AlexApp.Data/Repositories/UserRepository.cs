using AutoMapper;
using AlexApp.Data.Models;
using AlexApp.Domain.Contracts;
using AlexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlexApp.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AlexAppDbContext _db;
        private readonly DbSet<UserEF> _set;

        public UserRepository(AlexAppDbContext db)
        {
            _db = db;
            _set = _db.Set<UserEF>();
        }

        public User Get(int id)
        {
            return Mapper.Map<User>(_set.Find(id));
        }

        public User GetByUsername(string username)
        {
            var userEF = _set.FirstOrDefault(u => u.Title == username);
            if (userEF == null)
            {
                return null;
            }
            return Mapper.Map<User>(userEF);
        }
    }
}
