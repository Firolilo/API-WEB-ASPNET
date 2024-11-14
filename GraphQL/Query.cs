using Juan.Data;
using Juan.Models;
using Microsoft.EntityFrameworkCore;

namespace Juan.GraphQL
{
    public class Query
    {
        public async Task<List<Autor>> GetAutores([Service] MyDBContext context)
        {
            return await context.Autores.ToListAsync();
        }

        public async Task<Autor?> GetAutorById(int id, [Service] MyDBContext context)
        {
            return await context.Autores.FindAsync(id);
        }
    }
}