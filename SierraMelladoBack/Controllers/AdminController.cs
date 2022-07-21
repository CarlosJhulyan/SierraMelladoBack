using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using SierraMelladoBack.Models;

namespace SierraMelladoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly SierraMelladoDBContext context;
        public IWebHostEnvironment Environment;
        public AdminController(SierraMelladoDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.Environment = webHostEnvironment;
        }
        
        [HttpGet("getMedicos")]
        public async Task<ActionResult<Object>> GetMedicos()
        {
            try
            {
                var query = await (from medico in context.Medicos
                                   join usuario in context.Usuarios
                                   on medico.IdUsuario equals usuario.IdUsuario
                                   orderby usuario.FechaCrea descending
                                   select new
                                   {
                                       nombres = usuario.Nombres,
                                       apellidoPaterno = usuario.ApellidoPaterno,
                                       apellidoMaterno = usuario.ApellidoMaterno,
                                       key = medico.IdMedico,
                                       idMedico = medico.IdMedico,
                                       fechaCrea = usuario.FechaCrea,
                                       correo = usuario.Correo,
                                       avatar = medico.Avatar,
                                       dni = medico.Dni,
                                       celular = medico.Celular,
                                       idUsuario = usuario.IdUsuario,
                                       codColegiado = medico.CodColegiado,
                                       fechaNac = medico.FechaNac,
                                       especialidades = medico.CodEspecialidads
                                   }).ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Lista de medicos",
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

        [HttpPost("getMedicosByEspecialidad")]
        public async Task<ActionResult<Object>> GetMedicosByEspecialidad(MedicoByEspecialidadSchema medicoByEspecialidadSchema)
        {
            try
            {
                var query = await (from medico in context.Medicos
                                   join usuario in context.Usuarios
                                   on medico.IdUsuario equals usuario.IdUsuario
                                   orderby usuario.FechaCrea descending
                                   select new
                                   {
                                       nombres = usuario.Nombres,
                                       apellidoPaterno = usuario.ApellidoPaterno,
                                       apellidoMaterno = usuario.ApellidoMaterno,
                                       key = medico.IdMedico,
                                       idMedico = medico.IdMedico,
                                       fechaCrea = usuario.FechaCrea,
                                       correo = usuario.Correo,
                                       avatar = medico.Avatar,
                                       dni = medico.Dni,
                                       celular = medico.Celular,
                                       idUsuario = usuario.IdUsuario,
                                       codColegiado = medico.CodColegiado,
                                       fechaNac = medico.FechaNac,
                                       especialidades = medico.CodEspecialidads
                                   })
                                   .Where(x => x.especialidades.First(x => x.CodEspecialidad == medicoByEspecialidadSchema.CodEspecialidad).CodEspecialidad == medicoByEspecialidadSchema.CodEspecialidad)
                                   .ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Lista de medicos",
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

        [HttpGet("getPacientes")]
        public async Task<ActionResult<Object>> GetPacientes()
        {
            try
            {
                var query = await (from paciente in context.Pacientes
                                   join usuario in context.Usuarios
                                   on paciente.IdUsuario equals usuario.IdUsuario
                                   orderby usuario.FechaCrea descending
                                   select new
                                   {
                                       nombres = usuario.Nombres,
                                       apellidoPaterno = usuario.ApellidoPaterno,
                                       apellidoMaterno = usuario.ApellidoMaterno,
                                       key = paciente.IdPaciente,
                                       idPaciente = paciente.IdPaciente,
                                       fechaCrea = usuario.FechaCrea,
                                       correo = usuario.Correo,
                                       avatar = paciente.Avatar,
                                       dni = paciente.Dni,
                                       celular = paciente.Celular,
                                       idUsuario = usuario.IdUsuario,
                                       fechaNac = paciente.FechaNac
                                   }).ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Lista de pacientes",
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

        [HttpGet("getAdmins")]
        public async Task<ActionResult<Object>> GetAdmins()
        {
            try
            {
                var query = await (from admin in context.Admins
                                   join usuario in context.Usuarios
                                   on admin.IdUsuario equals usuario.IdUsuario
                                   orderby usuario.FechaCrea descending
                                   select new
                                   {
                                       nombres = usuario.Nombres,
                                       apellidoPaterno = usuario.ApellidoPaterno,
                                       apellidoMaterno = usuario.ApellidoMaterno,
                                       idAdmin = admin.IdAdmin,
                                       key = admin.IdAdmin,
                                       fechaCrea = usuario.FechaCrea,
                                       correo = usuario.Correo,
                                       estado = admin.Estado,
                                       rol = admin.Rol
                                   }).ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Lista de personal administrativo",
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

        [HttpPost("createAdmin")]
        public async Task<ActionResult<Object>> CreateAdmin(Admin admin)
        {
            try
            {
                admin.Estado = 1;
                context.Admins.Add(admin);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Personal registrado correctamente",
                    data = admin
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

        [HttpPost("createUsuario")]
        public async Task<ActionResult<Object>> CreateUsuario(Usuario usuario)
        {
            try
            {
                var usuarioLost = await context.Usuarios.FirstOrDefaultAsync(x => x.Usuario1 == usuario.Usuario1);

                if (usuarioLost != null)
                {
                    return Ok(new
                    {
                        success = false,
                        message = "Este nombre de usuario ya esta registrado",
                    });
                }

                usuario.FechaCrea = DateTime.Now;
                usuario.FechaMod = DateTime.Now;
                usuario.Nombres = usuario.Nombres?.ToUpper();
                usuario.ApellidoMaterno = usuario.ApellidoMaterno?.ToUpper();
                usuario.ApellidoPaterno = usuario.ApellidoPaterno?.ToUpper();
                usuario.Clave = BCrypt.Net.BCrypt.HashPassword(usuario.Clave);

                context.Usuarios.Add(usuario);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Usuario registrado correctamente",
                    data = usuario
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

        [HttpPost("createMedico")]
        public async Task<ActionResult<Object>> CreateMedico([FromForm] Medico medico)
        {
            try
            {
                var medicoLost = await context.Medicos.FirstOrDefaultAsync(x => x.Dni == medico.Dni);

                if (medicoLost != null)
                {
                    return Ok(new
                    {
                        success = false,
                        message = "Ya existe un médico con este número de documento",
                    });
                }

                var medicoLost2 = await context.Medicos.FirstOrDefaultAsync(x => x.CodColegiado == medico.CodColegiado);

                if (medicoLost2 != null)
                {
                    return Ok(new
                    {
                        success = false,
                        message = "Ya existe un médico con este código de colegiado",
                    });
                }

                var files = HttpContext.Request.Form.Files;
                if (files.LongCount() > 0)
                {
                    var filePath = await UploadImage(files[0]);
                    medico.Avatar = filePath.Value;
                }

                medico.Estado = 1;
                context.Medicos.Add(medico);
                await context.SaveChangesAsync();
                
                return Ok(new
                {
                    success = true,
                    message = "Médico registrado correctamente",
                    data = medico
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
        public async Task<ActionResult<Object>> ChangeEstado(ChangeEstadoAdminSchema changeEstadoAdminSchema)
        {
            try
            {
                var currentAdmin = context.Admins.FirstOrDefault(x => x.IdAdmin == changeEstadoAdminSchema.IdAdmin);

                if (currentAdmin == null) return Ok(new
                {
                    success = false,
                    message = "No se encontró el pesonal para cambiar el estado",
                });

                currentAdmin.Estado = currentAdmin.Estado == 1 ? 0 : 1;

                context.Admins.Update(currentAdmin);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Se cambió el estado del personal"
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

        [HttpPatch("changeRolAdmin")]
        public async Task<ActionResult<Object>> ChangeRolAdmin(ChangeRolAdminSchema changeRolAdminEspecialidad)
        {
            try
            {
                var currentAdmin = context.Admins.FirstOrDefault(x => x.IdAdmin == changeRolAdminEspecialidad.IdAdmin);

                if (currentAdmin == null) return Ok(new
                {
                    success = false,
                    message = "No se encontró el personal para cambiar el estado",
                });

                currentAdmin.Rol = changeRolAdminEspecialidad.Rol;

                context.Admins.Update(currentAdmin);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Se cambió el rol del personal"
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

        [HttpDelete("deleteAdmin/{idAdmin}")]
        public async Task<ActionResult<Object>> DeleteAdmin(int idAdmin)
        {
            try
            {
                var admin = await context.Admins.FirstOrDefaultAsync(x => x.IdAdmin == idAdmin);

                if (admin != null)
                {
                    context.Admins.Remove(admin);
                    await context.SaveChangesAsync();
                    var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == admin.IdUsuario);

                    context.Usuarios.Remove(usuario);
                    await context.SaveChangesAsync();

                    return Ok(new
                    {
                        success = true,
                        message = "Personal eliminado correctamente"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        success = false,
                        message = "No se encontro al personal"
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

        [HttpDelete("deleteUsuario/{idUsuario}")]
        public async Task<ActionResult<Object>> DeleteUsuario(int idUsuario)
        {
            try
            {
                var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == idUsuario);

                if (usuario != null)
                {
                    context.Usuarios.Remove(usuario);
                    await context.SaveChangesAsync();

                    return Ok(new
                    {
                        success = true,
                        message = "Usuario eliminado correctamente"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        success = false,
                        message = "No se encontro al usuario"
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

    public partial class ChangeEstadoAdminSchema
    {
        public int? IdAdmin { get; set; }
    }

    public partial class ChangeRolAdminSchema
    {
        public int? IdAdmin { get; set; }
        public string? Rol { get; set; }
    }

    public partial class MedicoByEspecialidadSchema
    {
        public int CodEspecialidad { get; set; }
    }
}
