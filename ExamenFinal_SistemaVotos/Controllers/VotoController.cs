using ExamenFinal_SistemaVotos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using BD_SVotos;

namespace ExamenFinal_SistemaVotos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VotoController : Controller
    {
        private readonly SistemaVotosContext _context;

        public VotoController()
        {
            _context = new SistemaVotosContext();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetList")]
        [HttpGet]
        public async Task<IEnumerable<BD_SVotos.Voto>> GetList()
        {
            IEnumerable<BD_SVotos.Voto> voto = await _context.Votos
                                                      .Select(v =>
                                                        new BD_SVotos.Voto
                                                        {
                                                            VotoId = v.VotoId,
                                                            Voto1 = v.Voto1,
                                                            CandidatoId = v.CandidatoId,
                                                            VotanteId = v.VotanteId,
                                                            FechaHora = v.FechaHora,
                                                            Ip = v.Ip

                                                        }).ToListAsync();

            return voto;
        }

        
        //CREAR BOOK
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Set")]
        [HttpPost]
        public async Task<BD_SVotos.GeneralResult> Set(BD_SVotos.Voto v)
        {
            BD_SVotos.GeneralResult generalResult = new BD_SVotos.GeneralResult
            {
                Result = false,
            };

            try
            {
                Models.Voto newVoto = new Models.Voto
                {
                    VotoId = v.VotoId,
                    Voto1 = v.Voto1,
                    CandidatoId = v.CandidatoId,
                    VotanteId = v.VotanteId,
                    FechaHora = v.FechaHora,
                    Ip = v.Ip

                };

                _context.Votos.Add(newVoto);
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
            if (id == null || _context.Votos == null)
            {
                return NotFound();
            }

            var voto = await _context.Votos.FindAsync(id);

            if (voto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(voto);
            }
        }

        //EDITAR BOOK
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Edit/{id}")]
        [HttpPut]
        public async Task<ActionResult> Edit(int id, BD_SVotos.Voto v)
        {
            BD_SVotos.GeneralResult generalResult = new BD_SVotos.GeneralResult
            {
                Result = false
            };

            try
            {
                Models.Voto newVoto = new Models.Voto
                {
                    VotoId = v.VotoId,
                    Voto1 = v.Voto1,
                    CandidatoId = v.CandidatoId,
                    VotanteId = v.VotanteId,
                    FechaHora = v.FechaHora,
                    Ip = v.Ip

                };

                _context.Update(newVoto);
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

            if (_context.Votos == null)
            {
                return NotFound("Entity set 'SistemaVotosContext.Votos'  is null.");
            }

            var voto = await _context.Votos.FindAsync(id);
            if (voto != null)
            {
                _context.Votos.Remove(voto);
            }

            await _context.SaveChangesAsync();
            return Ok(generalResult);
        }
    }
}
