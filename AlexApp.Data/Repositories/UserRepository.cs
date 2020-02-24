using AutoMapper;
using AlexApp.Data.Models;
using AlexApp.Domain.Contracts;
using AlexApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlexApp.Domain.Filters;
using LinqKit;

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

        public (IEnumerable<User> items, int count) GetRange(int page, int pageSize, UserFilter filter)
        {
            var filterBuilder = PredicateBuilder.New<UserEF>(true);
            filterBuilder = filterBuilder.And(u => !u.IsRemoved);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Login))
                {
                    filterBuilder = filterBuilder.And(u => u.Name.ToUpper().Contains(filter.Login.ToUpper()));
                }
            }

            var (dbItems, count) = RepositoryUtils.GetFilteredRange(_set, page, pageSize, filterBuilder);
            var items = dbItems.Select(Mapper.Map<User>).ToList();
            return (items, count);
        }
    }
}
