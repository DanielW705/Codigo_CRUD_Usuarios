using Codigo_examen.Data;
using Codigo_examen.Models;
using Codigo_examen.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using ROP;

namespace Codigo_examen.UseCase
{
    public class ValidateUserCase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ValidateUserCase(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Result<Usuarios>> Execute(LoginUserViewModel usuarios) => await ValidateUserExist(usuarios);

        private async Task<Result<Usuarios>> ValidateUserExist(LoginUserViewModel usuarios)
        {
            Usuarios? usuario = await _applicationDbContext.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario.Equals(usuarios.NombreUsuario) && u.Contrasena.Equals(usuarios.Contrasena));

            return usuario is not null ? usuario : Result.Failure<Usuarios>("El usuario no existe");

        }

    }
}
