using SafraEmprestimo.Domain.Emprestimos;
using SafraEmprestimo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafraEmprestimo.DomainTest._Builders
{
    public class EmprestimoBuilder
    {
        private int _id;
        private string _tipoCredito = "PessoaJuridica";
        public int _quantidadeParcelas = 10;
        public DateTime _dataPrimeiroVencimento = DateTime.Now;
        public double _valor = (double)1000;
        public string _statusCredito = "Analisando";
        public double _valorTotalComJuros = (double)1100;
        public double _valorJuros = (double)10;

        public static EmprestimoBuilder Novo()
        {
            return new EmprestimoBuilder();
        }

        public EmprestimoBuilder ComTipoCredito(string tipoCredito)
        {
            _tipoCredito = tipoCredito;
            return this;
        }

        public EmprestimoBuilder ComQuantidadeMinimaParcelas(int quantidadeParcelas)
        {
            _quantidadeParcelas = quantidadeParcelas;
            return this;
        }

        public EmprestimoBuilder ComQuantidadeMaximaParcelas(int quantidadeParcelas)
        {
            _quantidadeParcelas = quantidadeParcelas;
            return this;
        }

        public EmprestimoBuilder ComDataPrimeiroVencimento(DateTime primeiroVencimento)
        {
            _dataPrimeiroVencimento = primeiroVencimento;
            return this;
        }

        public EmprestimoBuilder ComValor(double valor)
        {
            _valor = valor;
            return this;
        }

        public EmprestimoBuilder ComValorTotalJuros(double valor)
        {
            _valorTotalComJuros = valor;
            return this;
        }

        public EmprestimoBuilder ComValorJuros(double valor)
        {
            _valorJuros = valor;
            return this;
        }

        public EmprestimoBuilder ComId(int id)
        {
            _id = id;
            return this;
        }

        public Emprestimo Build()
        {
            var emprestimo = new Emprestimo(_tipoCredito, _quantidadeParcelas, _dataPrimeiroVencimento, _statusCredito, _valor, _valorTotalComJuros, _valorJuros);

            if (_id > 0)
            {
                var propertyInfo = emprestimo.GetType().GetProperty("Id");
                propertyInfo.SetValue(emprestimo, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }

            return emprestimo;
        }
    }
}
