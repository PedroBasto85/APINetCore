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
    public class ProfesorController : Controller
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
                    IEnumerable<Profesor> registros;
                    registros = db.Profesores.ToList();
                    respuesta.messageID = 1;
                    respuesta.message = "Succeeded";
                    respuesta.data = registros;
                }
            }
            catch (Exception ex)
            {
                respuesta.message = "Failed: " + ex.Message;
            }
            return Ok(respuesta);

        }

        [HttpGet("{ProfesorID}")]
        public IActionResult GetProfesor(int ProfesorID)
        {
            dataResponse respuesta = new dataResponse();
            respuesta.messageID = 0;
            try
            {
                using (ColegioDbContext db = new ColegioDbContext())
                {
                    Profesor registros = db.Profesores.Find(ProfesorID);
                    if (registros != null)
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
        public IActionResult AddProfesor(profesorRequest oRequest)
        {
            dataResponse respuesta = new dataResponse();
            respuesta.messageID = 0;
            try
            {
                using (ColegioDbContext db = new ColegioDbContext())
                {
                    Profesor profesor = new Profesor();
                    profesor.Nombre = oRequest.Nombre;
                    profesor.Apellidos = oRequest.Apellidos;
                    profesor.Genero = oRequest.Genero;                    
                    profesor.UM = DateTime.Now;
                    db.Add(profesor);
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
        public IActionResult UpdateProfesor(profesorRequest oRequest)
        {
            dataResponse respuesta = new dataResponse();
            respuesta.messageID = 0;
            try
            {
                using (ColegioDbContext db = new ColegioDbContext())
                {
                    Profesor profesor = db.Profesores.Find(oRequest.ProfesorID);
                    if (profesor != null)
                    {
                        profesor.ProfesorID = oRequest.ProfesorID;
                        profesor.Nombre = oRequest.Nombre;
                        profesor.Apellidos = oRequest.Apellidos;
                        profesor.Genero = oRequest.Genero;                        
                        profesor.UM = DateTime.Now;
                        db.Update(profesor);
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

        [HttpDelete("{ProfesorID}")]
        public IActionResult DeleteProfesor(int ProfesorID)
        {
            dataResponse respuesta = new dataResponse();
            respuesta.messageID = 0;
            try
            {
                using (ColegioDbContext db = new ColegioDbContext())
                {
                    Profesor profesor = db.Profesores.Find(ProfesorID);
                        
                    if (profesor != null)
                    {
                        db.Remove(profesor);
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

