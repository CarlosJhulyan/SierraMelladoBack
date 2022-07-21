using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SierraMelladoBack.Models;

namespace SierraMelladoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly SierraMelladoDBContext context;
        public IWebHostEnvironment Environment;

        public PacienteController(SierraMelladoDBContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.Environment = environment;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Object>> CreatePaciente([FromForm] Paciente paciente)
        {
            try
            {
                var pacienteLost = await context.Pacientes.FirstOrDefaultAsync(x => x.Dni == paciente.Dni);

                if (pacienteLost != null)
                {
                    return Ok(new
                    {
                        success = false,
                        message = "El DNI ya existen",
                    });
                }

                var files = HttpContext.Request.Form.Files;
                if (files.LongCount() > 0)
                {
                    var filePath = await UploadImage(files[0]);
                    paciente.Avatar = filePath.Value;
                }

                paciente.Estado = 1;
                context.Pacientes.Add(paciente);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Paciente registrado correctamente",
                    data = paciente
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

        [HttpPatch("changeAvatar")]
        public async Task<ActionResult<Object>> ChangeAvatar([FromForm] Paciente paciente)
        {
            try
            {
                var pacienteFound = await context.Pacientes.FirstOrDefaultAsync(x => x.IdPaciente == paciente.IdPaciente);

                if (pacienteFound == null) return Ok(new
                {
                    success = false,
                    message = "No se encontró al paciente"
                });

                var files = HttpContext.Request.Form.Files;
                if (files.LongCount() > 0)
                {
                    var filePath = await UploadImage(files[0]);
                    pacienteFound.Avatar = filePath.Value;

                    context.Pacientes.Update(pacienteFound);
                    await context.SaveChangesAsync();

                    return Ok(new
                    {
                        success = true,
                        message = "Foto actualizada",
                        data = pacienteFound.Avatar
                    });
                }
                else return Ok(new
                {
                    success = false,
                    message = "No se encontró el archivo"
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

        private async Task<ActionResult<String>> UploadImage(IFormFile file)
        {
            FileInfo fi = new FileInfo(file.FileName);
            var newFileName = "avatar_" + System.Guid.NewGuid() + fi.Extension;
            var path = Path.Combine(Environment.ContentRootPath, "wwwroot/images");
            using (var stream = new FileStream(Path.Combine(path, newFileName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
                return Path.Combine("images", newFileName);
            }
        }
    }
}
