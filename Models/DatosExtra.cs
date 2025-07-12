using System.ComponentModel.DataAnnotations;

namespace Codigo_examen.Models
{
    public class DatosExtra
    {
        public int Id { get; set; }
        //Se obliga a ser escrito y tenga maximo 30 caracteres
        [Required(ErrorMessage = "Es necesario un apellido paterno")]
        [MaxLength(30, ErrorMessage = "El apellido paterno no debe superar el limite caracteres")]
        public string ApellidoPaterno { get; set; }
        //Es opcional y debe tener maximo 30 caracteres
        [MaxLength(30, ErrorMessage = "El apellido materno no debe superar el limite caracteres")]
        public string? ApellidoMaterno { get; set; }
        //Se obliga a ser escrito y tenga maximo 30 caracteres
        [Required(ErrorMessage = "Es necesario una calle")]
        [MaxLength(30, ErrorMessage = "La calle no debe superar el limite caracteres")]
        public string Calle { get; set; }
        //Es opcional y debe estar en el rango de 1 a 999
        [Range(1, 999, ErrorMessage = "El numero exterior debe estar en los limites")]
        public int? NumeroExterior { get; set; }
        //Se obliga a ser escrito y tenga maximo 50 caracteres
        [Required(ErrorMessage = "Es necesario una colonia")]
        [MaxLength(50, ErrorMessage = "La colonia no debe superar el limite de caracteres")]
        public string Colonia { get; set; }
        //Se obliga a ser escrito y debe estar en el rango de 1000 a 99999
        [Required(ErrorMessage = "Es necesario un codigo postal")]
        [Range(1000, 99999, ErrorMessage = "El codigo postal debe estar en los limites")]
        public int CodigoPostal { get; set; }
        //Es opcional y debe tener 50 caracteres
        [MaxLength(50, ErrorMessage = "La colonia no debe superar el limite caracteres")]
        public string? Municipio { get; set; }
        //Es opcional y debe tener 50 caracteres
        [MaxLength(50, ErrorMessage = "La colonia no debe superar el limite caracteres")]
        public string? Estado { get; set; }
        //Se requiere el campo
        [Required(ErrorMessage = "Es necesario un correo electronico")]
        public string Email { get; set; }
        // Tiene una relacion con los datos extra este es el campo que lo guarda
        public Guid DatosExtraDelUsuario { get; set; }
        // Esta es la relacion con el usuario
        public Usuarios usuario { get; set; }

    }
}
