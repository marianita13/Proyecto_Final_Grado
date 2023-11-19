using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos
{
    public class PostalCodeDto
    {
        public int Id { get; set; }
        public string PostalCode1 { get; set; } = null!;

        public int CityId { get; set; }


        public virtual ICollection<Office> Offices { get; set; } = new List<Office>();

        public virtual ICollection<Person> People { get; set; } = new List<Person>();
    }
}