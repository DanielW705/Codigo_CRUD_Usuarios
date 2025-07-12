using System.ComponentModel.DataAnnotations;

namespace Codigo_examen.Models
{
    public class Usuarios
    {
        public Guid Id { get; set; }
        //Es opcional y debe tener 30 caracteres
        [Required(ErrorMessage = "Se requiere un nombre de usuario")]
        [MaxLength(30, ErrorMessage = "El usuario no puede ser mayor a 30 caracteres")]
        public string NombreUsuario { get; set; }
        //Es opcional y debe tener minimo 10 caracteres a maximo 8
        [Required(ErrorMessage = "Se require una contraseña")]
        [MaxLength(10, ErrorMessage = "La contraseña puede ser mayor a 10 caracteres")]
        [MinLength(8, ErrorMessage = "La contraseña no puede ser menor a 8 caracteres")]
        public string Contrasena { get; set; }
        //Campo para el borrado suave
        public bool IsDeleted { get; set; } = false;
        //Relacion entre los usuarios y los datos extra
        public DatosExtra DatosExtra { get; set; }
    }
}
