using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafraEmprestimo.Domain.Dtos
{
    public class EmprestimoParaListagemDto
    {
        public int Id { get; set; }
        public string TipoCredito { get; set; }
        public int QuantidadeParcelas { get; set; }
        public DateTime DataPrimeiroVencimento { get; set; }
        public string StatusCredito { get; set; }
        public double Valor { get; set; }
        public double ValorTotalComJuros { get; set; }
        public double ValorJuros { get; set; }
    }
}
