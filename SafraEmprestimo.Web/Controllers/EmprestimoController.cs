using Microsoft.AspNetCore.Mvc;
using SafraEmprestimo.Domain.Dtos;
using SafraEmprestimo.Domain.Emprestimos;
using SafraEmprestimo.Domain.Enums;
using SafraEmprestimo.Domain.Interfaces;
using SafraEmprestimo.Service.Emprestimos;
using SafraEmprestimo.Web.Util;
using System.Drawing;

namespace SafraEmprestimo.Web.Controllers
{
    public class EmprestimoController : Controller
    {
        private readonly ArmazenadorDeEmprestimo _armazenadorDeEmprestimo;
        private readonly IRepositorio<Emprestimo> _emprestimoRepositorio;
        

        public EmprestimoController(ArmazenadorDeEmprestimo armazenadorDeEmprestimo, IRepositorio<Emprestimo> emprestimoRepositorio)
        {
            _armazenadorDeEmprestimo = armazenadorDeEmprestimo;
            _emprestimoRepositorio = emprestimoRepositorio;            
        }

        public IActionResult Index()
        {
            var emprestimos = _emprestimoRepositorio.Consultar();

            if (emprestimos.Any())
            {
                var dtos = emprestimos.Select(c => new EmprestimoParaListagemDto
                {
                    Id = c.Id,
                    TipoCredito = c.TipoCredito,
                    QuantidadeParcelas = c.QuantidadeParcelas,
                    DataPrimeiroVencimento = c.DataPrimeiroVencimento,
                    StatusCredito = c.StatusCredito,
                    Valor = c.Valor,
                    ValorTotalComJuros = c.ValorTotalComJuros,
                    ValorJuros = c.ValorJuros
                });
                return View("Index", PaginatedList<EmprestimoParaListagemDto>.Create(dtos, Request));
            }

            return View("Index", PaginatedList<EmprestimoParaListagemDto>.Create(null, Request));
        }

        public IActionResult Editar(int id)
        {
            var emprestimo = _emprestimoRepositorio.ObterPorId(id);
            var dto = new EmprestimoDto
            {
                Id = emprestimo.Id,
                TipoCredito = emprestimo.TipoCredito,
                QuantidadeParcelas = emprestimo.QuantidadeParcelas,
                DataPrimeiroVencimento = emprestimo.DataPrimeiroVencimento,
                Valor = emprestimo.Valor,
                ValorTotalComJuros = emprestimo.ValorTotalComJuros,
                ValorJuros = emprestimo.ValorJuros,
                StatusCredito = emprestimo.StatusCredito
        };

            return View("NovoOuEditar", dto);
        }

        public IActionResult Novo()
        {
            return View("NovoOuEditar", new EmprestimoDto());
        }

        [HttpPost]
        public IActionResult Salvar(EmprestimoDto model)
        {
            _armazenadorDeEmprestimo.Armazenar(model);

            return Ok();
        }

    }
}
