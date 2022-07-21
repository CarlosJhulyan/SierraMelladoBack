using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SierraMelladoBack.Models;

namespace SierraMelladoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase
    {
        private readonly SierraMelladoDBContext context;
        public IWebHostEnvironment Environment;
        public CitaController(SierraMelladoDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.Environment = webHostEnvironment;
        }

        [HttpGet("getCitas")]
        public async Task<ActionResult<Object>> GetCitas()
        {
            try
            {
                var citas = await (from cita in context.Cita
                                   join paciente in context.Pacientes
                                   on cita.Paciente equals paciente.IdPaciente
                                   join medico in context.Medicos
                                   on cita.Medico equals medico.IdMedico
                                   join servicio in context.Servicios
                                   on cita.Servicio equals servicio.IdServicio
                                   join orden in context.Ordens
                                   on cita.NumOrden equals orden.NumOrden
                                   join usuarioPaciente in context.Usuarios
                                   on paciente.IdUsuario equals usuarioPaciente.IdUsuario
                                   join usuarioMedico in context.Usuarios
                                   on medico.IdUsuario equals usuarioMedico.IdUsuario
                                   select new
                                   {
                                       key = cita.IdCita,
                                       idCita = cita.IdCita,
                                       numOrden = cita.NumOrden,
                                       descripcion = cita.Descripcion,
                                       fecha = cita.Fecha,
                                       hora = cita.Hora,
                                       nombresPaciente = usuarioPaciente.Nombres,
                                       apPaternoPaciente = usuarioPaciente.ApellidoPaterno,
                                       apMaternoPaciente = usuarioPaciente.ApellidoMaterno,
                                       nombresMedico = usuarioMedico.Nombres,
                                       apPaternoMedico = usuarioMedico.ApellidoPaterno,
                                       apMaternoMedico = usuarioMedico.ApellidoMaterno,
                                       servicio = servicio.Descripcion,
                                       estado = orden.Estado
                                   })
                                   .OrderByDescending(x => x.numOrden)
                                   .ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Lista de citas",
                    data = citas
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

        [HttpGet("getCitasByMedico/{idMedico}")]
        public async Task<ActionResult<Object>> GetCitasByMedico(int idMedico)
        {
            try
            {
                var citas = await (from cita in context.Cita
                                   join paciente in context.Pacientes
                                   on cita.Paciente equals paciente.IdPaciente
                                   join medico in context.Medicos
                                   on cita.Medico equals medico.IdMedico
                                   join servicio in context.Servicios
                                   on cita.Servicio equals servicio.IdServicio
                                   join orden in context.Ordens
                                   on cita.NumOrden equals orden.NumOrden
                                   join usuarioPaciente in context.Usuarios
                                   on paciente.IdUsuario equals usuarioPaciente.IdUsuario
                                   join usuarioMedico in context.Usuarios
                                   on medico.IdUsuario equals usuarioMedico.IdUsuario
                                   select new
                                   {
                                       key = cita.IdCita,
                                       idCita = cita.IdCita,
                                       numOrden = cita.NumOrden,
                                       descripcion = cita.Descripcion,
                                       fecha = cita.Fecha,
                                       hora = cita.Hora,
                                       nombresPaciente = usuarioPaciente.Nombres,
                                       apPaternoPaciente = usuarioPaciente.ApellidoPaterno,
                                       apMaternoPaciente = usuarioPaciente.ApellidoMaterno,
                                       nombresMedico = usuarioMedico.Nombres,
                                       apPaternoMedico = usuarioMedico.ApellidoPaterno,
                                       apMaternoMedico = usuarioMedico.ApellidoMaterno,
                                       servicio = servicio.Descripcion,
                                       idMedico = medico.IdMedico,
                                       estado = orden.Estado
                                   })
                                   .Where(x => x.idMedico == idMedico)
                                   .OrderByDescending(x => x.numOrden)
                                   .ToListAsync();

                if (citas.Count == 0) return Ok(new
                {
                    success = false,
                    message = "No tienes ninguna cita a tu nombre"
                });

                return Ok(new
                {
                    success = true,
                    message = "Lista de citas por médico",
                    data = citas
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

        [HttpGet("getCitasByPaciente/{idPaciente}")]
        public async Task<ActionResult<Object>> GetCitasByPaciente(int idPaciente)
        {
            try
            {
                var citas = await (from cita in context.Cita
                                   join paciente in context.Pacientes
                                   on cita.Paciente equals paciente.IdPaciente
                                   join medico in context.Medicos
                                   on cita.Medico equals medico.IdMedico
                                   join servicio in context.Servicios
                                   on cita.Servicio equals servicio.IdServicio
                                   join usuarioPaciente in context.Usuarios
                                   on paciente.IdUsuario equals usuarioPaciente.IdUsuario
                                   join usuarioMedico in context.Usuarios
                                   on medico.IdUsuario equals usuarioMedico.IdUsuario
                                   select new
                                   {
                                       key = cita.IdCita,
                                       idCita = cita.IdCita,
                                       numOrden = cita.NumOrden,
                                       descripcion = cita.Descripcion,
                                       fecha = cita.Fecha,
                                       hora = cita.Hora,
                                       nombresPaciente = usuarioPaciente.Nombres,
                                       apPaternoPaciente = usuarioPaciente.ApellidoPaterno,
                                       apMaternoPaciente = usuarioPaciente.ApellidoMaterno,
                                       nombresMedico = usuarioMedico.Nombres,
                                       apPaternoMedico = usuarioMedico.ApellidoPaterno,
                                       apMaternoMedico = usuarioMedico.ApellidoMaterno,
                                       servicio = servicio.Descripcion,
                                       idPaciente = paciente.IdPaciente
                                   })
                                   .Where(x => x.idPaciente == idPaciente)
                                   .OrderByDescending(x => x.numOrden)
                                   .ToListAsync();

                if (citas.Count == 0) return Ok(new
                {
                    success = false,
                    message = "No tienes ninguna cita a tu nombre"
                });

                return Ok(new
                {
                    success = true,
                    message = "Lista de citas por paciente",
                    data = citas
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

        [HttpGet("getCitaDetails/{idCita}")]
        public async Task<ActionResult<Object>> GetCitaDetails(int idCita)
        {
            try
            {
                var detalles = await (from cita in context.Cita
                                   join paciente in context.Pacientes
                                   on cita.Paciente equals paciente.IdPaciente
                                   join medico in context.Medicos
                                   on cita.Medico equals medico.IdMedico
                                   join servicio in context.Servicios
                                   on cita.Servicio equals servicio.IdServicio
                                   join usuarioPaciente in context.Usuarios
                                   on paciente.IdUsuario equals usuarioPaciente.IdUsuario
                                   join usuarioMedico in context.Usuarios
                                   on medico.IdUsuario equals usuarioMedico.IdUsuario
                                   join orden in context.Ordens
                                   on cita.NumOrden equals orden.NumOrden
                                   join metodo in context.MetodoPagos
                                   on orden.CodMetodo equals metodo.CodMetodo
                                      select new
                                   {
                                       key = cita.IdCita,
                                       idCita = cita.IdCita,
                                       numOrden = orden.NumOrden,
                                       descOrden = orden.Descripcion,
                                       montoTotal = orden.MontoTotal,
                                       estadoOrden = orden.Estado,
                                       descripcion = cita.Descripcion,
                                       fecha = cita.Fecha,
                                       hora = cita.Hora,
                                       nombresP = usuarioPaciente.Nombres,
                                       apPaternoP = usuarioPaciente.ApellidoPaterno,
                                       apMaternoP = usuarioPaciente.ApellidoMaterno,
                                       nombresM = usuarioMedico.Nombres,
                                       apPaternoM = usuarioMedico.ApellidoPaterno,
                                       apMaternoM = usuarioMedico.ApellidoMaterno,
                                       descServicio = servicio.Descripcion,
                                       abrServicio = servicio.Abreviatura,
                                       descMetodo = metodo.Descripcion,
                                       dniP = paciente.Dni,
                                       celularP = paciente.Celular,
                                       fechaNacP = paciente.FechaNac,
                                       dniM = medico.Dni,
                                       celularM = medico.Celular,
                                       fechaNacM = medico.FechaNac,
                                       codColegiadoM = medico.CodColegiado,
                                       correoM = usuarioMedico.Correo,
                                       correoP = usuarioPaciente.Correo,
                                      }).FirstOrDefaultAsync(x => x.idCita == idCita);

                if (detalles == null) return Ok(new
                {
                    success = false,
                    message = "No se encontro la cita"
                });

                return Ok(new
                {
                    success = true,
                    message = "Detalle de cita",
                    data = detalles
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

        [HttpPost("createCita")]
        public async Task<ActionResult<Object>> CreateCita(Citum citum)
        {
            try
            {
                context.Cita.Add(citum);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Cita generada correctamente",
                    data = citum
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

        [HttpPost("createOrden")]
        public async Task<ActionResult<Object>> CreateMedico([FromForm] Orden orden)
        {
            try
            {
                var files = HttpContext.Request.Form.Files;
                if (files.LongCount() > 0)
                {
                    var filePath = await UploadVaucher(files[0]);
                    orden.Descripcion = filePath.Value;
                    context.Ordens.Add(orden);
                    await context.SaveChangesAsync();

                    return Ok(new
                    {
                        success = true,
                        message = "Orden generado correctamete",
                        data = orden
                    });
                } else return Ok(new
                {
                    success = false,
                    message = "No se adjunto el vaucher"
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

        [HttpGet("searchCita/{numOrden}")]
        public async Task<ActionResult<Object>> SearchCita(int numOrden)
        {
            try
            {
                var cita = await context.Cita.FirstOrDefaultAsync(x => x.NumOrden == numOrden);

                if (cita != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Cita encontrada",
                        data = cita
                    });
                }
                else return Ok(new
                {
                    success = false,
                    message = "No existe una cita con este número de orden"
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

        [HttpPatch("changeEstadoOrdenPaciente")]
        public async Task<ActionResult<Object>> ChangeEstadoOrdenPaciente(ChangeEstadoOrdenSchema changeEstadoSchema)
        {
            try
            {
                var orden = await context.Ordens.FirstOrDefaultAsync(x => x.NumOrden == changeEstadoSchema.Id);

                orden.Estado = changeEstadoSchema.Estado;
                context.Ordens.Update(orden);
                await context.SaveChangesAsync();

                if (orden != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Estado de orden actualizado"
                    });
                }
                else return Ok(new
                {
                    success = false,
                    message = "No existe este registro"
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

        private async Task<ActionResult<String>> UploadVaucher(IFormFile file)
        {
            FileInfo fi = new FileInfo(file.FileName);
            var newFileName = "vaucher_" + System.Guid.NewGuid() + fi.Extension;
            var path = Path.Combine(Environment.ContentRootPath, "wwwroot/images");
            using (var stream = new FileStream(Path.Combine(path, newFileName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
                return Path.Combine("images", newFileName);
            }
        }
    }

    public partial class ChangeEstadoOrdenSchema
    {
        public int? Id { get; set; }
        public string? Estado { get; set; }
    }
}
