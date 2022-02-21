using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIColegio.Models
{
    public class Alumno
    {
       [Key]
       public int AlumnoID { get; set; }       
       public string Nombre { get; set; }
       public string Apellidos { get; set; }
       public string Genero { get; set; }
       public DateTime FechaNac { get; set; }        
       public DateTime UM { get; set; } 

    }
}
