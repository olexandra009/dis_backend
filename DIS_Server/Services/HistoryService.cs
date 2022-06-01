using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DIS_data.Entity;
using DIS_data.Repository;
using DIS_Server.Models;

namespace DIS_Server.Services
{
    public interface IHistoryService
    {

        public Task<History> Create(History history);
        public Task<History> Get(string login, DateTime time);
        public Task<History> Get(Guid id);

        public Task<List<History>> GetList(string login);

        public Task<List<History>> GetList(DateTime time);

        public Task<List<History>> GetList();
    }
    public class HistoryService: IHistoryService
    {
        protected IHistoryRepository Repository;
        protected IMapper Mapper;
        public HistoryService(IHistoryRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public async Task<History> Create(History history)
        {
            history.TransactionId = Guid.NewGuid();
            history.TransactionTime = DateTime.Now;
            var result = await Repository.Create(Mapper.Map<HistoryTransactionEntity>(history));
            return history;
        }

        public async Task<History> Get(string login, DateTime time)
        {
            var entity = await Repository.Get(login, time);
            return Mapper.Map<History>(entity);
        }

        public async Task<History> Get(Guid id)
        {
            var entity = await Repository.Get(id);
            return Mapper.Map<History>(entity);
        }

        public async Task<List<History>> GetList(string login)
        {
            var entities = await Repository.GetList(login);
            return Mapper.Map<List<History>>(entities);
        }

        public async Task<List<History>> GetList(DateTime time)
        {
            var entities = await Repository.GetList(time);
            return Mapper.Map<List<History>>(entities);
        }

        public async Task<List<History>> GetList()
        {
            var entities = await Repository.GetList();
            return Mapper.Map<List<History>>(entities);
        }
    }
}
