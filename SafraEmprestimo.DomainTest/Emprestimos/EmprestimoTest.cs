using Bogus;
using ExpectedObjects;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using SafraEmprestimo.Domain._Base;
using SafraEmprestimo.Domain.Emprestimos;
using SafraEmprestimo.Domain.Enums;
using SafraEmprestimo.DomainTest._Builders;
using SafraEmprestimo.DomainTest._Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafraEmprestimo.DomainTest.Emprestimos
{
    public class EmprestimoTest
    {
        private readonly string _tipoCredito;
        private readonly int _quantidadeParcelas;
        private readonly DateTime _dataPrimeiroVencimento;
        private readonly string _statusCredito;
        private readonly double _valor;
        private readonly double _valorTotalComJuros;
        private readonly double _valorJuros;
        private readonly Faker _faker;

        public EmprestimoTest()
        {
            _faker = new Faker();

            _tipoCredito = "PessoaJuridica";
            _quantidadeParcelas = _faker.Random.Number(1, 72);
            _dataPrimeiroVencimento = _faker.Date.Between(DateTime.Now.AddDays(1), DateTime.Now.AddDays(50));
            _statusCredito = "Analisando";
            _valor = (double)_faker.Random.Double(50, 1000000);
        }

        [Fact]
        public void DeveCriarEmprestimo()
        {
            var emprestimoEsperado = new
            {
                TipoCredito = _tipoCredito,
                QuantidadeParcelas = _quantidadeParcelas,
                DataPrimeiroVencimento = _dataPrimeiroVencimento,
                StatusCredito = _statusCredito,
                Valor = _valor
            };

            var emprestimo = new Emprestimo(
                emprestimoEsperado.TipoCredito,
                emprestimoEsperado.QuantidadeParcelas,
                emprestimoEsperado.DataPrimeiroVencimento,
                emprestimoEsperado.StatusCredito,
                emprestimoEsperado.Valor);

            emprestimoEsperado.ToExpectedObject().ShouldMatch(emprestimo);
        }

        [Theory]
        [InlineData(1000001)]
        public void NaoDeveCriarEmprestimoSeValorForSuperiorA1000000(double valor)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                EmprestimoBuilder.Novo().ComValor(valor).Build())
                .ComMensagem(Resource.ValorEstourado);
        }

        [Theory]
        [InlineData(0)]
        public void NaoDeveCriarEmprestimoSeQuantidadeMinimaParcelasInvalido(int quantidadeParcelas)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                EmprestimoBuilder.Novo().ComQuantidadeMinimaParcelas(quantidadeParcelas).Build())
                .ComMensagem(Resource.QuantidadeMinimaParcelas);
        }

        [Theory]
        [InlineData(80)]
        public void NaoDeveCriarEmprestimoSeQuantidadeMaximaParcelasInvalida(int quantidadeParcelas)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                EmprestimoBuilder.Novo().ComQuantidadeMaximaParcelas(quantidadeParcelas).Build())
                .ComMensagem(Resource.QuantidadeMaximaParcelas);
        }

        [Theory]
        [InlineData("PessoaJuridica", 4000)]
        public void NaoDeveCriarEmprestimoSePessoaJuridicaEValorInferior15k(string tipoCredito, double valor)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                EmprestimoBuilder.Novo().ComTipoCredito(tipoCredito).ComValor(valor).Build())
                .ComMensagem(Resource.PessoaJuridicaValorMinimo);
        }

        [Theory]
        [InlineData("2024-01-25")]
        public void NaoDeveCriarEmprestimoSeVencimentoMenorQue15Dias(DateTime dataPrimeiroVencimento)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                EmprestimoBuilder.Novo().ComDataPrimeiroVencimento(dataPrimeiroVencimento).Build())
                .ComMensagem(Resource.PrimeiroVencimentoMinimoErrado);
        }

        [Theory]
        [InlineData("2024-03-15")]
        public void NaoDeveCriarEmprestimoSeVencimentoMaiorQue72Dias(DateTime dataPrimeiroVencimento)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                EmprestimoBuilder.Novo().ComDataPrimeiroVencimento(dataPrimeiroVencimento).Build())
                .ComMensagem(Resource.PrimeiroVencimentoMaximaErrada);
        }
    }
}
