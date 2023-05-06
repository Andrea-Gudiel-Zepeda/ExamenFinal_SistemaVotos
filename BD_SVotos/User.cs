using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_SVotos
{
    public class User
    {
        public int Id { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}