using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Dtos.User
{
    public class UserLoginDTO
    {
        public string  UserName { get; set; } = string.Empty;
        public string Password { get; set; } =string.Empty;
    }
}