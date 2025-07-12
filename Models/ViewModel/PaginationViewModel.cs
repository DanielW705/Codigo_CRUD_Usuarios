using Codigo_examen.Models.Mapper;

namespace Codigo_examen.Models.ViewModel
{
    //Este es un modelo para la vista usado para la muestra de la vista
    public class PaginationViewModel
    {
        public Pagination<UsuarioDto> Pagination { get; set; }

        public int[] SelectedOption { get; set; } = [5, 10, 15];
    }
}
