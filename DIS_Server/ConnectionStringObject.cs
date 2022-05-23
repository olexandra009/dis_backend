using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIS_Server
{
    public class ConnectionStringObject
    {
        public string Host;
        public string Port;
        public string Database;
        public string UserId;
        public string Password;


        public ConnectionStringObject(string herokuDatabaseVariable)
        {
            var unsleshed = herokuDatabaseVariable.Split("/");
            Database = unsleshed[3];
            var removeTwoDots = unsleshed[2].Split(":");
            UserId = removeTwoDots[0];
            Port = removeTwoDots[2];
            var passAndHost = removeTwoDots[1].Split("@");
            Password = passAndHost[0];
            Host = passAndHost[1];

        }
    }
}
