using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class Authorization1
    {

        public enum Rol
        {
            DirectorGeneral = 1,
            SubdirectorMarketing = 2,
            SubdirectorVentas = 3,
            Secretaria = 4,
            RepresentanteVentas = 5,
            DirectorOficina = 6,
            Cliente = 7,
            Employee = 8
        }

        public const Rol rol_default = Rol.Employee;

    }
}
