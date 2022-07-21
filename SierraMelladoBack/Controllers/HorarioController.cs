using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SierraMelladoBack.Models;

namespace SierraMelladoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {
        private readonly SierraMelladoDBContext context;
        public IWebHostEnvironment Environment;
        public HorarioController(SierraMelladoDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.Environment = webHostEnvironment;
        }

        [HttpGet("getHorariosByMedico/{idMedico}")]
        public async Task<ActionResult<Object>> GetHorariosByMedico(int idMedico)
        {
            try
            {
                var horarios = await (from horario in context.Horarios
                                      join medico in context.Medicos
                                      on horario.IdMedico equals medico.IdMedico
                                      join usuario in context.Usuarios
                                      on medico.IdUsuario equals usuario.IdUsuario
                                      select new
                                      {
                                          codHorario = horario.CodHorario,
                                          horaInicio = horario.HoraInicio,
                                          horaFin = horario.HoraFin,
                                          dia = horario.Dia,
                                          nombres = usuario.Nombres + " " + usuario.ApellidoPaterno,
                                          idMedico = medico.IdMedico
                                      }).Where(x => x.idMedico == idMedico).ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Lista de horarios por médico",
                    data = horarios
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

        [HttpGet("getHorariosMedicos")]
        public async Task<ActionResult<Object>> GetHorariosMedicos()
        {
            try
            {
                var horarios = await (from horario in context.Horarios
                                      join medico in context.Medicos
                                      on horario.IdMedico equals medico.IdMedico
                                      join usuario in context.Usuarios
                                      on medico.IdUsuario equals usuario.IdUsuario
                                      select new
                                      {
                                          codHorario = horario.CodHorario,
                                          horaInicio = horario.HoraInicio,
                                          horaFin = horario.HoraFin,
                                          dia = horario.Dia,
                                          nombres = usuario.Nombres + " " + usuario.ApellidoPaterno,
                                          nombreCompleto = usuario.Nombres + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno,
                                          avatar = medico.Avatar
                                      }).ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Lista de horarios de médicos",
                    data = horarios
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

        [HttpDelete("deleteHorario/{codHorario}")]
        public async Task<ActionResult<Object>> DeleteHorario(int codHorario)
        {
            try
            {
                var horario = await context.Horarios.FirstOrDefaultAsync(x => x.CodHorario == codHorario);

                if (horario == null) return Ok(new
                {
                    success = false,
                    message = "No se encontro un horario"
                });

                context.Horarios.Remove(horario);
                await context.SaveChangesAsync(); ;

                return Ok(new
                {
                    success = true,
                    message = "Horario eliminado correctamente",
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
