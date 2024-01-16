using System.Threading.Tasks;

namespace SafraEmprestimo.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
