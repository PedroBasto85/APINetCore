using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIColegio.Models.request
{
    public class gradoRequest
    {
        public int GradoID { get; set; }
        public string Nombre { get; set; }
        public int ProfesorID { get; set; }
        
    }
}
