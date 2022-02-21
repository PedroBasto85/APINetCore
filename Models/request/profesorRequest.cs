using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIColegio.Models.request
{
    public class profesorRequest
    {
        public int ProfesorID { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Genero { get; set; }

    }
}
