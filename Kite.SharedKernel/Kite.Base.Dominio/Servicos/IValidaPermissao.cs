namespace Kite.Base.Dominio.Servicos
{
    public interface IValidaPermissao
    {
        bool ValidaPermissao(long perfilId, string controller, string verbo);
    }
}