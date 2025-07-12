using Codigo_examen.Data;
using Codigo_examen.Models;
using Codigo_examen.Models.Extensions;
using Codigo_examen.Models.Mapper;
using Microsoft.EntityFrameworkCore;
using ROP;

namespace Codigo_examen.UseCase
{
    public class GetPaginatedClassUseCase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        //constructor para ser inyectado con los servicios
        public GetPaginatedClassUseCase(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        //Se valida el tamaño minimo de la pagina
        private Result<Unit> ValidatePage(int page) => page < 1 ? Result.Failure("La pagina no esta soportada") : Result.Unit;
        //Se valida la cantidad que se muestra (en este caso 5 en 5)
        private Result<Unit> ValidatePageSize(int pageSize)
        {
            int[] allowedValues = [5, 10, 15];
            return !allowedValues.Contains(pageSize) ?
             Result.Failure("La pagina no esta soportada") :
             Result.Unit;
        }
        //Se obtiene los usuarios por la base de datos, tomando en cuenta el ultimo que se tomo y la cantidad que se piden
        private async Task<Result<List<Usuarios>>> GetFromDb(string? search, int page, int size)
        {
            var query = _applicationDbContext.Usuarios
                  .Include(u => u.DatosExtra)
                 .Skip(size * (page - 1))
                 .Take(size);
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(u => u.NombreUsuario.Contains(search));

            return await query.ToListAsync();
        }
        //Se cuentan el total de elementos
        private async Task<Result<int>> TotalElements(string? search) =>
        !string.IsNullOrEmpty(search) ?
         await _applicationDbContext.Usuarios
            .Where(u => u.NombreUsuario.Contains(search))
            .CountAsync() :
            await _applicationDbContext.Usuarios.CountAsync();
        // Valida la paginacion-> valida si es del tamaño correcto la muestra -> revisa que la paginacion no supere el maximo-> busca en la base de datos->Mapea para su proximo uso -> combina el tamaño y los resultados -> vuelve a mapear
        //                     -> si falla, la pagina vuelve a 1 -> valida si es del tamaño correcto la muestra -> revisa que la paginacion no supere el maximo-> busca en la base de datos->Mapea para su proximo uso -> combina el tamaño y los resultados -> vuelve a mapear
        //                                                                                                                                                     -> si falla, la pagina vuelve a 5-> busca en la base de datos->Mapea para su proximo uso -> combina el tamaño y los resultados -> vuelve a mapear
        public async Task<Result<Pagination<UsuarioDto>>> Execute(string? search, int page, int size)
            => await ValidatePage(page)
            .Fallback(_ =>
            {
                page = 1;
                return Result.Unit;
            }).Bind(_ => ValidatePageSize(page))
            .Fallback(_ =>
            {
                size = 5;
                return Result.Unit;
            }).Async()
            .Bind(x => GetFromDb(search, page, size))
            .Map(x => x.toDto())
            .Combine(x => TotalElements(search))
            .Map(x => new Pagination<UsuarioDto>(x.Item1, x.Item2, size, page, search));

    }
}
