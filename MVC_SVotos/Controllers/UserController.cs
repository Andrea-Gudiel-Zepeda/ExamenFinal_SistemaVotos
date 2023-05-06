using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MVC_Bicicletas.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;


        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Login()
        {
            return View();
        }

        //Muestra la página para iniciar sesión
        [HttpGet]
        public IActionResult SingIn()
        {
            return View();
        }

        //Obtiene los datos para validar e ingresar a la pagina principal
        [HttpPost]
        public async Task<IActionResult> SingIn(string NombreUsuario, string Password)
        {
            BD_Bicicletas.Token token = await Functions.APIServiceUser.Login(
                new BD_Bicicletas.Token
                {
                    token = "asdkhfalskdjfhas"
                });

            if (string.IsNullOrEmpty(token.token))
            {
                return NotFound();
            }

            IEnumerable<BD_Bicicletas.Voto> usuario = await Functions.APIServiceUser.UserGetList(token.token);

            foreach (var us in usuario)
            {
                if (us.NombreUsuario == NombreUsuario)
                {
                    if (us.Password == Password)
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim("idUser", Convert.ToString(us.Id)));
                        //claims.Add(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(us.IdUser)));
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        await HttpContext.SignInAsync(claimsPrincipal);
                        return RedirectToAction("Login", "User");

                    }
                    else
                    {
                        ViewBag.Credenciales = "La contraseña ingresada es incorrecta";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Credenciales = "No se encontró ningún usuario registrado con este correo";
                    return View();
                }
            }


            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("SingIn", "User");
        }
    }
}
