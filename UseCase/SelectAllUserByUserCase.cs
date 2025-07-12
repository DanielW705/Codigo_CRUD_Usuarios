using Codigo_examen.Data;
using Codigo_examen.Models.Extensions;
using Codigo_examen.Models.Mapper;
using Microsoft.EntityFrameworkCore;
using ROP;

namespace Codigo_examen.UseCase
{
    public class SelectAllUserByUserCase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public SelectAllUserByUserCase(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Result<List<UsuarioDto>>> SelectAllUserByUser() =>
            await _applicationDbContext.Usuarios
                .Include(u => u.DatosExtra)
                .Select(u => UsuariosExtension.toDto(u))
                .AsNoTracking()
                .ToListAsync();
        async Task<Result<List<UsuarioDto>>> Execute() => await SelectAllUserByUser();

    }
}
