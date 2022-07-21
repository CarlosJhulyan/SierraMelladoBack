using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SierraMelladoBack.Models;

namespace SierraMelladoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly SierraMelladoDBContext context;
        public IWebHostEnvironment Environment;
        public ServicioController(SierraMelladoDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.Environment = webHostEnvironment;
        }

        [HttpGet("getServicios")]
        public async Task<ActionResult<Object>> GetServicios()
        {
            try
            {
                var servicios = await context.Servicios.ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Lista de servicios",
                    data = servicios
                });
            } catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost("createServicio")]
        public async Task<ActionResult<Object>> CreateServicio(Servicio servicio)
        {
            try
            {
                context.Servicios.Add(servicio);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Servicio creado correctamente",
                    data = servicio
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpDelete("deleteServicio/{idServicio}")]
        public async Task<ActionResult<Object>> DeleteServicio(int idServicio)
        {
            try
            {
                var servicio = await context.Servicios.FirstOrDefaultAsync(x => x.IdServicio == idServicio);

                if (servicio == null) return Ok(new
                {
                    success = false,
                    message = "No se encontro ningun servicio"
                });

                context.Servicios.Remove(servicio);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Servicio eliminado correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
