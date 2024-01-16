using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafraEmprestimo.Domain._Base
{
    public static class Resource
    {
        public static string EmprestimoJaCadastrado = "Emprestimo já cadastrado.";
        public static string ValorEstourado = "Valor do crédito não pode ser superior a 1.000.000,00.";
        public static string TipoCreditoInvalido = "Tipo de crédito inválido.";
        public static string QuantidadeParcelasInvalida = "Quantidade de parcelas inválida.";
        public static string QuantidadeMinimaParcelas = "Quantidade miníma de parcelas deve ser igual ou superior a 5 parcelas.";
        public static string QuantidadeMaximaParcelas = "Quantidade máxima de parcelas não pode exceder 72 parcelas.";
        public static string PessoaJuridicaValorMinimo = "O valor mínimo para pessoa jurídica é de 15.000,00.";
        public static string PrimeiroVencimentoMinimoErrado = "A data de vencimento não pode ser inferior a 15 dias a partir da data atual.";
        public static string PrimeiroVencimentoMaximaErrada = "A data de vencimento não pode ser superior a 40 dias a partir da data atual.";
        public static string ValorTotalDeJurosInvalido = "Valor Total de Juros não pode ser 0 (zero) ou nulo.";
        public static string ValorDeJurosInvalido = "Valor de Juros não pode ser 0 (zero) ou nulo.";
    }
}
