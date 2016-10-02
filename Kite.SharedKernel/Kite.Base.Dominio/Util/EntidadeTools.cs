using System.Collections;
using System.Collections.Generic;
using Kite.Base.Dominio.Entidades;

namespace Kite.Base.Dominio.Util
{
    public static class EntidadeTools
    {
        /// <summary>
        /// Método de extensão usado para vincular registros de uma bag ao seu registro pai, realizando reflection no type para obter o tipo generico da propriedade, que se for lista, altera os objetos dinamicamente, referenciando o objeto pai.
        /// </summary>
        /// <param name="source"> Um registro que seja herdeiro de EntidadeBase </param>
        /// <returns>O próprio registro.</returns>
        public static EntidadeBase VincularColecoes(this EntidadeBase source)
        {
            foreach (var atributo in source.GetType().GetProperties())
            {
                var tipoAtributo = atributo.PropertyType;
                if (!tipoAtributo.IsGenericType) continue;
                var genericType = tipoAtributo.GetGenericTypeDefinition();
                if (genericType != typeof(IList<>)) continue;

                var colecao = (IList)source.GetType().GetProperty(atributo.Name).GetValue(source); 
                if (colecao == null) continue;

                foreach (var item in colecao)
                {
                    var type = item.GetType();
                    var sourceType = source.GetType().Name;
                    var property = type.GetProperty(sourceType);
                    property?.SetValue(item, source);
                }
            }
            return source;
        }
    }
}