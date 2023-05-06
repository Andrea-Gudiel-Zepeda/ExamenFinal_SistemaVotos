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
        public async Task<IActionResult> BicicletumList()
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

            IEnumerable<BD_SVotos.Candidato> bici_ls = await Functions.APIServiceCandidato.BicicletaGetList(token.token);
            
            return View(bici_ls);
        }

        //Crear 
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateBici()
        {

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBici([Bind("Id, Nombre, Partido, CantidadPiñones")] BD_SVotos.Candidato candi)
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
                await Functions.APIServiceCandidato.BicicletaSet(candi, token.token);
                return RedirectToAction("BicicletumList", "Bicicletum");
            }

            

            return View();
        }

        public async Task<IActionResult> DetailsBici(int id)
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

            BD_SVotos.Candidato bici = await Functions.APIServiceCandidato.GetBicicletaByID(id, token.token);

            if (id == null || bici == null)
            {
                return NotFound();
            }

            return View(bici);
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditBici(int id)
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

           BD_SVotos.Candidato bici = await Functions.APIServiceCandidato.GetBicicletaByID(id, token.token);

            return View(bici);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditBici(int id, [Bind("Id, Nombre, Partido, CantidadPiñones")] BD_SVotos.Candidato candi)
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
                await Functions.APIServiceCandidato.BicicletaEdit(candi, id, token.token);
                return RedirectToAction(nameof(BicicletumList));

            }

            return View(candi);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteBici(int id)
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

            BD_SVotos.Candidato candi = await Functions.APIServiceCandidato.GetBicicletaByID(id, token.token);


            return View(candi);
        }


        [HttpPost, ActionName("DeleteBici")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmedB(int id)
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
                await Functions.APIServiceCandidato.BicicletaDelete(id, token.token);
            }


            return RedirectToAction(nameof(BicicletumList));
        }

    }
}
