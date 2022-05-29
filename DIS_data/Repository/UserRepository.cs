using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIS_data.Entity;
using Microsoft.EntityFrameworkCore;

namespace DIS_data.Repository
{
    public interface IUserRepository
    {
        public Task<UserEntity> Get(string login);
        public Task<UserEntity> Create(UserEntity user);
        public void Update(UserEntity user);
    }

    public class UserRepository:IUserRepository
    {
        protected readonly DbContext DbContext;

        public UserRepository(DisContext disContext)
        {
            DbContext = disContext;
        }

        public async void Update(UserEntity user)
        {
            DbContext.Attach<UserEntity>(user);
            DbContext.Entry(user).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        public async Task<UserEntity> Get(string login)
        {
            var keys = new object[] { login };
            return await DbContext.Set<UserEntity>().FindAsync(keys);
            // return new UserEntity() {Login = "ababa", Password = "qwerty", Role = "user,admin"};
        }

        public async Task<UserEntity> Create(UserEntity user)
        {
            DbContext.Set<UserEntity>().Add(user);
            await DbContext.SaveChangesAsync();
            return user;
        }
    }
}
