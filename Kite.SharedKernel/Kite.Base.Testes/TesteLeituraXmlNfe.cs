using System;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using Kite.Base.Dominio.Util;

namespace Kite.Base.Testes
{
    [TestFixture]
    public class TesteLeituraXmlNfe
    {
        [Test]
        public void Pode_Ler_Dados_Xml()
        {
            // Acerte o caminho do arquivo aqui!!
            var arquivo = @"C:\Fontes\Kite\kite-sharedkernel\Kite.SharedKernel\Kite.Base.Testes\nfe.xml";

            var xml = XElement.Load(arquivo);

            // Pega tag NFe
            var xmlNotas = xml.Elements().FirstOrDefault(e => e.Name.LocalName == "NFe");
            if (xmlNotas == null)
            {
                Console.WriteLine("XML Inválido!");
                return;
            }

            // Pega tag infNFe
            var xmlNota = xmlNotas.Elements().FirstOrDefault(e => e.Name.LocalName == "infNFe");
            if (xmlNota == null)
            {
                Console.WriteLine("XML Inválido!");
                return;
            }

            var versao          = xmlNota.GetAtributo("versao");
            var identificador   = xmlNota.GetAtributo("Id");

            Console.WriteLine(versao);
            Console.WriteLine(identificador);

            var xmlIde = xmlNota.Elements().FirstOrDefault(e => e.Name.LocalName == "ide");

            var nroNf = xmlIde.GetStringDeFilho("nNF");
            Console.WriteLine(nroNf);

        }
    }
}