using SafraEmprestimo.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafraEmprestimo.Domain.Interfaces
{
    public interface ICalculaJurosService
    {
        public EmprestimoDto CalculaJuros(EmprestimoDto emprestimo);
    }
}
