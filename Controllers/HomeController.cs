using System.Diagnostics;
using System.Threading.Tasks;
using Codigo_examen.Models;
using Codigo_examen.Models.ViewModel;
using Codigo_examen.UseCase;
using Microsoft.AspNetCore.Mvc;
using ROP;

namespace Codigo_examen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ValidateUserCase _validateUserCase;
        public readonly AddUserCase _addUserCase;
        public readonly GetUserCase _getUserCase;
        public HomeController(ValidateUserCase validateUserCase, AddUserCase addUserCase, GetUserCase getUserCase)
        {
            _validateUserCase = validateUserCase;
            _addUserCase = addUserCase;
            _getUserCase = getUserCase;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel usuarios)
        {
            ActionResult output;
            if (ModelState.IsValid)
            {
                Result<Usuarios> userValidate = await _validateUserCase.Execute(usuarios);

                if (userValidate.Success)
                    output = RedirectToAction("MainPage", "Home", new { userValidate.Value.Id });
                else
                {
                    foreach (var error in userValidate.Errors)
                        ModelState.AddModelError("", error.Message);
                    output = View("Index", usuarios);
                }

            }
            else
                output = View("Index", usuarios);

            return output;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CrearUsuario(CreateUsuarioViewModel nvoUsuario)
        {
            ActionResult output;
            if (ModelState.IsValid)
            {
                Result<Usuarios> UserAdded = await _addUserCase.Execute(nvoUsuario);
                if (UserAdded.Success)
                    output = RedirectToAction("MainPage", "Home", new { UserAdded.Value.Id });
                else
                {
                    foreach (var error in UserAdded.Errors)
                        ModelState.AddModelError("", error.Message);
                    output = View("Create", nvoUsuario);
                }
            }
            else
                output = View("Create", nvoUsuario);

            return output;
        }
        [HttpGet]
        public async Task<IActionResult> MainPage(Guid Id)
        {
            Result<Usuarios> usuario = await _getUserCase.Execute(Id);
            return View(usuario.Value);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
