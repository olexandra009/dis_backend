using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIS_data.Entity;

namespace DIS_data.Repository
{
    public interface IUserRepository
    {
        public Task<UserEntity> Get(string login);
        public Task<UserEntity> Create(UserEntity user);
    }
    public class UserRepository:IUserRepository
    {
        //todo change to database connection
        public async Task<UserEntity> Get(string login)
        {
            return new UserEntity() {Login = "ababa", Password = "qwerty", Role = "user,admin"};
        }

        public async Task<UserEntity> Create(UserEntity user)
        {
            return user;
        }
    }
}
