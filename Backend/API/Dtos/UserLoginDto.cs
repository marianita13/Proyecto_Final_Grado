using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class UserLoginDto
    {
        public required string email {get; set;}
        public required string password {get; set;}
    }
}