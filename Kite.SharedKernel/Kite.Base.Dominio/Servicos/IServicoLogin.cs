using Kite.Base.Dominio.ViewModels;

namespace Kite.Base.Dominio.Servicos
{
    public interface IServicoLogin
    {
        Token Login(string login, string senha, string appToken = null);
    }
}