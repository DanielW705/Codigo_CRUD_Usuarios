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

        public GetPaginatedClassUseCase(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        private Result<Unit> ValidatePage(int page) => page < 1 ? Result.Failure("La pagina no esta soportada") : Result.Unit;

        private Result<Unit> ValidatePageSize(int pageSize)
        {
            int[] allowedValues = [5, 10, 15];
            return !allowedValues.Contains(pageSize) ?
             Result.Failure("La pagina no esta soportada") :
             Result.Unit;
        }
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
        private async Task<Result<int>> TotalElements(string? search) =>
        !string.IsNullOrEmpty(search) ?
         await _applicationDbContext.Usuarios
            .Where(u => u.NombreUsuario.Contains(search))
            .CountAsync() :
            await _applicationDbContext.Usuarios.CountAsync();

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
