using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos
{
    public class StateDto
    {
        public int Id { get; set; }
        public string StateName { get; set; } = null!;

        public int CountryId { get; set; }

        public virtual ICollection<City> Cities { get; set; } = new List<City>();

    }
}