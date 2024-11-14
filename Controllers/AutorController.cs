using Juan.Data;
using Juan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Juan.Controllers
{
    [Route("api/autores")]
    [ApiController]
    public class AutorController : Controller
    {
        private readonly MyDBContext _dbc;

        public AutorController(MyDBContext dbc)
        {
            _dbc = dbc;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> Listar()
        {
            return Ok(await _dbc.Autores.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] Autor autor)
        {
            if (string.IsNullOrWhiteSpace(autor.Nombre) || autor.Nombre.Length < 2 ||
                string.IsNullOrWhiteSpace(autor.Apellido) || autor.Apellido.Length < 2)
            {
                return BadRequest("Nombre y Apellido son obligatorios y deben tener al menos 2 caracteres.");
            }
            
            if (autor.FechaNacimiento != default(DateOnly))
            {
                if (!DateOnly.TryParse(autor.FechaNacimiento.ToString(), out _))
                {
                    return BadRequest("Fecha de nacimiento no es válida.");
                }
            }

            _dbc.Autores.Add(autor);
            await _dbc.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAutor), new { id = autor.Id }, autor);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> GetAutor(int id)
        {
            var autor = await _dbc.Autores.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] Autor autor)
        {
            var autorExistente = await _dbc.Autores.FindAsync(id);
            if (autorExistente == null)
            {
                return NotFound();
            }

            autorExistente.Nombre = autor.Nombre;
            autorExistente.Apellido = autor.Apellido;
            autorExistente.FechaNacimiento = autor.FechaNacimiento;
            autorExistente.Nacionalidad = autor.Nacionalidad;
            autorExistente.Biografia = autor.Biografia;

            _dbc.Autores.Update(autorExistente);
            await _dbc.SaveChangesAsync();
            return Ok(autorExistente);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var autor = await _dbc.Autores.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            

            _dbc.Autores.Remove(autor);
            await _dbc.SaveChangesAsync();
            return NoContent();
        }
    }
}
