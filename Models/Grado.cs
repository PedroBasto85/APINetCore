using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIColegio.Models
{
    public class Grado
    {
        public int GradoID { get; set; }
        public string Nombre { get; set; }
        public int ProfesorID { get; set; }
        public DateTime UM { get; set; }
    }
}
