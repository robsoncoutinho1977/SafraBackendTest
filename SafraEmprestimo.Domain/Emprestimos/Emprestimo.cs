using SafraEmprestimo.Domain._Base;
using SafraEmprestimo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafraEmprestimo.Domain.Emprestimos
{
    public class Emprestimo : Entidade
    {
        public string TipoCredito { get; private set; }
        public int QuantidadeParcelas { get; private set; }
        public DateTime DataPrimeiroVencimento { get; private set; }
        public string StatusCredito { get; private set; }
        public double Valor { get; private set; }
        public double ValorTotalComJuros { get; private set; }
        public double ValorJuros { get; private set; }

        private Emprestimo() { }

        public Emprestimo(string tipoCredito, int quantidadeParcelas, DateTime dataPrimeiroVencimento, string statusCredito,
            double valor = 0, double valorTotalComJuros = 0, double valorJuros = 0)
        {
            ValidadorDeRegra.Novo()
                .Quando(valor > 1000000, Resource.ValorEstourado)
                .Quando(quantidadeParcelas < 5, Resource.QuantidadeMinimaParcelas)
                .Quando(quantidadeParcelas > 72, Resource.QuantidadeMaximaParcelas)
                .Quando(tipoCredito == "PessoaJuridica" && valor < 15000, Resource.PessoaJuridicaValorMinimo)
                .Quando(dataPrimeiroVencimento < DateTime.Now.AddDays(15), Resource.PrimeiroVencimentoMinimoErrado)
                .Quando(dataPrimeiroVencimento > DateTime.Now.AddDays(40), Resource.PrimeiroVencimentoMaximaErrada)
                .DispararExcecaoSeExistir();

            TipoCredito = tipoCredito;
            QuantidadeParcelas = quantidadeParcelas;
            DataPrimeiroVencimento = dataPrimeiroVencimento;
            Valor = valor;
            StatusCredito = statusCredito;
        }

    }
}
