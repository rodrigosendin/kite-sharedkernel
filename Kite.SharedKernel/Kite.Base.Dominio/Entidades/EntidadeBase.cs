using System;

namespace Kite.Base.Dominio.Entidades
{
    public class EntidadeBase : Entidade<long>
    {
        public DateTime?    DataInclusao        { get; set; }
        public string       UsuarioInclusao     { get; set; }
        public DateTime?    DataAlteracao       { get; set; }
        public string       UsuarioAlteracao    { get; set; }

        public EntidadeBase Clone()
        {
            return (EntidadeBase)MemberwiseClone();
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}