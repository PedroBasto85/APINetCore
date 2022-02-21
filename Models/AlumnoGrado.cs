using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIColegio.Models
{
    public class AlumnoGrado
    {
        public int GrupoID { get; set; }
        public int AlumnoID { get; set; }
        public int GradoID { get; set; }
        public string Seccion { get; set; }
    }
}
