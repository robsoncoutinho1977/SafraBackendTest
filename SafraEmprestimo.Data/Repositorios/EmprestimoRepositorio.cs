using SafraEmprestimo.Data.Contextos;
using SafraEmprestimo.Domain.Emprestimos;
using SafraEmprestimo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafraEmprestimo.Data.Repositorios
{
    public class EmprestimoRepositorio : RepositorioBase<Emprestimo>, IEmprestimoRepositorio
    {
        public EmprestimoRepositorio(ApplicationDbContext context) : base(context)
        {
        }

        public Emprestimo ObterPeloId(int id)
        {
            var entidade = Context.Set<Emprestimo>().Where(c => c.Id == id);
            if (entidade.Any())
                return entidade.First();
            return null;
        }
    }
}
