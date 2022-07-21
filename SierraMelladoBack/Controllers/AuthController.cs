using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SierraMelladoBack.Models;

namespace SierraMelladoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SierraMelladoDBContext context;
        public IWebHostEnvironment Environment;
        public AuthController(SierraMelladoDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.Environment = webHostEnvironment;
        }

        [HttpPost("loginPaciente")]
        public async Task<ActionResult<Object>> LoginPaciente(LoginPacienteSchema loginPacienteSchema)
        {
            try
            {
                var query = await (from paciente in context.Pacientes
                                   join usuario in context.Usuarios
                                   on paciente.IdUsuario equals usuario.IdUsuario
                                   select new
                                   {
                                       idUsuario = usuario.IdUsuario,
                                       nombres = usuario.Nombres,
                                       apellidoPaterno = usuario.ApellidoPaterno,
                                       apellidoMaterno = usuario.ApellidoMaterno,
                                       correo = usuario.Correo,
                                       usuario = usuario.Usuario1,
                                       idPaciente = paciente.IdPaciente,
                                       dni = paciente.Dni,
                                       avatar = paciente.Avatar,
                                       clave = usuario.Clave,
                                       celular = paciente.Celular,
                                       fechaNac = paciente.FechaNac
                                   }).FirstOrDefaultAsync(x => x.usuario == loginPacienteSchema.User);

                if (query == null)
                {
                    return Ok(new
                    {
                        success = false,
                        message = "Este usuario no esta registrado",
                    });
                }

                var verified = BCrypt.Net.BCrypt.Verify(loginPacienteSchema.Pass, query.clave);

                if (!verified) return Ok(new
                {
                    success = false,
                    message = "La contraseña es incorrecta"
                });

                return Ok(new
                {
                    success = true,
                    message = "Ingreso correcto",
                    data = query
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

        [HttpPost("loginAdmin")]
        public async Task<ActionResult<Object>> LoginAdmin(LoginAdminSchema loginAdminSchema)
        {
            try
            {
                var query = await (from admin in context.Admins
                                   join usuario in context.Usuarios
                                   on admin.IdUsuario equals usuario.IdUsuario
                                   select new
                                   {
                                       idUsuario = usuario.IdUsuario,
                                       nombres = usuario.Nombres,
                                       apellidoPaterno = usuario.ApellidoPaterno,
                                       apellidoMaterno = usuario.ApellidoMaterno,
                                       correo = usuario.Correo,
                                       usuario = usuario.Usuario1,
                                       idAdmin = admin.IdAdmin,
                                       rol = admin.Rol,
                                       estado = admin.Estado,
                                       clave = usuario.Clave,
                                       fechaCrea = usuario.FechaCrea,
                                   }).FirstOrDefaultAsync(x => x.usuario == loginAdminSchema.User);

                if (query == null)
                {
                    return Ok(new
                    {
                        success = false,
                        message = "El administrador no existe",
                    });
                }

                var verified = BCrypt.Net.BCrypt.Verify(loginAdminSchema.Pass, query.clave);

                if (!verified) return Ok(new
                {
                    success = false,
                    message = "La contraseña es incorrecta"
                });

                if (query.estado == 0) return Ok(new
                {
                    success = false,
                    message = "Su usuario esta deshabilitado"
                });

                return Ok(new
                {
                    success = true,
                    message = "Ingreso correcto",
                    data = query
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

        [HttpPost("loginMedico")]
        public async Task<ActionResult<Object>> LoginMedico(LoginMedicoSchema loginMedicoSchema)
        {
            try
            {
                var query = await (from medico in context.Medicos
                                   join usuario in context.Usuarios
                                   on medico.IdUsuario equals usuario.IdUsuario
                                   select new
                                   {
                                       idUsuario = usuario.IdUsuario,
                                       nombres = usuario.Nombres,
                                       apellidoPaterno = usuario.ApellidoPaterno,
                                       apellidoMaterno = usuario.ApellidoMaterno,
                                       correo = usuario.Correo,
                                       usuario = usuario.Usuario1,
                                       idMedico = medico.IdMedico,
                                       clave = usuario.Clave,
                                       avatar = medico.Avatar,
                                       celular = medico.Celular,
                                       fechaNac = medico.FechaNac,
                                       dni = medico.Dni,
                                       codColegiado = medico.CodColegiado,
                                       especialidades = medico.CodEspecialidads
                                   }).FirstOrDefaultAsync(x => x.usuario == loginMedicoSchema.User);

                if (query == null)
                {
                    return Ok(new
                    {
                        success = false,
                        message = "El médico no existe",
                    });
                }

                var verified = BCrypt.Net.BCrypt.Verify(loginMedicoSchema.Pass, query.clave);

                if (!verified) return Ok(new
                {
                    success = false,
                    message = "La contraseña es incorrecta"
                });

                return Ok(new
                {
                    success = true,
                    message = "Ingreso correcto",
                    data = query
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

        [HttpPatch("changePassword")]
        public async Task<ActionResult<Object>> ChangePassword(ChangePasswordSchema changePasswordSchema)
        {
            try
            {
                var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == changePasswordSchema.IdUsuario);

                if (usuario == null) return Ok(new
                {
                    success = false,
                    message = "No se encontró al usuario"
                });

                usuario.Clave = BCrypt.Net.BCrypt.HashPassword(changePasswordSchema.clave);
                context.Usuarios.Update(usuario);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Se cambio su contraseña"
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

    public partial class LoginPacienteSchema
    {
        public string? Pass { get; set; }
        public string? User { get; set; }
    }

    public partial class LoginAdminSchema
    {
        public string? Pass { get; set; }
        public string? User { get; set; }
    }

    public partial class LoginMedicoSchema
    {
        public string? Pass { get; set; }
        public string? User { get; set; }
    }

    public partial class ChangePasswordSchema
    {
        public int IdUsuario { get; set; }
        public string clave { get; set; }
    }
}
