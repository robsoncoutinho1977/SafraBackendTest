using Bogus;
using Moq;
using SafraEmprestimo.Domain._Base;
using SafraEmprestimo.Domain.Dtos;
using SafraEmprestimo.Domain.Emprestimos;
using SafraEmprestimo.Domain.Enums;
using SafraEmprestimo.Domain.Interfaces;
using SafraEmprestimo.DomainTest._Builders;
using SafraEmprestimo.DomainTest._Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafraEmprestimo.Service.Emprestimos
{
    public class ArmazenadorDeEmprestimoTest
    {
        private readonly EmprestimoDto _emprestimoDto;
        private readonly ArmazenadorDeEmprestimo _armazenadorDeEmprestimo;
        private readonly Mock<IEmprestimoRepositorio> _emprestimoRepositorioMock;

        public ArmazenadorDeEmprestimoTest()
        {
            var _faker = new Faker();
            _emprestimoDto = new EmprestimoDto
            {
                TipoCredito = "PessoaJuridica",
                QuantidadeParcelas = _faker.Random.Number(1, 72),
                DataPrimeiroVencimento = _faker.Date.Between(DateTime.Now.AddDays(1), DateTime.Now.AddDays(50)),
                StatusCredito = "Analisando",
                Valor = (double)_faker.Random.Double(50, 1000000)
            };

            _emprestimoRepositorioMock = new Mock<IEmprestimoRepositorio>();
            _armazenadorDeEmprestimo = new ArmazenadorDeEmprestimo(_emprestimoRepositorioMock.Object);
        }

        [Fact]
        public void DeveAdicionarEmprestimo()
        {
            _armazenadorDeEmprestimo.Armazenar(_emprestimoDto);

            _emprestimoRepositorioMock.Verify(r => r.Adicionar(
                It.Is<Emprestimo>(
                    c => c.Id == _emprestimoDto.Id
                )
            ));
        }

        [Fact]
        public void NaoDeveAdicioonarEmprestimoSemValorTotalJuros()
        {
            var ValorTotalJurosInvalido = 0;
            _emprestimoDto.ValorTotalComJuros = ValorTotalJurosInvalido;

            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeEmprestimo.Armazenar(_emprestimoDto))
                .ComMensagem(Resource.ValorTotalDeJurosInvalido);
        }

        [Fact]
        public void NaoDeveAdicioonarEmprestimoSemValorJuros()
        {
            var ValorJurosInvalido = 0;
            _emprestimoDto.ValorJuros = ValorJurosInvalido;

            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeEmprestimo.Armazenar(_emprestimoDto))
                .ComMensagem(Resource.ValorDeJurosInvalido);
        }

    }
}
