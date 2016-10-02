using NHibernate.Dialect;

namespace Kite.Base.Repositorio
{
    public class MsSqlAzureDialect : MsSql2008Dialect
    {
        public override string PrimaryKeyString => "primary key CLUSTERED";
    }
}
