using SafraEmprestimo.Domain._Base;
using SafraEmprestimo.Domain.Dtos;
using SafraEmprestimo.Domain.Emprestimos;
using SafraEmprestimo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafraEmprestimo.Service.Emprestimos
{
    public class ArmazenadorDeEmprestimo
    {
        private readonly IEmprestimoRepositorio _emprestimoRepositorio;
        private readonly ICalculaJurosService _calculadoraJurosService;
        private IEmprestimoRepositorio @object;

        public ArmazenadorDeEmprestimo(IEmprestimoRepositorio @object)
        {
            this.@object = @object;
        }

        public ArmazenadorDeEmprestimo(IEmprestimoRepositorio emprestimoRepositorio, ICalculaJurosService calculadoraJurosService)
        {
            _emprestimoRepositorio = emprestimoRepositorio;
            _calculadoraJurosService = calculadoraJurosService;
        }

        public void Armazenar(EmprestimoDto emprestimoDto)
        {
            var emprestimoJaSalvo = _emprestimoRepositorio.ObterPeloId(emprestimoDto.Id);

            ValidadorDeRegra.Novo()
                .Quando(emprestimoJaSalvo != null && emprestimoJaSalvo.Id != emprestimoJaSalvo.Id, Resource.EmprestimoJaCadastrado)
                .DispararExcecaoSeExistir();

            var emprestimoCalculado = _calculadoraJurosService.CalculaJuros(emprestimoDto);

            var emprestimo =
                new Emprestimo(
                    emprestimoDto.TipoCredito,
                    emprestimoDto.QuantidadeParcelas,
                    emprestimoDto.DataPrimeiroVencimento,
                    emprestimoCalculado.StatusCredito,
                    emprestimoDto.Valor,
                    emprestimoCalculado.ValorTotalComJuros,
                    emprestimoCalculado.ValorJuros);

            if (emprestimoDto.Id == 0)
                _emprestimoRepositorio.Adicionar(emprestimo);
        }
    }
}
