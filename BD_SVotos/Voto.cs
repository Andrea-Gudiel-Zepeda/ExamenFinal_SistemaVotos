using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_SVotos
{
    public class Voto
    {
        public int VotoId { get; set; }

        public int? Voto1 { get; set; }

        public int CandidatoId { get; set; }

        public int VotanteId { get; set; } 

        public string FechaHora { get; set; } = null!;

        public string Ip { get; set; } = null!;
    }
}
