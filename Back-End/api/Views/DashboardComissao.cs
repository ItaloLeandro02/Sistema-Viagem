using System;

namespace api.Views
{
    public class DashboardComissao
    {
            public Int64 Id { get; set; }
            public int? Mes { get; set; }
            public string Nome { get; set; }
            public double Total { get; set; }
            public double Comissao { get; set; }   
    }
}