namespace Codigo_examen.Models.Mapper
{
    //Este es una mapeado, para el uso y facilitar la manipulacion de algunos datos
    public record class UsuarioDto(Guid Id, string Nombre, string ApellidoPaterno, string? ApellidoMaterno, string Calle, int? NumeroExterior, string Colonia, int CodigoPostal, string? Municipio, string? Estado, string Email);
}
