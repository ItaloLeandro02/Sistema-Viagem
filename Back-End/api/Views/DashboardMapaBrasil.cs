using System;

namespace api.Views
{
    public class DashboardMapaBrasil
    {
        public Int64 Id { get; set; }
        public double Faturamento { get; set; }
        public double Despesa { get; set; }
        public double Liquido { get; set; }
        public string Uf { get; set; }
    }
}