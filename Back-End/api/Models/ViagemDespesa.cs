using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class ViagemDespesa
    {
        public int Id { get; set; }
        
        public int ViagemId { get; set; }
        [NotMapped]
        //public Viagem viagem { get; set; }

        public DateTimeOffset DataLancamento { get; set; }
        public string Historico { get; set; }
        public double Valor { get; set; }
        public int Tipo { get; set; }
    }
}