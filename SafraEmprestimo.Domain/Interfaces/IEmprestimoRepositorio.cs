using SafraEmprestimo.Domain.Emprestimos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafraEmprestimo.Domain.Interfaces
{
    public interface IEmprestimoRepositorio : IRepositorio<Emprestimo>
    {
        Emprestimo ObterPeloId(int id);
    }
}
