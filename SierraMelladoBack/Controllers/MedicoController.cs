using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SierraMelladoBack.Helpers;
using SierraMelladoBack.Models;

namespace SierraMelladoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly SierraMelladoDBContext context;
        public IWebHostEnvironment Environment;
        public MedicoController(SierraMelladoDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.Environment = webHostEnvironment;
        }

        [HttpPost("createHorarioMedico")]
        public async Task<ActionResult<Object>> CreateHorarioMedico(Horario horario)
        {
            try
            {
                var horarioExiste = await context.Horarios.FirstOrDefaultAsync(x => x.HoraInicio == horario.HoraInicio && x.Dia == horario.Dia);

                if (horarioExiste == null)
                {
                    context.Horarios.Add(horario);
                    await context.SaveChangesAsync();

                    return Ok(new
                    {
                        success = true,
                        message = "Horario de médico creado correctamente",
                        data = horario
                    });
                }

                return Ok(new
                {
                    success = false,
                    message = "Este horario está ocupado",
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

        [HttpDelete("deleteMedico/{idMedico}")]
        public async Task<ActionResult<Object>> DeleteMedico(int idMedico)
        {
            try
            {
                var medico = await context.Medicos.FirstOrDefaultAsync(x => x.IdMedico == idMedico);

                if (medico != null)
                {
                    context.Medicos.Remove(medico);
                    await context.SaveChangesAsync();
                    var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == medico.IdUsuario);

                    context.Usuarios.Remove(usuario);
                    await context.SaveChangesAsync();

                    return Ok(new
                    {
                        success = true,
                        message = "Médico eliminado correctamente"
                    });
                } else
                {
                    return Ok(new
                    {
                        success = false,
                        message = "No se encontro al médico"
                    });
                }
            } catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message,
                });
            }
        }

        [HttpPost("createEspecialidadMedico")]
        public async Task<ActionResult<Object>> CreateEspecialidad(EspecialidadMedico especialidadMedico)
        {
            try
            {
                var especialidad = await context.Especialidads.FirstOrDefaultAsync(x => x.CodEspecialidad == especialidadMedico.CodEspecialidad);
                var medico = await context.Medicos.FirstOrDefaultAsync(x => x.IdMedico == especialidadMedico.IdMedico);

                if (especialidad != null && medico != null)
                {
                    especialidad.IdMedicos.Add(medico);
                    await context.SaveChangesAsync();

                    return Ok(new
                    {
                        success = true,
                        message = "Especialidad de médico asignado correctamente",
                    });
                } else
                {
                    return Ok(new
                    {
                        success = false,
                        message = "No se encontro el médico o la especialidad"
                    });
                }
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

        [HttpDelete("deleteEspecialidadesByMedico/{idMedico}")]
        public async Task<ActionResult<Object>> DeleteEspecialidadesByMedico(int idmedico)
        {
            try
            {
                var especialidades = await context.Especialidads.ToListAsync();

                especialidades.ForEach( async (item) =>
                {
                    //if (item.IdMedicos.FirstOrDefault(x => x.IdMedico == idmedico))
                    //{
                        //context.Especialidads.Remove(item);
                        //await context.SaveChangesAsync();
                    //}
                });

                return Ok(new
                {
                    success = true,
                    message = "Se eliminó la especialidad",
                    data = especialidades
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
        public async Task<ActionResult<Object>> ChangeAvatar([FromForm] Medico medico)
        {
            try
            {
                var medicoFound = await context.Medicos.FirstOrDefaultAsync(x => x.IdMedico == medico.IdMedico);

                if (medicoFound == null) return Ok(new
                {
                    success = false,
                    message = "No se encontró el médico"
                });

                var files = HttpContext.Request.Form.Files;
                if (files.LongCount() > 0)
                {
                    var filePath = await UploadImage(files[0]);
                    medicoFound.Avatar = filePath.Value;

                    context.Medicos.Update(medicoFound);
                    await context.SaveChangesAsync();

                    return Ok(new
                    {
                        success = true,
                        message = "Foto actualizada",
                        data = medicoFound.Avatar
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

        [HttpPut("updateMedico")]

        public async Task<ActionResult<Object>> UpdateMedico(Medico medico)
        {
            try
            {
                var medicoFound = await context.Medicos.FirstOrDefaultAsync(x => x.IdMedico == medico.IdMedico);

                if (medicoFound == null) return Ok(new
                {
                    success = false,
                    message = "No se encontró al médico"
                });

                medicoFound.Celular = medico.Celular;
                medicoFound.CodColegiado = medico.CodColegiado;
                medicoFound.FechaNac = medico.FechaNac;
                medicoFound.Dni = medico.Dni;

                context.Medicos.Update(medicoFound);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Se actualizó los datos del médico",
                    data = medicoFound
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
