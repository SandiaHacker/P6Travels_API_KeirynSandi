using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P6Travels_API_KeirynSandi.Models;
using P6Travels_API_KeirynSandi.ModelsDTOs;

namespace P6Travels_API_KeirynSandi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly P620242travelsContext _context;

        public UsersController(P620242travelsContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        //Esta version de get muestra la info de un ususario
        //segun su correo, y será usada para cargar la info del usuario
        //justo despues de la validacion del login en la app,
        //con esto crearemos un usuario global

        [HttpGet("GetUserInfoByEmail")]
        public ActionResult<IEnumerable<UserDTO>> GetUserInfoByEmail(string pEmail)
        {
            //acá vamos a imitar la misma consulta qyuee hicimos en SSMS
            //pero usando Linq

            var query = (from u in _context.Users
                         join ur in _context.UserRoles
                         on u.UserRoleId equals ur.UserRoleId
                         where u.Email == pEmail
                         select new
                         {
                             id = u.UserId,
                             correo = u.Email,
                             nombre = u.Name,
                             telefono = u.PhoneNumber,
                             contrasennia = u.LoginPassword,
                             rolid = u.UserRoleId,
                             desciprol = ur.UserRoleDescription
                         }
                         ).ToList();
            //Los nombres que están dentro del selec new{}
            //se usarán de forma temporal para la captura de los 
            //datos que luego se pasaran al DTO de respuesta de
            //la función

            //Ahora que tenemos la info necesaria, vamos a crear 
            //un objeto en lista del tipo DTO de respuesta, 
            //para llenar con los datos de la consulta 

            List<UserDTO> list = new List<UserDTO>();

            //como query puede tener varios resultados entonces hacemos
            //un recorrido por cada uno de ellos con un foreach

            //Foreach = recorre objetos

            foreach (var item in query)
            {
                UserDTO nuevoUsuario = new UserDTO()
                {
                    UsuarioID = item.id,
                    Correo = item.correo,
                    Nombre = item.nombre,
                    Telefono = item.telefono,
                    Contrasennia = item.contrasennia,
                    RolID = item.rolid,
                    RolDescripcion = item.desciprol

                };
                list.Add(nuevoUsuario);
            }

            if (list == null) { return NotFound(); }

            return list;
        }


        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
