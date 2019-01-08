using System;

namespace api.Views
{
    public class DashboardFaturamento
    {
        public Int64 Id { get; set; }
        public int Mes { get; set; }
        public string Modelo { get; set; }
        public double Total { get; set; }
    }
}