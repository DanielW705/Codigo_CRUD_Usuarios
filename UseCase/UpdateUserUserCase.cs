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

        //constructor para ser inyectado con los servicios
        public UpdateUserUserCase(ApplicationDbContext applicationDbContext, GetUserCase getUserCase)
        {
            _applicationDbContext = applicationDbContext;
            _getUserCase = getUserCase;
        }
        //Verifica que haya un solo usuario, que sea diferente al que se esta actualizando
        private async Task<Result<bool>> VerificarQueHayaSoloUnUsuario(ActualizarUsuarioViewModel usuario)
        {
            bool existe = await _applicationDbContext.Usuarios.AnyAsync(u => !u.Id.Equals(usuario.Id) && u.NombreUsuario.Equals(usuario.NombreUsuario));
            return !existe ? existe : Result.Failure<bool>("Ese usuario ya esta en uso");
        }
        //Verifica que haya un solo correo, que sea diferente al que se esta actualizando
        private async Task<Result<bool>> VerificarQueHayaSoloUnCorreo(ActualizarUsuarioViewModel usuario)
        { 
            bool existe = await _applicationDbContext.DatosExtras.AnyAsync(u => !u.DatosExtraDelUsuario.Equals(usuario.Id) && u.Email.Equals(usuario.Email));
            return !existe ? existe : Result.Failure<bool>("Ese correo ya esta en uso");
        }
        private async Task<Result<bool>> Actualizar(Usuarios usuarios, ActualizarUsuarioViewModel usuarioViewModel)
        {
            //Se revisa que campos se va a cambiar, para evitar problemas de rendimiento al actualizar
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
            //Se salvan los cambios
            await _applicationDbContext.SaveChangesAsync();

            return true;
        }
        //Se valida que solo haya un usuario -> Se valida que solo haya un correo -> Se actualiza
        public async Task<Result<bool>> Execute(ActualizarUsuarioViewModel usuarioViewModel)
        {
            Usuarios usuario = (await _getUserCase.Execute(usuarioViewModel.Id)).Throw();
            return await VerificarQueHayaSoloUnUsuario(usuarioViewModel)
                         .Bind(_ => VerificarQueHayaSoloUnCorreo(usuarioViewModel))
                         .Bind(_ => Actualizar(usuario, usuarioViewModel));

        }
    }
}
