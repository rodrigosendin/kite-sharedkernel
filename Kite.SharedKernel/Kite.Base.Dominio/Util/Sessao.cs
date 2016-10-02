namespace Kite.Base.Dominio.Util
{
    public static class Sessao
    {
        public static string    AppToken    { get; set; }
        public static string    Token       { get; set; }
        public static string    UrlBaseApi  { get; set; }
        public static bool      EstaLogado => !string.IsNullOrWhiteSpace(Token);
    }
}
