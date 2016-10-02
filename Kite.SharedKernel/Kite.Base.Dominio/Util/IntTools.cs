namespace Kite.Base.Dominio.Util
{
    public static class IntTools
    {
        public static int? ToNullableInt(this string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }
    }
}
