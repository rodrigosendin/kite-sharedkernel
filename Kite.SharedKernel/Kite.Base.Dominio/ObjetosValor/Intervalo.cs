using System;

namespace Kite.Base.Dominio.ObjetosValor
{
    public class Intervalo : ObjetoValor<Intervalo>
    {
        public DateTime Inicio  { get; set; }
        public DateTime Fim     { get; set; }

        public Intervalo(DateTime inicio, DateTime fim)
        {
            Inicio  = inicio;
            Fim     = fim;
        }

        public Intervalo()
        {            
        }

        public int RetornaDuracaoEmDias()
        {
            return Fim.Subtract(Inicio).Days;
        }

        public int RetornaDuracaoEmHoras()
        {
            return (int) Fim.Subtract(Inicio).TotalHours;
        }

        public bool IntervaloVigente(DateTime data)
        {
            return data >= Inicio && data <= Fim;
        }

        public bool IntervaloVigente()
        {
            return IntervaloVigente(DateTime.Today);
        }
    }
}
