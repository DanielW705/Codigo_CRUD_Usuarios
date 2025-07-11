namespace Codigo_examen.Models
{
    public record class Pagination<T>(List<T> Items, int TotalItems, int PageSize, int CurrentPage, string? Search);

}
