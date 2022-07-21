using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SierraMelladoBack.Models;

namespace SierraMelladoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensajeController : ControllerBase
    {
        private readonly SierraMelladoDBContext context;
        public IWebHostEnvironment Environment;
        public MensajeController(SierraMelladoDBContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.Environment = environment;
        }

        [HttpPost("createMensaje")]
        public async Task<ActionResult<Object>> CreateMensaje(Mensaje mensaje)
        {
            try
            {
                mensaje.FechaEmi = DateTime.Now;
                mensaje.Estado = "P";

                context.Mensajes.Add(mensaje);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Se envió el mensaje satisfactoriamente",
                    data = mensaje,
                });
            } catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message,
                });
            }
        }

        [HttpPatch("changeEstado")]
        public async Task<ActionResult<Object>> ChangeEstado(ChangeEstadoSchema changeEstadoSchema)
        {
            try
            {
                var currentMensaje = context.Mensajes.FirstOrDefault(x => x.IdMensaje == changeEstadoSchema.IdMensaje);

                if (currentMensaje == null) return Ok(new
                {
                    success = false,
                    message = "No se encontró el mensaje para cambiar el estado",
                });

                currentMensaje.Estado = "A";
                currentMensaje.IdAdmin = changeEstadoSchema.IdAdmin;

                context.Mensajes.Update(currentMensaje);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "El mensaje se marcó como revisado",
                    data = currentMensaje
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message,
                });
            }
        }

        [HttpGet("getRecentMensajes")]
        public async Task<ActionResult<Object>> GetRecentMensajes()
        {
            try
            {
                var mensajes = await context.Mensajes
                    .OrderByDescending(x => x.FechaEmi)
                    .ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Lista de mensajes recientes",
                    data = mensajes
                });
            } catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message,
                });
            }
        }

        [HttpGet("getPendientMensajes")]
        public async Task<ActionResult<Object>> GetPendientMensajes()
        {
            try
            {
                var mensajes = await context.Mensajes
                    .OrderByDescending(x => x.FechaEmi)
                    .Where(x => x.Estado == "P")
                    .ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Lista de mensajes pendientes",
                    data = mensajes
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message,
                });
            }
        }
    }
}

public partial class ChangeEstadoSchema
{
    public int? IdAdmin { get; set; }
    public int? IdMensaje { get; set; }
}

