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
    public class GradoController : Controller
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
                    var registros = (from grado in db.Grados
                                   join prof in db.Profesores
                                   on grado.ProfesorID equals prof.ProfesorID
                                   select new
                                   {
                                       GradoID = grado.GradoID,
                                       NombreGrado = grado.Nombre,
                                       NombreProfesor = prof.Nombre + ' ' + prof.Apellidos
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

        [HttpGet("{GradoID}")]
        public IActionResult GetGrado(int GradoID)
        {
            dataResponse respuesta = new dataResponse();
            respuesta.messageID = 0;
            try
            {
                using (ColegioDbContext db = new ColegioDbContext())
                {
                    var registros = (from grado in db.Grados
                                     join prof in db.Profesores
                                     on grado.ProfesorID equals prof.ProfesorID
                                     where grado.GradoID == GradoID
                                     select new
                                     {
                                         GradoID = grado.GradoID,
                                         NombreGrado = grado.Nombre,
                                         NombreProfesor = prof.Nombre + ' ' + prof.Apellidos
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
        public IActionResult AddGrado(gradoRequest oRequest)
        {
            dataResponse respuesta = new dataResponse();
            respuesta.messageID = 0;
            try
            {
                using (ColegioDbContext db = new ColegioDbContext())
                {
                    Grado grado = new Grado();
                    grado.Nombre = oRequest.Nombre;
                    grado.ProfesorID = oRequest.ProfesorID;
                    grado.UM = DateTime.Now;
                    db.Add(grado);
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
        public IActionResult UpdateGrado(gradoRequest oRequest)
        {
            dataResponse respuesta = new dataResponse();
            respuesta.messageID = 0;
            try
            {
                using (ColegioDbContext db = new ColegioDbContext())
                {
                    Grado grado = db.Grados.Find(oRequest.GradoID);
                    if (grado != null)
                    {
                        grado.Nombre = oRequest.Nombre;
                        grado.ProfesorID = oRequest.ProfesorID;
                        grado.UM = DateTime.Now;
                        db.Update(grado);
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

        [HttpDelete("{GradoID}")]
        public IActionResult DeleteGrado(int GradoID)
        {
            dataResponse respuesta = new dataResponse();
            respuesta.messageID = 0;
            try
            {
                using (ColegioDbContext db = new ColegioDbContext())
                {
                    Grado grado = db.Grados.Find(GradoID);

                    if (grado != null)
                    {
                        db.Remove(grado);
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
