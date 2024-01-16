using SafraEmprestimo.Domain.Dtos;
using SafraEmprestimo.Domain.Emprestimos;
using SafraEmprestimo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SafraEmprestimo.Service.Emprestimos
{
    public class CalculaJurosService : ICalculaJurosService
    {
        public EmprestimoDto CalculaJuros(EmprestimoDto emprestimo)
        {
            try
            {
                switch (emprestimo.TipoCredito)
                {
                    case "Direto":
                        emprestimo.ValorTotalComJuros = ((emprestimo.Valor * 2) / 100) + emprestimo.Valor;
                        emprestimo.ValorJuros = (emprestimo.Valor * 2) / 100;
                        break;
                    case "Consignado":
                        emprestimo.ValorTotalComJuros = ((emprestimo.Valor * 1) / 100) + emprestimo.Valor;
                        emprestimo.ValorJuros = (emprestimo.Valor * 1) / 100;
                        break;
                    case "PessoaFisica":
                        emprestimo.ValorTotalComJuros = ((emprestimo.Valor * 5) / 100) + emprestimo.Valor;
                        emprestimo.ValorJuros = (emprestimo.Valor * 5) / 100;
                        break;
                    case "Imobiliario":
                        emprestimo.ValorTotalComJuros = ((emprestimo.Valor * 3) / 100) + emprestimo.Valor;
                        emprestimo.ValorJuros = (emprestimo.Valor * 3) / 100;
                        break;
                    default:
                        emprestimo.ValorTotalComJuros = ((emprestimo.Valor * 9) / 100) + emprestimo.Valor;
                        emprestimo.ValorJuros = (emprestimo.Valor * 9) / 100;
                        break;
                }

                emprestimo.StatusCredito = "Liberado";
            }
            catch(Exception ex)
            {
                emprestimo.StatusCredito = "Erro";
            }
            return emprestimo;
        }
    }
}
