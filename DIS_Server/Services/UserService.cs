using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DIS_Server.Models;

namespace DIS_Server.Services
{
    public interface IUserService
    {
        public Task<User> Get(string login, string password, bool hashed = false);
        public Task<User> Create(User user);

    }
    public class UserService: IUserService
    {
        protected IMapper Mapper;
        public UserService(IMapper mapper)
        {
            Mapper = mapper;
        }


        //todo change to connection to db
        public async Task<User> Get(string login, string password, bool hashed = false)
        {
            if (login == "ababa" && password == "qwerty")
            {
                return new User() {Login = "ababa", Password = "qwerty", Role = "user, admin"};
            }
            return null;
        }

        public async Task<User> Create(User user)
        {
            //todo change to connection to db
            return user;
        }
    }
}
