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
        //Tenemos todos los servicios que se deben de inyectar
        private readonly ValidateUserCase _validateUserCase;
        private readonly AddUserCase _addUserCase;
        private readonly GetPaginatedClassUseCase _getPaginatedClassUseCase;
        private readonly DeleteUserCase _deleteUserCase;
        private readonly GetUserCase _getUserCase;
        private readonly UpdateUserUserCase _updateUserUserCase;
        //Tenemos un constructor que cuando resuelve la inyeccion de los servicios, es inyectado con estos
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
            //Como no sabemos el resultado, ponemos una salida
            ActionResult output;
            if (ModelState.IsValid)
            {
                //Ejecutamos cuando hay que validar un solo usuario
                Result<Usuarios> userValidate = await _validateUserCase.Execute(usuarios);

                if (userValidate.Success)
                    //Redireccionamos si funciona
                    output = RedirectToAction("AllUsers", "Home");
                else
                {
                    foreach (var error in userValidate.Errors)
                        //Mandamos errores si es que no es exitoso
                        ModelState.AddModelError("", error.Message);
                    //Devolvemos la vista, con los modelos para mostrar los errores
                    output = View("Index", usuarios);
                }

            }
            else
                //Devolvemos la vista con los modelos para mostrar los errores
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
            //Como no sabemos el resultado, ponemos una salida
            ActionResult output;
            if (ModelState.IsValid)
            {
                //Ejecutamos la accion de agregar usuario
                Result<Usuarios> UserAdded = await _addUserCase.Execute(nvoUsuario);
                //Si es exitoso, se redirecciona
                if (UserAdded.Success)
                    output = RedirectToAction("AllUsers", "Home");
                else
                {
                    //En caso contrario, se manda errores y se devuelve a la vista
                    foreach (var error in UserAdded.Errors)
                        ModelState.AddModelError("", error.Message);
                    output = View("Create", nvoUsuario);
                }
            }
            else
                //En caso que el modelo, no sea valido regresa a la vista
                output = View("Create", nvoUsuario);

            return output;
        }
        //Enrutadores, para cuando se quiera una ruta especifica
        [HttpGet("AllUsers")]
        //Con parametros especificos
        [HttpGet("AllUsers/{page:int}")]
        //Controlador para paginar todos los usuarios
        public async Task<IActionResult> AllUsers(string? search, int page = 1, int size = 5)
        {
            //Se ejecuta la creacion de la paginacion
            Pagination<UsuarioDto> usuarios = (await _getPaginatedClassUseCase.Execute(search, page, size)).Throw();
            // Se devuelve una vista del modelo, donde esta la lista de paginacion
            return View(new PaginationViewModel
            {
                Pagination = usuarios
            });
        }
        //Enrutadores, para cuando se quiera una ruta especifica y parametros especificos
        [HttpGet("Delete/{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            Guid id = Guid.Parse(Id);
            //Se ejecuta la eliminacion de un usuario
            await _deleteUserCase.Execute(id);
            //Se redirije a una accion
            return RedirectToAction("AllUsers");
        }
        //Enrutadores, para cuando se quiera una ruta especifica y parametros especificos
        [HttpGet("Update/{Id}")]
        public async Task<IActionResult> Update(string Id)
        {
            Guid id = Guid.Parse(Id);
            //Se Ejecuta el mostrar un solo usaurio
            Usuarios usuarios = (await _getUserCase.Execute(id)).Throw();
            // Se devuelve una vista del modelo, donde esta la lista para un solo usuario
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
            //Como no sabemos el resultado, ponemos una salida
            ActionResult output;
            if (ModelState.IsValid)
            {
                //Ejecutamos la actualizacion del usuario
                Result<bool> updated = await _updateUserUserCase.Execute(actualizarUsuario);
                //Si es exitoso, se redirije a los usuarios
                if (updated.Success)
                    output = RedirectToAction("AllUsers");
                else
                    //Si no lo es, se muestran los errores
                    output = View("Update", actualizarUsuario);
            }
            else
                //Si no es valido el modelo, se mestran los errores
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
