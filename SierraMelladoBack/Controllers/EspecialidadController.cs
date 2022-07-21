using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SierraMelladoBack.Models;

namespace SierraMelladoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        private readonly SierraMelladoDBContext context;
        public IWebHostEnvironment Environment;
        public EspecialidadController(SierraMelladoDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.Environment = webHostEnvironment;
        }

        [HttpPost("createEspecialidad")]
        public async Task<ActionResult<Object>> CreateEspecialidad(Especialidad especialidad)
        {
            try
            {
                context.Especialidads.Add(especialidad);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Especialidad creado correctamente",
                    data = especialidad,
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

        [HttpGet("getEspecialidades")]
        public async Task<ActionResult<Object>> GetEspecialidades()
        {
            try
            {
                var especialidades = await context.Especialidads.ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Lista de especialidades",
                    data = especialidades,
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
