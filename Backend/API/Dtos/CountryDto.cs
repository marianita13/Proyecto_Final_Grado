using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos
{
    public class CountryDto
    {
        public string CountryName { get; set; } = null!;

    public virtual ICollection<State> States { get; set; } = new List<State>();
    }
}