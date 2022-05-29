using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIS_data.Entity;
using Microsoft.EntityFrameworkCore;

namespace DIS_data.Repository
{
    public interface IHistoryRepository
    {
        public Task<HistoryTransactionEntity> Get(string login, DateTime time);
        public Task<HistoryTransactionEntity> Get(Guid id);

        public Task<List<HistoryTransactionEntity>> GetList(string login);

        public Task<List<HistoryTransactionEntity>> GetList(DateTime time);

        public Task<List<HistoryTransactionEntity>> GetList();

        public Task<HistoryTransactionEntity> Create(HistoryTransactionEntity user);
    }
    public class HistoryRepository: IHistoryRepository
    {

        protected readonly DbContext DbContext;

        public HistoryRepository(DisContext disContext)
        {
            DbContext = disContext;
        }


        public async Task<HistoryTransactionEntity> Get(string login, DateTime time)
        {

            var result = DbContext.Set<HistoryTransactionEntity>().Select(h => h)
                .Where(h => h.UserLogin == login && h.TransactionTime == time).ToList();
            if (result.Count < 1) return null;

            return result[0];
        }

        public async Task<HistoryTransactionEntity> Get(Guid id)
        {
            var keys = new object[] { id };
            return await DbContext.Set<HistoryTransactionEntity>().FindAsync(keys);
        }

        public async Task<List<HistoryTransactionEntity>> GetList(string login)
        {
            var result = DbContext.Set<HistoryTransactionEntity>().Select(h => h)
                .Where(h => h.UserLogin == login).ToList();
            if (result.Count < 1) return null;

            return result;
        }

        public async Task<List<HistoryTransactionEntity>> GetList(DateTime time)
        {
            var result = DbContext.Set<HistoryTransactionEntity>().Select(h => h)
                .Where(h=> h.TransactionTime == time).ToList();
            if (result.Count < 1) return null;
            return result;
        }

        public async Task<List<HistoryTransactionEntity>> GetList()
        {
            return await DbContext.Set<HistoryTransactionEntity>().ToListAsync();
        }

        public async Task<HistoryTransactionEntity> Create(HistoryTransactionEntity transaction)
        {
            await DbContext.Set<HistoryTransactionEntity>().AddAsync(transaction);
            await DbContext.SaveChangesAsync();
            return transaction;
        }
    }
}
