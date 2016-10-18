using System;
using Kite.Base.Dominio.Entidades;
using Kite.Base.Dominio.Repositorio;

namespace Kite.Base.Dominio.Servicos
{
    public class Servico<T> : ServicoConsulta<T> where T : EntidadeBase, IAggregateRoot
    {
        public Servico(IRepositorioHelper helper) : base(helper)
        {
        }

        public virtual bool Inclui(T entidade, string usuario = "sistema")
        {
            Mensagens.Clear();
            var valido = Valida(entidade);
            if (valido == false) return false;

            using (var sessao = Helper.AbrirSessao())
            {
                var repo = sessao.GetRepositorio<T>();
                try
                {
                    sessao.IniciaTransacao();
                    entidade.DataInclusao = DateTime.Now;
                    entidade.UsuarioInclusao = usuario;
                    repo.Inclui(entidade);
                    sessao.ComitaTransacao();
                    return true;
                }
                catch (Exception ex)
                {
                    sessao.RollBackTransacao();
                    Mensagens.Add(ex.InnerException?.Message ?? ex.Message);
                    return false;
                }
            }
        }

        public virtual bool Altera(T entidade, string usuario = "sistema")
        {
            Mensagens.Clear();
            if (Valida(entidade) == false) return false;

            using (var sessao = Helper.AbrirSessao())
            {
                var repo = sessao.GetRepositorio<T>();
                try
                {
                    sessao.IniciaTransacao();
                    entidade.DataAlteracao = DateTime.Now;
                    entidade.UsuarioAlteracao = usuario;
                    repo.Altera(entidade);
                    sessao.ComitaTransacao();
                    return true;
                }
                catch (Exception ex)
                {
                    sessao.RollBackTransacao();
                    Mensagens.Add(ex.InnerException?.Message ?? ex.Message);
                    return false;
                }
            }
        }

        public virtual bool Exclui(long id)
        {
            Mensagens.Clear();
            using (var sessao = Helper.AbrirSessao())
            {
                var repo = sessao.GetRepositorio<T>();
                try
                {
                    sessao.IniciaTransacao();
                    var entidade = repo.Retorna(id);
                    repo.Exclui(entidade);
                    sessao.ComitaTransacao();
                    return true;
                }
                catch (Exception ex)
                {
                    sessao.RollBackTransacao();
                    Mensagens.Add(ex.InnerException?.Message ?? ex.Message);
                    return false;
                }
            }
        }

        public virtual bool Salva(T entidade, string usuario = "sistema")
        {
            Mensagens.Clear();
            if (Valida(entidade) == false) return false;

            using (var sessao = Helper.AbrirSessao())
            {
                var repo = sessao.GetRepositorio<T>();
                try
                {
                    sessao.IniciaTransacao();
                    if (entidade.Id == 0)
                    {
                        entidade.DataInclusao = DateTime.Now;
                        entidade.UsuarioInclusao = usuario;
                    }
                    else
                    {
                        entidade.DataAlteracao = DateTime.Now;
                        entidade.UsuarioAlteracao = usuario;
                    }
                    repo.Salva(entidade);
                    sessao.ComitaTransacao();
                    return true;
                }
                catch (Exception ex)
                {
                    sessao.RollBackTransacao();
                    Mensagens.Add(ex.InnerException?.Message ?? ex.Message);
                    return false;
                }
            }
        }
        
        public virtual bool Valida(T entidade)
        {
            // Sobrecarregar esse método para as validações
            return true;
        }

        public virtual bool ExecutaComando(string hql)
        {
            Mensagens.Clear();
            using (var sessao = Helper.AbrirSessao())
            {
                var repo = sessao.GetRepositorio<T>();
                try
                {
                    sessao.IniciaTransacao();
                    repo.ExecutaComando(hql);
                    sessao.ComitaTransacao();
                    return true;
                }
                catch (Exception ex)
                {
                    sessao.RollBackTransacao();
                    Mensagens.Add(ex.InnerException?.Message ?? ex.Message);
                    return false;
                }
            }
        }
    }
}
