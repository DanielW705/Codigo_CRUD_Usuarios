using System.ComponentModel.DataAnnotations;

namespace Codigo_examen.Models.ViewModel
{
    public class CreateUsuarioViewModel
    {
        [Required(ErrorMessage = "Se requiere un nombre de usuario")]
        [MaxLength(30, ErrorMessage = "El usuario no puede ser mayor a 30 caracteres")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Se require una contraseña")]
        [MaxLength(10, ErrorMessage = "La contraseña puede ser mayor a 10 caracteres")]
        [MinLength(8, ErrorMessage = "La contraseña no puede ser menor a 8 caracteres")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "Es necesario un apellido paterno")]
        [MaxLength(30, ErrorMessage = "El apellido paterno no debe superar el limite caracteres")]
        public string ApellidoPaterno { get; set; }
        [MaxLength(30, ErrorMessage = "El apellido materno no debe superar el limite caracteres")]
        public string? ApellidoMaterno { get; set; }
        [Required(ErrorMessage = "Es necesario una calle")]
        [MaxLength(30, ErrorMessage = "La calle no debe superar el limite caracteres")]
        public string Calle { get; set; }
        [Range(1, 999, ErrorMessage = "El numero exterior debe estar en los limites")]
        public int? NumeroExterior { get; set; }
        [Required(ErrorMessage = "Es necesario una colonia")]
        [MaxLength(50, ErrorMessage = "La colonia no debe superar el limite de caracteres")]
        public string Colonia { get; set; }
        [Required(ErrorMessage = "Es necesario un codigo postal")]
        [Range(1000, 99999, ErrorMessage = "El codigo postal debe estar en los limites")]
        public int CodigoPostal { get; set; }
        [MaxLength(50, ErrorMessage = "La colonia no debe superar el limite caracteres")]
        public string? Municipio { get; set; }
        [MaxLength(50, ErrorMessage = "La colonia no debe superar el limite caracteres")]
        public string? Estado { get; set; }
        [Required(ErrorMessage = "Es necesario un correo electronico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Se require una contraseña")]
        [MaxLength(10, ErrorMessage = "La contraseña puede ser mayor a 10 caracteres")]
        [MinLength(8, ErrorMessage = "La contraseña no puede ser menor a 8 caracteres")]
        public string ConfirmacionContraseña { get; set; }
    }
}
