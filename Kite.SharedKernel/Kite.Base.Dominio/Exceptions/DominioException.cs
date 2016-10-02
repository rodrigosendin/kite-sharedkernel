using System;

namespace Kite.Base.Dominio.Exceptions
{
    public class DominioException : Exception
    {
        public DominioException(string message) : base(message)
        {
        }
    }
}
