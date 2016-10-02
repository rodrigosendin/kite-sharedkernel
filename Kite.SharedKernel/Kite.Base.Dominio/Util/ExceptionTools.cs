using System;

namespace Kite.Base.Dominio.Util
{
    public static class ExceptionTools
    {
        public static string BuildExceptionMessage(this Exception exception)
        {
            if (exception == null) return null;

            var msg = "";
            while (true)
            {
                msg = msg + exception.Message + Environment.NewLine;
                if (exception.InnerException == null) return msg;
                exception = exception.InnerException;
            }
        }
    }
}
