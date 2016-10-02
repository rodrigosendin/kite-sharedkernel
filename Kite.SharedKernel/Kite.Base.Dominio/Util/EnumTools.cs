using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Kite.Base.Dominio.ViewModels;

namespace Kite.Base.Dominio.Util
{
    public static class EnumTools
    {
        public static string RetornaDescricao<T>(string enumValue)
        {
            var enumType = typeof(T);
            if (!enumType.IsEnum) throw new InvalidOperationException();

            var descriptionAttribute = enumType
              .GetField(enumValue)
              .GetCustomAttributes(typeof(DescriptionAttribute), false)
              .FirstOrDefault() as DescriptionAttribute;

            return descriptionAttribute != null ? descriptionAttribute.Description : enumValue;
        }

        public static IList<EnumItem> RetornaLista<T>()
        {
            var type = typeof(T);
            var data = Enum.GetNames(type).Select(name => new EnumItem
                {
                    Id          = (int)Enum.Parse(type, name),
                    Name        = name,
                    Description = RetornaDescricao<T>(name)
                }
            ).ToList();
            return data;
        }
    }
}
