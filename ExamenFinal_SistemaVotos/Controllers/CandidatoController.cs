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
    public class CandidatoController : Controller
    {
        private readonly SistemaVotosContext _context;

        public CandidatoController()
        {
            _context = new SistemaVotosContext();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<BD_SVotos.Candidato>> GetList()
        {
            IEnumerable<BD_SVotos.Candidato> candi = await _context.Candidatos
                                                      .Select(b =>
                                                        new BD_SVotos.Candidato
                                                        {
                                                            Id = b.Id,
                                                            Nombre = b.Nombre,
                                                            Partido = b.Partido,
                                                            CantidadPiñones = b.CantidadPiñones,

                                                        }).ToListAsync();

            return candi;
        }

        
        //CREAR BOOK
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Set")]
        [HttpPost]
        public async Task<BD_SVotos.GeneralResult> Set(BD_SVotos.Candidato candi)
        {
            BD_SVotos.GeneralResult generalResult = new BD_SVotos.GeneralResult
            {
                Result = false,
            };

            try
            {
                ExamenFinal_SistemaVotos.Models.Candidato newCandi = new ExamenFinal_SistemaVotos.Models.Candidato
                {
                    Id = candi.Id,
                    Nombre = candi.Nombre,
                    Partido = candi.Partido,
                    CantidadPiñones = candi.CantidadPiñones,
                };

                _context.Candidatos.Add(newCandi);
                await _context.SaveChangesAsync();
                return generalResult;
            }
            catch (Exception)
            {
                return generalResult;
                throw;
            }
        }

        //OBTNER POR ID BOOK
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetByID/{id}")]
        [HttpGet]
        public async Task<ActionResult> GetByID(int id)
        {
            if (id == null || _context.Candidatos == null)
            {
                return NotFound();
            }

            var candi = await _context.Candidatos.FindAsync(id);

            if (candi == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(candi);
            }
        }

        //EDITAR BOOK
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Edit/{id}")]
        [HttpPut]
        public async Task<ActionResult> Edit(int id, BD_SVotos.Candidato candi)
        {
            BD_SVotos.GeneralResult generalResult = new BD_SVotos.GeneralResult
            {
                Result = false
            };

            try
            {
                ExamenFinal_SistemaVotos.Models.Candidato newCandi = new ExamenFinal_SistemaVotos.Models.Candidato
                {
                    Id = candi.Id,
                    Nombre = candi.Nombre,
                    Partido = candi.Partido,
                    CantidadPiñones = candi.CantidadPiñones,
                };

                _context.Update(newCandi);
                await _context.SaveChangesAsync();
                return Ok(generalResult);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }

        //ELIMINAR BOOK
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            BD_SVotos.GeneralResult generalResult = new BD_SVotos.GeneralResult
            {
                Result = false
            };

            if (_context.Candidatos == null)
            {
                return NotFound("Entity set 'SistemaVotosContext.Candidatos'  is null.");
            }

            var candi = await _context.Candidatos.FindAsync(id);
            if (candi != null)
            {
                _context.Candidatos.Remove(candi);
            }

            await _context.SaveChangesAsync();
            return Ok(generalResult);
        }
    }
}
