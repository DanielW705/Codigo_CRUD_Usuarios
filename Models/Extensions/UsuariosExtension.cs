using Codigo_examen.Models.Mapper;

namespace Codigo_examen.Models.Extensions
{
    public static class UsuariosExtension
    {
        public static UsuarioDto toDto(this Usuarios usuario, DatosExtra datosExtra) =>
                        new UsuarioDto(usuario.Id, usuario.NombreUsuario, datosExtra.ApellidoPaterno, datosExtra.ApellidoMaterno, datosExtra.Calle, datosExtra.NumeroExterior, datosExtra.Colonia, datosExtra.CodigoPostal, datosExtra.Municipio, datosExtra.Estado, datosExtra.Email);
        public static UsuarioDto toDto(this Usuarios usuario) =>
            new UsuarioDto(usuario.Id, usuario.NombreUsuario, usuario.DatosExtra.ApellidoPaterno, usuario.DatosExtra.ApellidoMaterno, usuario.DatosExtra.Calle, usuario.DatosExtra.NumeroExterior, usuario.DatosExtra.Colonia, usuario.DatosExtra.CodigoPostal, usuario.DatosExtra.Municipio, usuario.DatosExtra.Estado, usuario.DatosExtra.Email);
        public static List<UsuarioDto> toDto(this List<Usuarios> usuarios) =>
            usuarios.Select(toDto).ToList();
    }
}
