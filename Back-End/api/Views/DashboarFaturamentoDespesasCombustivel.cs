using System;

namespace api.Views
{
    public class DashboarFaturamentoDespesasCombustivel
    {
        public Int64 Id { get; set; }
        public double Bruto { get; set; }
        public double Despesas { get; set; }
        public double Combustivel { get; set; }
        public int Mes { get; set; }
    }
}