using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Viagem
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("veiculo")]
        public int VeiculoId { get; set; }
        public virtual Veiculo veiculo { get; set; }

        [ForeignKey("motorista")]
        public int MotoristaId { get; set; }
        public virtual Motorista motorista { get; set; }
        public DateTimeOffset DataSaida { get; set; }
        public DateTimeOffset DataChegada { get; set; }

        [ForeignKey("cidadeOrigem")]
        public int OrigemCidadeId { get; set; }
        public virtual CidadeIbge cidadeOrigem { get; set; }

        [ForeignKey("cidadeDestino")]
        public int DestinoCidadeId { get; set; }
        public virtual CidadeIbge cidadeDestino { get; set; }
        //[ForeignKey("despesas")]
        public virtual List<ViagemDespesa> despesas { get; set; }
        
        public double ToneladaPrecoUnitario { get; set; }
        public double ToneladaCarga { get; set; }
        public double ValorTotalBruto { get; set; }
        public double ValorTotalDespesa { get; set; }
        public double ValorTotalLiquido { get; set; }
        //[ForeignKey("combustivel")]
        public virtual List<ViagemDespesa> combustivel { get; set; }
        public double Valor_Total_Combustivel { get; set; }
        public double Valor_Imposto { get; set; }
    }
}