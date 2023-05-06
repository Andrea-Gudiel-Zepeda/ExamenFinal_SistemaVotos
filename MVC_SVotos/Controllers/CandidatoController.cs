using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using System.Diagnostics;
using System.IO;


namespace MVC_SVotos.Controllers
{
    public class CandidatoController : Controller
    {
        private readonly ILogger<CandidatoController> _logger;

        public CandidatoController(ILogger<CandidatoController> logger)
        {
            _logger = logger;
        }

        //Obtener listado
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CandidatoList()
        {

            BD_SVotos.Token token = await Functions.APIServiceCandidato.Login(
            new BD_SVotos.Token
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            IEnumerable<BD_SVotos.Candidato> bici_ls = await Functions.APIServiceCandidato.CandidatoGetList(token.token);
            
            return View(bici_ls);
        }

        //Crear 
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateCandidato()
        {

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCandidato([Bind("Id, Nombre, Partido, CantidadPiñones")] BD_SVotos.Candidato candi)
        {

            BD_SVotos.Token token = await Functions.APIServiceCandidato.Login(
            new BD_SVotos.Token
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                await Functions.APIServiceCandidato.CandidatoSet(candi, token.token);
                return RedirectToAction("BicicletumList", "Bicicletum");
            }

            

            return View();
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditCandidato(int id)
        {

            BD_SVotos.Token token = await Functions.APIServiceCandidato.Login(
            new BD_SVotos.Token
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

           BD_SVotos.Candidato bici = await Functions.APIServiceCandidato.GetCandidatoByID(id, token.token);

            return View(bici);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditCandidato(int id, [Bind("Id, Nombre, Partido, CantidadPiñones")] BD_SVotos.Candidato candi)
        {

            BD_SVotos.Token token = await Functions.APIServiceCandidato.Login(
            new BD_SVotos.Token
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            if (id != candi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await Functions.APIServiceCandidato.CandidatoEdit(candi, id, token.token);
                return RedirectToAction(nameof(CandidatoList));

            }

            return View(candi);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteCandidato(int id)
        {

            BD_SVotos.Token token = await Functions.APIServiceCandidato.Login(
            new BD_SVotos.Token
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            BD_SVotos.Candidato candi = await Functions.APIServiceCandidato.GetCandidatoByID(id, token.token);


            return View(candi);
        }


        [HttpPost, ActionName("DeleteBici")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmedC(int id)
        {

            BD_SVotos.Token token = await Functions.APIServiceCandidato.Login(
            new BD_SVotos.Token
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            if (id != 0)
            {
                await Functions.APIServiceCandidato.CandidatoDelete(id, token.token);
            }


            return RedirectToAction(nameof(CandidatoList));
        }

    }
}
