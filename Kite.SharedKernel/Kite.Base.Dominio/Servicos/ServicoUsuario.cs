using Kite.Base.Dominio.Repositorio;
using Kite.Base.Dominio.Entidades;
using Kite.Base.Dominio.Util;
using Kite.Base.Dominio.ViewModels;
using System.Linq;
using System;

namespace Kite.Base.Dominio.Servicos
{
    public class ServicoUsuario : Servico<Usuario>
    {
        public ServicoUsuario(IRepositorioHelper helper) : base(helper)
        {
        }

        public override bool Inclui(Usuario entidade, string usuario = "sistema")
        {
            entidade.Senha = CryptoTools.ComputeHashMd5(entidade.Senha);
            return base.Inclui(entidade);
        }

        public override bool Altera(Usuario entidade, string usuario = "sistema")
        {
            entidade.Senha = Retorna(entidade.Id).Senha;
            return base.Altera(entidade);
        }

        public override bool Salva(Usuario entidade, string usuario = "sistema")
        {
            entidade.Senha = entidade.Id == 0
                ? CryptoTools.ComputeHashMd5(entidade.Senha)
                : Retorna(entidade.Id).Senha;
            return base.Salva(entidade);
        }

        public Token Login(string login, string senha)
        {
            senha = CryptoTools.ComputeHashMd5(senha);
            var usuario = Consulta(x => x.Login.ToUpper() == login.ToUpper() && x.Senha == senha).FirstOrDefault();

            var token = new Token
            {
                UsuarioId = usuario.Id,
                UsuarioNome = usuario.Nome,
                Login = usuario.Login,
                Data = DateTime.Now
            };

            return token;
        }
    }
}
