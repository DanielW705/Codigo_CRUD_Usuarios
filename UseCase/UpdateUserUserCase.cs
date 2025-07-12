using Codigo_examen.Data;
using Codigo_examen.Models;
using Codigo_examen.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using ROP;

namespace Codigo_examen.UseCase
{
    public class UpdateUserUserCase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private readonly GetUserCase _getUserCase;
        public UpdateUserUserCase(ApplicationDbContext applicationDbContext, GetUserCase getUserCase)
        {
            _applicationDbContext = applicationDbContext;
            _getUserCase = getUserCase;
        }
        private async Task<Result<bool>> VerificarQueHayaSoloUnUsuario(ActualizarUsuarioViewModel usuario)
        {
            bool existe = await _applicationDbContext.Usuarios.AnyAsync(u => !u.Id.Equals(usuario.Id) && u.NombreUsuario.Equals(usuario.NombreUsuario));
            return !existe ? existe : Result.Failure<bool>("Ese usuario ya esta en uso");
        }
        private async Task<Result<bool>> VerificarQueHayaSoloUnCorreo(ActualizarUsuarioViewModel usuario)
        { 
            bool existe = await _applicationDbContext.DatosExtras.AnyAsync(u => !u.DatosExtraDelUsuario.Equals(usuario.Id) && u.Email.Equals(usuario.Email));
            return !existe ? existe : Result.Failure<bool>("Ese correo ya esta en uso");
        }
        private async Task<Result<bool>> Actualizar(Usuarios usuarios, ActualizarUsuarioViewModel usuarioViewModel)
        {
            if (!usuarios.NombreUsuario.Equals(usuarioViewModel.NombreUsuario))
                usuarios.NombreUsuario = usuarioViewModel.NombreUsuario;
            if (!usuarios.DatosExtra.ApellidoPaterno.Equals(usuarioViewModel.ApellidoPaterno))
                usuarios.DatosExtra.ApellidoPaterno = usuarioViewModel.ApellidoPaterno;
            if (!(usuarios.DatosExtra.ApellidoMaterno == usuarioViewModel.ApellidoMaterno))
                usuarios.DatosExtra.ApellidoMaterno = usuarioViewModel.ApellidoMaterno;
            if (!usuarios.DatosExtra.Calle.Equals(usuarioViewModel.Calle))
                usuarios.DatosExtra.Calle = usuarioViewModel.Calle;
            if (!(usuarios.DatosExtra.NumeroExterior == usuarioViewModel.NumeroExterior))
                usuarios.DatosExtra.NumeroExterior = usuarioViewModel.NumeroExterior;
            if (!usuarios.DatosExtra.Colonia.Equals(usuarioViewModel.Colonia))
                usuarios.DatosExtra.Colonia = usuarioViewModel.Colonia;
            if (!usuarios.DatosExtra.CodigoPostal.Equals(usuarioViewModel.CodigoPostal))
                usuarios.DatosExtra.CodigoPostal = usuarioViewModel.CodigoPostal;
            if (!(usuarios.DatosExtra.Municipio == usuarioViewModel.Municipio))
                usuarios.DatosExtra.Municipio = usuarioViewModel.Municipio;
            if (!(usuarios.DatosExtra.Estado == usuarioViewModel.Estado))
                usuarios.DatosExtra.Estado = usuarioViewModel.Estado;
            if (!usuarios.DatosExtra.Email.Equals(usuarioViewModel.Email))
                usuarios.DatosExtra.Email = usuarioViewModel.Email;

            await _applicationDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Result<bool>> Execute(ActualizarUsuarioViewModel usuarioViewModel)
        {
            Usuarios usuario = (await _getUserCase.Execute(usuarioViewModel.Id)).Throw();
            return await VerificarQueHayaSoloUnUsuario(usuarioViewModel)
                         .Bind(_ => VerificarQueHayaSoloUnCorreo(usuarioViewModel))
                         .Bind(_ => Actualizar(usuario, usuarioViewModel));

        }
    }
}
