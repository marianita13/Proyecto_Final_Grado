using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace API.Dtos
{
    public class CityDto
    {
    public string CityName { get; set; } = null!;

    public int? StateId { get; set; }

    public virtual ICollection<PostalCode> PostalCodes { get; set; } = new List<PostalCode>();
    }
}