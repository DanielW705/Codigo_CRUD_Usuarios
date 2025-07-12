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

        //constructor para ser inyectado con los servicios
        public ValidateUserCase(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Result<Usuarios>> Execute(LoginUserViewModel usuarios) => await ValidateUserExist(usuarios);

        private async Task<Result<Usuarios>> ValidateUserExist(LoginUserViewModel usuarios)
        {
            //Buscamos el primer usuario que haya
            Usuarios? usuario = await _applicationDbContext.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario.Equals(usuarios.NombreUsuario) && u.Contrasena.Equals(usuarios.Contrasena));
            // si no es nulo, se devuelve en caso contrario se manda error
            return usuario is not null ? usuario : Result.Failure<Usuarios>("El usuario no existe");

        }

    }
}
