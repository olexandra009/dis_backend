using AutoMapper;
using DIS_data.Entity;
using DIS_Server.DTO;
using DIS_Server.Models;

namespace DIS_Server.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<User, UserEntity>();
            CreateMap<UserEntity, User>();


            CreateMap<HistoryDto, History>();
            CreateMap<History, HistoryDto>();
            CreateMap<History, HistoryTransactionEntity>();
            CreateMap<HistoryTransactionEntity, History>();
        }
    }
}
