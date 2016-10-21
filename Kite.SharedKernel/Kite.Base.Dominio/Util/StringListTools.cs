using System;
using System.Collections.Generic;

namespace Kite.Base.Dominio.Util
{
    public static class StringListTools
    {
        public static string ToMessageBoxString(this List<string> lista)
        {
            var msg = string.Empty;
            foreach (var m in lista)
            {
                msg += m + "<br/>";
            }
            return msg.Trim();
        }
    }
}
