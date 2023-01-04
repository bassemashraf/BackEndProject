using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.models;

namespace BackEnd.Dtos.User
{
    public class AddUserDto
    {
         public string  UserName { get; set; } = string.Empty;
        public string Password { get; set; } =string.Empty;
       
    }
}