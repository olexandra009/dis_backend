using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIS_Server.DTO
{
    public class HistoryDto
    {
        public Guid TransactionId { get; set; }

        public string UserLogin { get; set; }
        public DateTime TransactionTime { get; set; }
        public string TransactionType { get; set; }
        public double Amount { get; set; }
        public string MoneyType { get; set; }

    }
}
