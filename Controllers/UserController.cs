using Juan.Data;
using Juan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Juan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private MyDBContext _dbc;
        public UserController(MyDBContext dbc)
        {
            _dbc = dbc;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Listar()
        {
            return await _dbc.Usuarios.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Grabar(User user)
        {
            _dbc.Usuarios.Add(user);
            await _dbc.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetUsuario), new { id = user.Id }, user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUsuario(int id)
        {
            var usuario = await _dbc.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }
        
        [HttpDelete]
        public async Task<ActionResult<User>> Eliminar(int id)
        {
            var usuario = await _dbc.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            _dbc.Usuarios.Remove(usuario);
            await _dbc.SaveChangesAsync();
            return Ok(usuario);
        }

        [HttpPut]
        public async Task<ActionResult<User>> Editar(int id, User user)
        {
            var usuario = await _dbc.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            usuario.Name = user.Name;
            usuario.LastName = user.LastName;
            usuario.Email = user.Email;
            usuario.Password = user.Password;

            try
            {
                _dbc.Usuarios.Update(usuario);
                await _dbc.SaveChangesAsync();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
