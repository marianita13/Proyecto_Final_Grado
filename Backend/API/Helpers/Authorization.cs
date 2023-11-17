using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class Authorization
    {

        public enum PersonType
        {
            DirectorGeneral = 1,
            SubdirectorMarketing = 2,
            SubdirectorVentas = 3,
            Secretaria = 4,
            RepresentanteVentas = 5,
            DirectorOficina = 6,
            Cliente = 7
        }

        public const PersonType rol_default = PersonType.Cliente;

    }
}
