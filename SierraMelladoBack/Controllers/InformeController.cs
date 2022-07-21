using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SierraMelladoBack.Models;

namespace SierraMelladoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformeController : ControllerBase
    {
        private readonly SierraMelladoDBContext context;
        public IWebHostEnvironment Environment;
        public InformeController(SierraMelladoDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.Environment = webHostEnvironment;
        }

        [HttpGet("getInformePacienteDetails/{idPaciente}")]
        public async Task<ActionResult<Object>> GetInformePacienteDetails(int idPaciente)
        {
            try
            {
                var informe = await (from informePaciente in context.InformePacientes
                                     join paciente in context.Pacientes
                                     on informePaciente.Paciente equals paciente.IdPaciente
                                     select new
                                     {
                                         resumen = informePaciente.Resumen,
                                         numInforme = informePaciente.NumInforme,
                                         fechaEmi = informePaciente.FechaEmi,
                                         idPaciente = paciente.IdPaciente
                                     })
                                     .Where(x => x.idPaciente == idPaciente)
                                     .OrderByDescending(x => x.fechaEmi)
                                     .FirstOrDefaultAsync();

                if (informe == null) return Ok(new
                {
                    success = false,
                    message = "No se encontro informe para este paciente",
                });

                return Ok(new
                {
                    success = true,
                    message = "Detalles de informe de paciente encontrado",
                    data = informe
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

        [HttpGet("getInformesMedico")]
        public async Task<ActionResult<Object>> GetInformesMedico()
        {
            try
            {
                var informes = await (from informe in context.Informes
                                     join medico in context.Medicos
                                     on informe.IdMedico equals medico.IdMedico
                                     join usuario in context.Usuarios
                                     on medico.IdUsuario equals usuario.IdUsuario
                                     select new
                                     {
                                         key = informe.NumInforme,
                                         asunto = informe.Asunto,
                                         archivo = informe.Archivo,
                                         numInforme = informe.NumInforme,
                                         fechaEmi = informe.FechaEmi,
                                         medico = usuario.Nombres + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno
                                     })
                                     .OrderByDescending(x => x.fechaEmi)
                                     .ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Lista de informes de médicos",
                    data = informes
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

        [HttpGet("getInformePacienteByMedico/{idMedico}")]
        public async Task<ActionResult<Object>> GetInformePacienteByMedico(int idMedico)
        {
            try
            {
                var informes = await (from informe in context.InformePacientes
                                      join paciente in context.Pacientes
                                      on informe.Paciente equals paciente.IdPaciente
                                      join usuario in context.Usuarios
                                      on paciente.IdUsuario equals usuario.IdUsuario
                                      select new
                                      {
                                          idMedico = informe.Medico,
                                          key = informe.NumInforme,
                                          numInforme = informe.NumInforme,
                                          resumen = informe.Resumen,
                                          fechaEmi = informe.FechaEmi,
                                          archivo = informe.Archivo,
                                          paciente = usuario.Nombres + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno,
                                      })
                                      .Where(x => x.idMedico == idMedico)
                                      .OrderByDescending(x => x.fechaEmi)
                                      .ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Lista de informes de pacientes",
                    data = informes
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

        [HttpGet("getInformePacienteByCita/{idCita}")]
        public async Task<ActionResult<Object>> GetInformePacienteByCita(int idCita)
        {
            try
            {
                var informe = await context.InformePacientes.FirstOrDefaultAsync(x => x.Cita == idCita);

                if (informe == null) return Ok(new
                {
                    success = false,
                    message = "No hay informes de esta cita"
                });

                return Ok(new
                {
                    success = true,
                    message = "Informe de cita encontrado",
                    data = informe
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

        [HttpGet("getInformePacienteByPaciente/{idPaciente}")]
        public async Task<ActionResult<Object>> GetInformePacienteByPaciente(int idPaciente)
        {
            try
            {
                var informes = await (from informe in context.InformePacientes
                                      join paciente in context.Pacientes
                                      on informe.Paciente equals paciente.IdPaciente
                                      join usuario in context.Usuarios
                                      on paciente.IdUsuario equals usuario.IdUsuario
                                      select new
                                      {
                                          idPaciente = informe.Paciente,
                                          key = informe.NumInforme,
                                          numInforme = informe.NumInforme,
                                          resumen = informe.Resumen,
                                          fechaEmi = informe.FechaEmi,
                                          archivo = informe.Archivo,
                                          paciente = usuario.Nombres + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno,
                                      })
                                      .Where(x => x.idPaciente == idPaciente)
                                      .OrderByDescending(x => x.fechaEmi)
                                      .ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Lista de informes de pacientes",
                    data = informes
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

        [HttpGet("getInformesByMedico/{idMedico}")]
        public async Task<ActionResult<Object>> GetInformesByMedico(int idMedico)
        {
            try
            {
                var informes = await context.Informes
                    .Where(x => x.IdMedico == idMedico)
                    .OrderByDescending(x => x.FechaEmi)
                    .ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Lista de informes de médico",
                    data = informes
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

        [HttpPost("createReporteMedico")]
        public async Task<ActionResult<Object>> CreateReporteMedico([FromForm] Informe informe)
        {
            try
            {
                informe.FechaEmi = DateTime.Now;
                var files = HttpContext.Request.Form.Files;
                if (files.LongCount() > 0)
                {
                    var filePath = await UploadReport(files[0]);
                    informe.Archivo = filePath.Value;

                    context.Informes.Add(informe);
                    await context.SaveChangesAsync();

                    return Ok(new
                    {
                        success = true,
                        message = "Informe registrado correctamente",
                        data = informe
                    });
                } else
                {
                    return Ok(new
                    {
                        success = false,
                        message = "No se encontró informe adjuntado",
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

        [HttpPost("createReportePaciente")]
        public async Task<ActionResult<Object>> CreateReportePaciente([FromForm] InformePaciente informePaciente)
        {
            try
            {
                var informe = await context.InformePacientes.FirstOrDefaultAsync(x => x.Cita == informePaciente.Cita);
                if (informe != null) return Ok(new
                {
                    success = false,
                    message = "Esta cita ya tiene un informe adjuntado"
                });

                informePaciente.FechaEmi = DateTime.Now;
                var files = HttpContext.Request.Form.Files;
                if (files.LongCount() > 0)
                {
                    var filePath = await UploadReport(files[0]);
                    informePaciente.Archivo = filePath.Value;

                    context.InformePacientes.Add(informePaciente);
                    await context.SaveChangesAsync();

                    return Ok(new
                    {
                        success = true,
                        message = "Informe registrado correctamente",
                        data = informePaciente
                    });
                }
                else
                {
                    return Ok(new
                    {
                        success = false,
                        message = "No se encontró informe adjuntado",
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

        private async Task<ActionResult<String>> UploadReport(IFormFile file)
        {
            FileInfo fi = new FileInfo(file.FileName);
            var newFileName = "reporte_" + System.Guid.NewGuid() + fi.Extension;
            var path = Path.Combine(Environment.ContentRootPath, "wwwroot/files");
            using (var stream = new FileStream(Path.Combine(path, newFileName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
                return Path.Combine("files", newFileName);
            }
        }
    }
}
