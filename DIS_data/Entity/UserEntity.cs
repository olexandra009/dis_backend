using System.Collections.Generic;

namespace DIS_data.Entity
{
    public class UserEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsUserConfitmed { get; set; }

        public List<HistoryTransactionEntity> Transactions { get; set; }
    }
}
