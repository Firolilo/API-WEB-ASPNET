using Juan.Data;
using Juan.Models;
using HotChocolate;
using HotChocolate.Types;
using System.Threading.Tasks;

namespace Juan.GraphQL
{
    public class Mutation
    {
        public async Task<Autor> CreateAutor(AutorInput input, [Service] MyDBContext context)
        {
            var autor = new Autor
            {
                Nombre = input.Nombre,
                Apellido = input.Apellido,
                FechaNacimiento = input.FechaNacimiento,
                Nacionalidad = input.Nacionalidad,
                Biografia = input.Biografia
            };

            context.Autores.Add(autor);
            await context.SaveChangesAsync();

            return autor;
        }
        
        public async Task<Autor?> UpdateAutor(int id, AutorInput input, [Service] MyDBContext context)
        {
            var autor = await context.Autores.FindAsync(id);
            if (autor == null)
            {
                return null;
            }

            autor.Nombre = input.Nombre;
            autor.Apellido = input.Apellido;
            autor.FechaNacimiento = input.FechaNacimiento;
            autor.Nacionalidad = input.Nacionalidad;
            autor.Biografia = input.Biografia;

            await context.SaveChangesAsync();
            return autor;
        }
        
        public async Task<bool> DeleteAutor(int id, [Service] MyDBContext context)
        {
            var autor = await context.Autores.FindAsync(id);
            if (autor == null)
            {
                return false;
            }

            context.Autores.Remove(autor);
            await context.SaveChangesAsync();
            return true;
        }
    }
    
    public class AutorInput
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public string Biografia { get; set; }
    }
}
