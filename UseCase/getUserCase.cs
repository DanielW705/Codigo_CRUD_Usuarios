using Codigo_examen.Data;
using Codigo_examen.Models;
using Microsoft.EntityFrameworkCore;
using ROP;

namespace Codigo_examen.UseCase
{
    public class GetUserCase
    {
        public readonly ApplicationDbContext _applicationDbContext;

        public GetUserCase(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        private async Task<Result<Usuarios>> GetUserByGuid(Guid id) => await _applicationDbContext.Usuarios
                                                                        .Include(u => u.DatosExtra)
                                                                        .FirstAsync(u => u.Id.Equals(id));

        public async Task<Result<Usuarios>> Execute(Guid id) => await GetUserByGuid(id);
    }
}
