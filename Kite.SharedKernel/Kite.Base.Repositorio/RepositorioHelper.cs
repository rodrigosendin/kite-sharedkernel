using Kite.Base.Dominio.Repositorio;

namespace Kite.Base.Repositorio
{
    public class RepositorioHelper : IRepositorioHelper
    {
        public IRepositorioSessao AbrirSessao()
        {
            return new RepositorioSessao(
                NHibernateHelper.OpenSession());
        }
    }
}