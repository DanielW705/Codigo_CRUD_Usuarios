using System.Diagnostics;
using Codigo_examen.Models;
using Codigo_examen.Models.Mapper;
using Codigo_examen.Models.ViewModel;
using Codigo_examen.UseCase;
using Microsoft.AspNetCore.Mvc;
using ROP;

namespace Codigo_examen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ValidateUserCase _validateUserCase;
        private readonly AddUserCase _addUserCase;
        private readonly GetPaginatedClassUseCase _getPaginatedClassUseCase;
        private readonly DeleteUserCase _deleteUserCase;
        private readonly GetUserCase _getUserCase;
        private readonly UpdateUserUserCase _updateUserUserCase;
        public HomeController(ValidateUserCase validateUserCase, AddUserCase addUserCase, GetPaginatedClassUseCase getPaginatedClassUseCase, DeleteUserCase deleteUserCase, GetUserCase getUserCase, UpdateUserUserCase updateUserUserCase)
        {
            _validateUserCase = validateUserCase;
            _addUserCase = addUserCase;
            _getPaginatedClassUseCase = getPaginatedClassUseCase;
            _deleteUserCase = deleteUserCase;
            _getUserCase = getUserCase;
            _updateUserUserCase = updateUserUserCase;
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
                    output = RedirectToAction("AllUsers", "Home");
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
                    output = RedirectToAction("AllUsers", "Home");
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
        [HttpGet("AllUsers")]
        [HttpGet("AllUsers/{page:int}")]
        public async Task<IActionResult> AllUsers(string? search, int page = 1, int size = 5)
        {
            Pagination<UsuarioDto> usuarios = (await _getPaginatedClassUseCase.Execute(search, page, size)).Throw();
            return View(new PaginationViewModel
            {
                Pagination = usuarios
            });
        }
        [HttpGet("Delete/{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            Guid id = Guid.Parse(Id);
            await _deleteUserCase.Execute(id);
            return RedirectToAction("AllUsers");
        }
        [HttpGet("Update/{Id}")]
        public async Task<IActionResult> Update(string Id)
        {
            Guid id = Guid.Parse(Id);

            Usuarios usuarios = (await _getUserCase.Execute(id)).Throw();

            return View(new ActualizarUsuarioViewModel()
            {
                Id = id,
                NombreUsuario = usuarios.NombreUsuario,
                ApellidoPaterno = usuarios.DatosExtra.ApellidoPaterno,
                ApellidoMaterno = usuarios.DatosExtra.ApellidoMaterno,
                Calle = usuarios.DatosExtra.Calle,
                CodigoPostal = usuarios.DatosExtra.CodigoPostal,
                Colonia = usuarios.DatosExtra.Colonia,
                NumeroExterior = usuarios.DatosExtra.NumeroExterior,
                Municipio = usuarios.DatosExtra.Municipio,
                Estado = usuarios.DatosExtra.Estado,
                Email = usuarios.DatosExtra.Email,
            });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(ActualizarUsuarioViewModel actualizarUsuario)
        {
            ActionResult output;
            if (ModelState.IsValid)
            {
                Result<bool> updated = await _updateUserUserCase.Execute(actualizarUsuario);
                if (updated.Success)
                    output = RedirectToAction("AllUsers");
                else
                    output = View("Update", actualizarUsuario);
            }
            else
                output = View("Update", actualizarUsuario);
            return output;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
