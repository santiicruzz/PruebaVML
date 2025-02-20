using Biblioteca_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private BibliotecaContext _bibliotecaContext;
        public LibrosController(BibliotecaContext bibliotecaContext) { 
            _bibliotecaContext = bibliotecaContext;
        }


        // GET: api/<LibrosController>
        [HttpGet]
        public async Task<List<Libro>> GetLibros()
        {
            var libros = await _bibliotecaContext.Libros.ToListAsync();
            return libros;
        }

        // GET api/<BibliotecaController>/
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLibroById(int id)
        {
            var libro = await _bibliotecaContext.Libros.FindAsync(id);
            if (libro != null)
            {
                return Ok(libro);
            }
            return NotFound();
        }

        // POST api/<BibliotecaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Libro libroObj)
        {
            if (libroObj != null)
            {
                var libro = new Libro
                {
                    Titulo = libroObj.Titulo,
                    Autor = libroObj.Autor,
                    Genero = libroObj.Genero,
                    AnoPublicacion = libroObj.AnoPublicacion,
                };
                await _bibliotecaContext.AddAsync(libro);
                await _bibliotecaContext.SaveChangesAsync();
            }
            return Ok(new { mensaje = "Libro Creado" });
        }

        // PUT api/<LibroController>/
        [HttpPut]
        public async Task<IActionResult> PutLibro([FromBody] Libro libroObj)
        {
            var libro = await _bibliotecaContext.Libros.FindAsync(libroObj.Id);
            if (libro == null)
            {
                return BadRequest();
            }
            libro.Titulo = libroObj.Titulo;
            libro.Autor = libroObj.Autor;
            libro.Genero = libroObj.Genero;
            libro.AnoPublicacion = libroObj.AnoPublicacion;

            _bibliotecaContext.Entry(libro).State = EntityState.Modified;
            await _bibliotecaContext.SaveChangesAsync();
            return Ok(new { mensaje = "Libro actualizado" });
        }

        // DELETE api/<LibrosController>/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            try
            {
                var libro = await _bibliotecaContext.Libros.FindAsync(id);
                if (libro == null)
                    return BadRequest();
                _bibliotecaContext.Libros.Remove(libro);
                await _bibliotecaContext.SaveChangesAsync();
                return Ok(new { mensaje = "libro eliminado" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }


}
