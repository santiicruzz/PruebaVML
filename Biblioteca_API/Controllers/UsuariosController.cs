using Biblioteca_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_API.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
    public class UsuariosController : ControllerBase
    {
        private BibliotecaContext _bibliotecaContext;
        public  UsuariosController (BibliotecaContext bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        // GET: api/<UsuariosController>
        [HttpGet]
        public async Task<List<Usuario>> GetUsuarios()
        {
            var usuarios = await _bibliotecaContext.Usuarios.ToListAsync();
            return usuarios;
        }

        // GET api/<UsuariosController>/
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _bibliotecaContext.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                return Ok(usuario);
            }
            return NotFound();
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] Usuario usuarioObj)
        {
            if (usuarioObj != null)
            {
                var usuario = new Usuario
                {
                    Cedula = usuarioObj.Cedula,
                    PrimerNombre = usuarioObj.PrimerNombre,
                    SegundoNombre = usuarioObj.SegundoNombre,
                    PrimerApellido = usuarioObj.PrimerApellido,
                    SegundoApellido = usuarioObj.SegundoApellido,
                    CorreElectronico = usuarioObj.CorreElectronico,
                    NumeroContacto = usuarioObj.NumeroContacto
                };
                await _bibliotecaContext.AddAsync(usuario);
                await _bibliotecaContext.SaveChangesAsync();
            }
            return Ok(new { mensaje = "Uusario Creado" });
        }

        // PUT api/<UsuariosController>/
        [HttpPut]
        public async Task<IActionResult> PutUsuario([FromBody] Usuario usuarioObj)
        {
            var usuario = await _bibliotecaContext.Usuarios.FindAsync(usuarioObj.Id);
            if (usuario == null)
            {
                return BadRequest();
            }
            usuario.Cedula = usuarioObj.Cedula;
            usuario.PrimerNombre = usuarioObj.PrimerNombre;
            usuario.SegundoNombre = usuarioObj.SegundoNombre;
            usuario.PrimerApellido = usuarioObj.SegundoApellido;
            usuario.SegundoApellido = usuarioObj.SegundoApellido;
            usuario.CorreElectronico = usuarioObj.CorreElectronico;
            usuario.NumeroContacto = usuarioObj.NumeroContacto;

            _bibliotecaContext.Entry(usuario).State = EntityState.Modified;
            await _bibliotecaContext.SaveChangesAsync();
            return Ok(new { mensaje = "Usuario actualizado" });
        }

        // DELETE api/<LibrosController>/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                var usuario = await _bibliotecaContext.Usuarios.FindAsync(id);
                if (usuario == null)
                    return BadRequest();
                _bibliotecaContext.Usuarios.Remove(usuario);
                await _bibliotecaContext.SaveChangesAsync();
                return Ok(new { mensaje = "Usuario eliminado" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }
}
