using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public int PersonId { get; set; }

        public string extention{ get; set; } = null!;
        public string OfficeCode { get; set; } = null!;

        public int ManagerCode { get; set; }

        // public string Position { get; set; }


    }
}