using System;
using System.Data;
using NUnit.Framework;
using Kite.Base.Dominio.Util;
using Kite.Base.Repositorio;

namespace Kite.Base.Testes
{
    [TestFixture]
    public class TesteLeituraSqlServer
    {
        [Test]
        public void Pode_Ler_Dados_SqlServer()
        {
            // Acerte a Connection String!! Está apontando para um SQLServer local, database master
            // Use essa Connection String para autenticação com usuário SA (ou outro usuário do SQLServer)
            //var cs = "Server=(local); initial catalog=master; user id=sa; password=MinhaSenha";

            var cs = "Server=(local); initial catalog=master; Trusted_Connection=True;";
            
            var repositorio = new RepositorioSqlClient(cs);

            var comando = "SELECT * From information_schema.tables";

            var dados = repositorio.ConsultaDataTable(comando);
            foreach (DataRow row in dados.Rows)
            {
                var nome = row["TABLE_NAME"].ToString();
                Console.WriteLine(nome);
            }
        }
    }
}