using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_SVotos
{
    public class Candidato
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Partido { get; set; } = null!;

        public int CantidadPiñones { get; set; }
    }
}
