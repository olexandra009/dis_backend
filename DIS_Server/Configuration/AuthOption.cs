using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace DIS_Server.Configuration
{
    public class AuthOption
    {
        public const string ISSUER = "DIS_Server";
        public const string AUDIENCE = "DIS_Server";
        const string KEY = "dis_server_super_secret_KEY_345112";
        public const int LIFETIME = 140;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
