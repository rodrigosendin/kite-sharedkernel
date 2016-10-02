using System;

namespace Kite.Base.Dominio.ViewModels
{
    public class Token
    {
        public long     UsuarioId       { get; set; }
        public string   UsuarioNome     { get; set; }
        public string   Login           { get; set; }
        public DateTime Data            { get; set; }
    }
}