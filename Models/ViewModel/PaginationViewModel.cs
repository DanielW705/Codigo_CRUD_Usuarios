using Codigo_examen.Models.Mapper;

namespace Codigo_examen.Models.ViewModel
{
    public class PaginationViewModel
    {
        public Pagination<UsuarioDto> Pagination { get; set; }

        public int[] SelectedOption { get; set; } = [5, 10, 15];
    }
}
