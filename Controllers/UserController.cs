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
    }
}
