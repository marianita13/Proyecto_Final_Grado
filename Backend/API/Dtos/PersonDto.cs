using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; } = null!;

        public string LastName1 { get; set; } = null!;

        public string LastName2 { get; set; }


        public string Email { get; set; } = null!;

        public int PersonTypeId { get; set; }




    }
}