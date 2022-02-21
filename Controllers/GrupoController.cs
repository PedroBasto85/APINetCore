using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIColegio.Models;
using APIColegio.Models.request;
using APIColegio.Models.response;


namespace APIColegio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            dataResponse respuesta = new dataResponse();
            respuesta.messageID = 0;
            try
            {
                using (ColegioDbContext db = new ColegioDbContext())
                {
                    var registros = (from grupo in db.Grupos
                                     join alumno in db.Alumnos
                                     on grupo.AlumnoID equals alumno.AlumnoID
                                     join grado in db.Grados
                                     on grupo.GradoID equals grado.GradoID 
                                     select new
                                     {
                                         GrupoID = grupo.GrupoID,
                                         Alumno = alumno.Nombre + ' ' + alumno.Apellidos,
                                         Grado = grado.Nombre,
                                         Seccion = grupo.Seccion
                                     }
                                   ).ToList();
                    if (registros.Count > 0)
                    {
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
            }
            catch (Exception ex)
            {
                respuesta.message = "Failed: " + ex.Message;
            }
            return Ok(respuesta);

        }

        [HttpGet("{GrupoID}")]
        public IActionResult GetGrupo(int GrupoID)
        {
            dataResponse respuesta = new dataResponse();
            respuesta.messageID = 0;
            try
            {
                using (ColegioDbContext db = new ColegioDbContext())
                {
                    var registros = (from grupo in db.Grupos
                                     join alumno in db.Alumnos
                                     on grupo.AlumnoID equals alumno.AlumnoID
                                     join grado in db.Grados
                                     on grupo.GradoID equals grado.GradoID
                                     where grupo.GrupoID == GrupoID
                                     select new
                                     {
                                         GrupoID = grupo.GrupoID,
                                         Alumno = alumno.Nombre + ' ' + alumno.Apellidos,
                                         Grado = grado.Nombre,
                                         Seccion = grupo.Seccion
                                     }
                                   ).ToList();
                    if (registros.Count > 0)
                    {
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
            }
            catch (Exception ex)
            {
                respuesta.message = "Failed: " + ex.Message;
            }
            return Ok(respuesta);
        }

        [HttpPost]
        public IActionResult AddGrupo(grupoRequest oRequest)
        {
            dataResponse respuesta = new dataResponse();
            respuesta.messageID = 0;
            try
            {
                using (ColegioDbContext db = new ColegioDbContext())
                {
                    AlumnoGrado grupo = new AlumnoGrado();
                    grupo.AlumnoID = oRequest.AlumnoID;
                    grupo.GradoID = oRequest.GradoID;
                    grupo.Seccion = oRequest.Seccion;                    
                    db.Add(grupo);
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
        public IActionResult UpdateGrupo(grupoRequest oRequest)
        {
            dataResponse respuesta = new dataResponse();
            respuesta.messageID = 0;
            try
            {
                using (ColegioDbContext db = new ColegioDbContext())
                {
                    AlumnoGrado grupo = db.Grupos.Find(oRequest.GrupoID);
                    if (grupo != null)
                    {
                        grupo.AlumnoID = oRequest.AlumnoID;
                        grupo.GradoID = oRequest.GradoID;
                        grupo.Seccion = oRequest.Seccion;
                        db.Update(grupo);
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

        [HttpDelete("{GrupoID}")]
        public IActionResult DeleteGrupo(int GrupoID)
        {
            dataResponse respuesta = new dataResponse();
            respuesta.messageID = 0;
            try
            {
                using (ColegioDbContext db = new ColegioDbContext())
                {
                    AlumnoGrado grupo = db.Grupos.Find(GrupoID);

                    if (grupo != null)
                    {
                        db.Remove(grupo);
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

