using BD_SVotos;
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
    public class VotoController : Controller
    {
        int cantidadFraunde = 0;
        string cantip = "192.168.0.0";
        int idCandiFav = 0;
        private readonly ILogger<CandidatoController> _logger;

        public VotoController(ILogger<CandidatoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Login()
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

            IEnumerable<BD_SVotos.Voto> voto_ls = await Functions.APIServiceVoto.VotoGetList(token.token);
            ViewBag.CantVotos = voto_ls.Count();
            ViewBag.CantFraud = cantidadFraunde;

            return View(voto_ls);
        }

        //Obtener listado
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> VotoList()
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

            IEnumerable<BD_SVotos.Voto> voto_ls = await Functions.APIServiceVoto.VotoGetList(token.token);
            
            return View(voto_ls);
        }

        //Crear 
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateVoto()
        {

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateVoto([Bind("VotoId, Voto1, CandidatoId, VotanteId, FechaHora, Ip")] BD_SVotos.Voto voto)
        {
            int idUsuario = Convert.ToInt32(User.Claims.FirstOrDefault(s => s.Type == "idUser")?.Value);

            BD_SVotos.Token token = await Functions.APIServiceVoto.Login(
            new BD_SVotos.Token
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            IEnumerable<BD_SVotos.Voto> voto_ls = await Functions.APIServiceVoto.VotoGetList(token.token);
            foreach(var _voto in voto_ls)
            {
                if(_voto.VotanteId == idUsuario)
                {
                    cantidadFraunde++;
                    ViewBag.Fraude = "intento de hacer fraude";
                    return View();
                }
            }

            voto.VotanteId = idUsuario;
            voto.FechaHora = DateTime.Now.ToString();
            voto.Ip = cantip + 1;
            if (!ModelState.IsValid)
            {
                await Functions.APIServiceVoto.VotoSet(voto, token.token);
                return RedirectToAction("VotoList", "Voto");
            }

            return View();
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditVoto(int id)
        {

            BD_SVotos.Token token = await Functions.APIServiceVoto.Login(
            new BD_SVotos.Token
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

           BD_SVotos.Voto voto = await Functions.APIServiceVoto.GetVotoByID(id, token.token);

            return View(voto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditVoto(int id, [Bind("VotoId, Voto1, CandidatoId, VotanteId, FechaHora, Ip")] BD_SVotos.Voto voto)
        {

            BD_SVotos.Token token = await Functions.APIServiceVoto.Login(
            new BD_SVotos.Token
            {
                token = "asdkhfalskdjfhas"
            });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            if (id != voto.VotoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await Functions.APIServiceVoto.VotoEdit(voto, id, token.token);
                return RedirectToAction(nameof(VotoList));

            }

            return View(voto);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteVoto(int id)
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

            BD_SVotos.Voto voto = await Functions.APIServiceVoto.GetVotoByID(id, token.token);


            return View(voto);
        }


        [HttpPost, ActionName("DeleteVoto")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmeV(int id)
        {

            BD_SVotos.Token token = await Functions.APIServiceVoto.Login(
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
                await Functions.APIServiceVoto.VotoDelete(id, token.token);
            }


            return RedirectToAction(nameof(VotoList));
        }

    }
}
