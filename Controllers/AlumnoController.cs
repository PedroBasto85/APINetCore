using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIColegio.Models;
using APIColegio.Models.request;
using APIColegio.Models.response;
using Microsoft.AspNetCore.Cors;

namespace APIColegio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class AlumnoController : Controller
    {
        
        [HttpGet]
        public IActionResult Get()
        {
            dataResponse respuesta = new dataResponse();
            respuesta.messageID = 0;
            try {
                using (ColegioDbContext db = new ColegioDbContext())
                {
                    IEnumerable<Alumno> registros;
                    registros = db.Alumnos.ToList();                    
                    respuesta.messageID = 1;
                    respuesta.message = "Succeeded";
                    respuesta.data = registros;
                }
            } catch (Exception ex)
            {
                respuesta.message = "Failed: " + ex.Message;
            }
            return Ok(respuesta);

        }
        [HttpGet("{AlumnoID}")]
        public IActionResult GetAlumno(int AlumnoID)
        {
            dataResponse respuesta = new dataResponse();
            respuesta.messageID = 0;
            try
            {
                using (ColegioDbContext db = new ColegioDbContext())
                {
                    Alumno registros = db.Alumnos.Find(AlumnoID);
                    if (registros != null) {
                        respuesta.messageID = 1;
                        respuesta.message = "Succeeded";
                        respuesta.data = registros;
                    }
                    else
                    {
                        respuesta.messageID = 0;
                        respuesta.message = "No Data";
                    }

                }
            } catch (Exception ex)
            {
                respuesta.message = "Failed: " + ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpPost]
        public IActionResult AddAlumno(alumnoRequest oRequest)
        {
            dataResponse respuesta = new dataResponse();
            respuesta.messageID = 0;
            try
            {
                using (ColegioDbContext db = new ColegioDbContext())
                {
                    Alumno alumno = new Alumno();
                    alumno.Nombre = oRequest.Nombre;
                    alumno.Apellidos = oRequest.Apellidos;
                    alumno.Genero = oRequest.Genero;
                    alumno.FechaNac = oRequest.FechaNac;
                    alumno.UM = DateTime.Now;
                    db.Add(alumno);
                    db.SaveChanges();
                    respuesta.messageID = 1;
                    respuesta.message = "succeeded";
                }
            }
            catch (Exception ex)
            {
                respuesta.message = "Failed: " + ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpPut]
        public IActionResult UpdateAlumno(alumnoRequest oRequest)
        {
            dataResponse respuesta = new dataResponse();
            respuesta.messageID = 0;
            try
            {
                using (ColegioDbContext db = new ColegioDbContext())
                {
                    Alumno alumno = db.Alumnos.Find(oRequest.AlumnoID);
                    if (alumno != null)
                    {
                        alumno.AlumnoID = oRequest.AlumnoID;
                        alumno.Nombre = oRequest.Nombre;
                        alumno.Apellidos = oRequest.Apellidos;
                        alumno.Genero = oRequest.Genero;
                        alumno.FechaNac = oRequest.FechaNac;
                        alumno.UM = DateTime.Now;
                        db.Update(alumno);
                        db.SaveChanges();
                        respuesta.messageID = 1;
                        respuesta.message = "succeeded";
                    }
                    else
                    {
                        respuesta.messageID = 0;
                        respuesta.message = "Failed: No Data With That ID";
                    }

                }
            }
            catch (Exception ex)
            {
                respuesta.message = "Failed: " + ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpDelete("{AlumnoID}")]
        public IActionResult DeleteAlumno(int AlumnoID)
        {
            dataResponse respuesta = new dataResponse();
            respuesta.messageID = 0;
            try
            {
                using (ColegioDbContext db = new ColegioDbContext())
                {
                    Alumno alumno = db.Alumnos.Find(AlumnoID);
                    if (alumno != null)
                    {
                        db.Remove(alumno);
                        db.SaveChanges();
                        respuesta.messageID = 1;
                        respuesta.message = "Succeeded";
                        
                    }
                    else
                    {
                        respuesta.messageID = 0;
                        respuesta.message = "Delete Not Found";
                    }

                }
            }
            catch (Exception ex)
            {
                respuesta.message = "Failed: " + ex.Message;
            }
            return Ok(respuesta);
        }

    }
}
