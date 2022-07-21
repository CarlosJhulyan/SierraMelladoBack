using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SierraMelladoBack.Models;

namespace SierraMelladoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly SierraMelladoDBContext context;
        public IWebHostEnvironment Environment;
        public ArticuloController(SierraMelladoDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.Environment = webHostEnvironment;
        }

        [HttpPost("createArticulo")]
        public async Task<ActionResult<Object>> CreateArticulo([FromForm] Articulo articulo)
        {
            try
            {
                articulo.FechaCrea = DateTime.Now;
                articulo.FechaMod = DateTime.Now;

                var files = HttpContext.Request.Form.Files;
                if (files.LongCount() > 0)
                {
                    var filePath = await UploadImage(files[0]);
                    articulo.Imagen = filePath.Value;
                }

                context.Articulos.Add(articulo);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Artículo creado correctamente",
                    data = articulo

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

        [HttpGet("getRecentArticulos")]
        public async Task<ActionResult<Articulo>> GetRecentArticulos()
        {
            try
            {
                var articulos = await (from articulo in context.Articulos
                                       join admin in context.Admins
                                       on articulo.IdAdmin equals admin.IdAdmin
                                       join usuario in context.Usuarios
                                       on admin.IdUsuario equals usuario.IdUsuario
                                       select new
                                       {
                                           titulo = articulo.Titulo,
                                           contenido = articulo.Contenido,
                                           fechaCrea = articulo.FechaCrea,
                                           fechaMod = articulo.FechaMod,
                                           admin = usuario.Nombres,
                                           key = articulo.CodArticulo,
                                           autor = articulo.Autor
                                       })
                                       .OrderByDescending(x => x.fechaCrea)
                                       .ToListAsync();


                return Ok(new
                {
                    success = true,
                    message = "Artículos recientes",
                    data = articulos
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

        [HttpGet("getRecentArticulosHome")]
        public async Task<ActionResult<Articulo>> GetRecentArticulosHome()
        {
            try
            {
                var articulos = await context.Articulos.OrderByDescending(x => x.FechaCrea).Take(3).ToListAsync();


                return Ok(new
                {
                    success = true,
                    message = "Artículos recientespara el home",
                    data = articulos
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
            var newFileName = "articulo_" + System.Guid.NewGuid() + fi.Extension;
            var path = Path.Combine(Environment.ContentRootPath, "wwwroot/images");
            using (var stream = new FileStream(Path.Combine(path, newFileName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
                return Path.Combine("images", newFileName);
            }
        }
    }
}
