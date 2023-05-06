using System;
using System.Collections.Generic;

namespace ExamenFinal_SistemaVotos.Models;

public partial class User
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Voto> Votos { get; set; } = new List<Voto>();
}
