using System;
using System.Collections.Generic;

namespace ExamenFinal_SistemaVotos.Models;

public partial class Voto
{
    public int VotoId { get; set; }

    public int? Voto1 { get; set; }

    public int CandidatoId { get; set; }

    public int VotanteId { get; set; }

    public string FechaHora { get; set; } = null!;

    public string Ip { get; set; } = null!;

    public virtual Candidato Candidato { get; set; } = null!;

    public virtual User Votante { get; set; } = null!;
}
