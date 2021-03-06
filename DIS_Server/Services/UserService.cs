using System.Threading.Tasks;
using AutoMapper;
using DIS_data.Entity;
using DIS_data.Repository;
using DIS_Server.Models;

namespace DIS_Server.Services
{
    public interface IUserService
    {
        public Task<User> Get(string login);
        public Task<User> Get(string login, string password = null, bool hashed = false);
        public Task<User> Create(User user);
        public void Update(User user);

    }
    public class UserService : IUserService
    {
        protected IMapper Mapper;
        protected IUserRepository Repository;
        public UserService(IMapper mapper, IUserRepository repository)
        {
            Mapper = mapper;
            Repository = repository;
        }

        public void Update(User user)
        {
            Repository.Update(Mapper.Map<UserEntity>(user));
        }

        public async Task<User> Get(string login, string password, bool hashed = false)
        {
            var entity = await Repository.Get(login);
            if (entity.Password == password)
            {
                return Mapper.Map<User>(entity);
            }
            return null;
        }

        public async Task<User> Get(string login)
        {
            var entity = await Repository.Get(login);

            return Mapper.Map<User>(entity);
        }

        public async Task<User> Create(User user)
        {
            var existed = await Repository.Get(user.Login);
            if (existed != null) return null;
            var userEntity = Mapper.Map<UserEntity>(user);
            var entity = await Repository.Create(userEntity);
            return Mapper.Map<User>(entity);
        }
    }
}
