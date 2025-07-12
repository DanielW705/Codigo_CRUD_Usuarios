using Codigo_examen.Data;
using Codigo_examen.Models;
using ROP;
using System.Threading.Tasks;

namespace Codigo_examen.UseCase
{
    public class DeleteUserCase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        private readonly GetUserCase _getUserCase;
        public DeleteUserCase(ApplicationDbContext applicationDbContext, GetUserCase getUserCase)
        {
            _applicationDbContext = applicationDbContext;
            _getUserCase = getUserCase;
        }

        private async Task<Result<bool>> DeleteUser(Usuarios usuario)
        {
            usuario.IsDeleted = true;
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Result<bool>> Execute(Guid Id) => await _getUserCase.Execute(Id).Bind(DeleteUser);
    }
}
