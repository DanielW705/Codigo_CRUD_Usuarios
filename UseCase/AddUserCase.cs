using Codigo_examen.Data;
using Codigo_examen.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using ROP;
using Codigo_examen.Models;
namespace Codigo_examen.UseCase
{
    public class AddUserCase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AddUserCase(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        private async Task<Result<bool>> ValidateUser(string nombreUsuario)
        {
            bool usuarioExiste = await _applicationDbContext.Usuarios
                                        .Where(u => u.NombreUsuario.Equals(nombreUsuario))
                                        .AnyAsync();

            return !usuarioExiste ? usuarioExiste : Result.Failure<bool>("El usuario ya existe");
        }

        private async Task<Result<bool>> ValidatePassword(string contrasena, string confirmaContrasena)
        {
            bool passWordExiste = await _applicationDbContext.Usuarios
                                        .Where(u => u.Contrasena.Equals(contrasena))
                                        .AnyAsync();
            return !confirmaContrasena.Equals(contrasena) ?
                    Result.Failure<bool>("Se debe confirmar corectamente la contraseña")
                    : !passWordExiste ?
                    passWordExiste :
                    Result.Failure<bool>("Esta contraseña no se puede usar");
        }

        private async Task<Result<Usuarios>> AddUsuarioToDatabase(CreateUsuarioViewModel nvoUsuarioViewModel)
        {
            Usuarios nvoUsuario = new Usuarios
            {
                NombreUsuario = nvoUsuarioViewModel.NombreUsuario,
                Contrasena = nvoUsuarioViewModel.Contrasena
            };

            DatosExtra nvoDatosExtra = new DatosExtra
            {
                ApellidoPaterno = nvoUsuarioViewModel.ApellidoPaterno,
                ApellidoMaterno = nvoUsuarioViewModel.ApellidoMaterno,
                Calle = nvoUsuarioViewModel.Calle,
                NumeroExterior = nvoUsuarioViewModel.NumeroExterior,
                Colonia = nvoUsuarioViewModel.Colonia,
                CodigoPostal = nvoUsuarioViewModel.CodigoPostal,
                Municipio = nvoUsuarioViewModel.Municipio,
                Estado = nvoUsuarioViewModel.Estado,
                Email = nvoUsuarioViewModel.Email,
                usuario = nvoUsuario
            };
            await _applicationDbContext.Usuarios.AddAsync(nvoUsuario);
            await _applicationDbContext.DatosExtras.AddAsync(nvoDatosExtra);
            await _applicationDbContext.SaveChangesAsync();

            return nvoUsuario;
        }

        public async Task<Result<Usuarios>> Execute(CreateUsuarioViewModel nvoUsuarioViewModel)
            => await ValidatePassword(nvoUsuarioViewModel.Contrasena, nvoUsuarioViewModel.ConfirmacionContraseña)
            .Bind(x => ValidateUser(nvoUsuarioViewModel.NombreUsuario))
            .Bind(x => AddUsuarioToDatabase(nvoUsuarioViewModel));
    }
}
