using Biblioteca_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase   
    {
        private BibliotecaContext _bibliotecaContext;
        public PrestamosController(BibliotecaContext bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        // GET: api/<PrestamosController>
        [HttpGet]
        public async Task<List<Prestamo>> GetPrestamos()
        {
            var prestamos = await _bibliotecaContext.Prestamos.ToListAsync();
            return prestamos;
        }

        // GET api/<PrestamosController>/
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrestamoById(int id)
        {
            var prestamo = await _bibliotecaContext.Prestamos.FindAsync(id);
            if (prestamo != null)
            {
                return Ok(prestamo);
            }
            return NotFound();
        }

        // POST api/<PrestamosController>
        [HttpPost]
        public async Task<IActionResult> PostPrestamo([FromBody] Prestamo prestamoObj)
        {
            if (prestamoObj != null)
            {
                var prestamo = new Prestamo
                {
                    IdUsuario = prestamoObj.IdUsuario,
                    Idlibro = prestamoObj.Idlibro,
                    FechaPrestamo = prestamoObj.FechaPrestamo,
                    FechaDevolucion = prestamoObj.FechaDevolucion,
                    IdUsuarioNavigation = prestamoObj.IdUsuarioNavigation,
                    IdlibroNavigation = prestamoObj.IdlibroNavigation
                };
                await _bibliotecaContext.AddAsync(prestamo);
                await _bibliotecaContext.SaveChangesAsync();
            }
            return Ok(new { mensaje = "Prestamo Creado" });
        }

        // PUT api/<PrestamosController>/
        [HttpPut]
        public async Task<IActionResult> PutPrestamo([FromBody] Prestamo prestamoObj)
        {
            var prestamo = await _bibliotecaContext.Prestamos.FindAsync(prestamoObj.Id);
            if (prestamo == null)
            {
                return BadRequest();
            }
            prestamo.IdUsuario = prestamoObj.IdUsuario;
            prestamo.Idlibro = prestamoObj.Idlibro;
            prestamo.FechaPrestamo = prestamoObj.FechaPrestamo;
            prestamo.FechaDevolucion = prestamoObj.FechaDevolucion;
            prestamo.IdUsuarioNavigation = prestamoObj.IdUsuarioNavigation;
            prestamo.IdlibroNavigation = prestamoObj.IdlibroNavigation;

            _bibliotecaContext.Entry(prestamo).State = EntityState.Modified;
            await _bibliotecaContext.SaveChangesAsync();
            return Ok(new { mensaje = "Prestamo actualizado" });
        }

        // DELETE api/<PrestamosController>/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestamo(int id)
        {
            try
            {
                var prestamo = await _bibliotecaContext.Prestamos.FindAsync(id);
                if (prestamo == null)
                    return BadRequest();
                _bibliotecaContext.Prestamos.Remove(prestamo);
                await _bibliotecaContext.SaveChangesAsync();
                return Ok(new { mensaje = "Prestamo eliminado" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
