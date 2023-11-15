<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
    }
=======
﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Employee
{
    public int EmployeeCode { get; set; }

    public int PersonId { get; set; }

    public string OfficeCode { get; set; } = null!;

    public int ManagerCode { get; set; }

    public string Position { get; set; }

    public virtual Office OfficeCodeNavigation { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
>>>>>>> origin/Dev_duban
}
