using Microsoft.AspNetCore.Mvc;
using PracticaMVC.Models;
using PracticaMVC.Servicios;
using System.Diagnostics;

namespace PracticaMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly equiposDbContext _context;

        public HomeController(ILogger<HomeController> logger, equiposDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Autenticacion]
        public IActionResult Index()
        {
            //Recuperamos las variables de session de UsuarioId y TipoUsuario
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var tipoUsuario = HttpContext.Session.GetString("TipoUsuario");
            var nombreUsuario = HttpContext.Session.GetString("Nombre");

            if (usuarioId == null)
            {
                //si no esxiste la session, redirigir al login
                return RedirectToAction("Autenticar", "Home");
            }

            //retorno informacion a la vista por ViewBag y ViewData
            ViewBag.nombre = nombreUsuario;
            ViewData["tipoUsuario"] = tipoUsuario;
            return View();
        }

        public IActionResult Autenticar()
        {
            ViewData["ErrorMessage"] = "";
            return View();
        }

        [HttpPost]
        public IActionResult Autenticar(string txtUsuario, string txtClave)
        {
            //validar usuario en la BD
            var usuario = (from u in _context.usuarios
                           where u.email == txtUsuario
                           && u.contrasenia == txtClave
                           && u.activo == "S"
                           && u.bloqueado == "N"
                           select u).FirstOrDefault();

            //Si el usuario existe con todas sus validaciones
            if (usuario != null)
            {
                //Se crean variables de sesion
                HttpContext.Session.SetInt32("UsuarioId", usuario.id_usuario);
                HttpContext.Session.SetString("TipoUsuario", usuario.tipo_usuario);
                HttpContext.Session.SetString("Nombre", usuario.nombre);

                //Se reedirecciona al metodo de Index del controlador Home
                return RedirectToAction("Index", "Home");
            }
            //Muestra por view data un error
            ViewData["ErrorMessage"] = "Error, usuario invalido";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View();
        }
    }
}
