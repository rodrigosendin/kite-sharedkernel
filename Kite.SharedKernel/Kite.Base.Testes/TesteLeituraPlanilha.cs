using System;
using System.Data;
using NUnit.Framework;
using Kite.Base.Dominio.Util;

namespace Kite.Base.Testes
{
    [TestFixture]
    public class TesteLeituraPlanilha
    {
        [Test]
        public void Pode_Ler_Dados_Planilha()
        {
            // Acerte o caminho do arquivo aqui!!
            var arquivo = @"C:\Fontes\Kite\kite-sharedkernel\Kite.SharedKernel\Kite.Base.Testes\planilha.xlsx";

            var dados = ExcelTools.XlsxToDataTable(arquivo, "clientes");
            foreach (DataRow row in dados.Rows)
            {
                Console.WriteLine($"{row["Codigo"]} - {row["Nome"]}");
            }
            Assert.Greater(dados.Rows.Count, 0);
        }
    }
}