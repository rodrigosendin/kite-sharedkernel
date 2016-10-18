namespace Kite.Base.Dominio.Entidades
{
    public class Usuario : EntidadeBase, IAggregateRoot
    {               
        public string Nome  { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}

