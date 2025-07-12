namespace Codigo_examen.Models
{
    //Objeto de paginacion con los item, el total, tamaño de la pagina, pagina actual y opcional el nombre de busqueda
    public record class Pagination<T>(List<T> Items, int TotalItems, int PageSize, int CurrentPage, string? Search);

}
