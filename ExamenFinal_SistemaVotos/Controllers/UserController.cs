using ExamenFinal_SistemaVotos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;

namespace ExamenFinal_SistemaVotos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly SistemaVotosContext _context;

        public UserController()
        {
            _context = new SistemaVotosContext();
        }


        //OBTENER INFORMACION DE LOS USUARIOS
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<BD_SVotos.User>> GetList()
        {
            IEnumerable<BD_SVotos.User> usuario = await _context.Users
                                                      .Select(u =>
                                                        new BD_SVotos.User
                                                        {
                                                            Id = u.Id,
                                                            NombreUsuario = u.NombreUsuario,
                                                            Password = u.Password

                                                        }).OrderBy(s => s.NombreUsuario).ToListAsync();

            return usuario;
        }

        //CREAR USUARIO
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Set")]
        [HttpPost]
        public async Task<BD_SVotos.GeneralResult> Set(BD_SVotos.User user)
        {
            BD_SVotos.GeneralResult generalResult = new BD_SVotos.GeneralResult
            {
                Result = false,
            };

            try
            {
                Models.User newUser = new Models.User
                {
                    Id = user.Id,
                    NombreUsuario = user.NombreUsuario,
                    Password = user.Password
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                return generalResult;
            }
            catch (Exception)
            {
                return generalResult;
                throw;
            }
        }

        //OBTNER POR ID
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetByID/{id}")]
        [HttpGet]
        public async Task<ActionResult> GetByID(int id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        //EDITAR USUARIO 
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Edit/{id}")]
        [HttpPut]
        public async Task<ActionResult> Edit(int id, BD_SVotos.User user)
        {
            BD_SVotos.GeneralResult generalResult = new BD_SVotos.GeneralResult
            {
                Result = false
            };

            try
            {
                Models.User newUser = new Models.User
                {
                    Id = user.Id,
                    NombreUsuario = user.NombreUsuario,
                    Password = user.Password
                };
                _context.Update(newUser);
                await _context.SaveChangesAsync();
                return Ok(generalResult);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }

        //ELIMINAR USUARIO
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            BD_SVotos.GeneralResult generalResult = new BD_SVotos.GeneralResult
            {
                Result = false
            };

            if (_context.Users == null)
            {
                return NotFound("Entity set 'BS_VotosContext.Users'  is null.");
            }

            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return Ok(generalResult);
        }
    }
        
    
}
