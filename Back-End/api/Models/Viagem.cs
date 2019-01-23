using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Validation;

namespace api.Models
{
    public class Viagem
    {
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
        public virtual List<ViagemDespesa> despesas { get; set; }
        public double ToneladaPrecoUnitario { get; set; }
        public double ToneladaCarga { get; set; }
        public double ValorTotalBruto { get; set; }
        public double ValorTotalDespesa { get; set; }
        public double ValorTotalLiquido { get; set; }
        public virtual List<CombustivelDTO> combustivel { get; set; }
        public double Valor_Total_Combustivel { get; set; }
        public double Valor_Imposto { get; set; }

        public void Validacoes()
        {
            AssertionConcern.AssertArgumentNotEquals(this.VeiculoId, 0, "Veiculo é obrigatório!");
            AssertionConcern.AssertArgumentNotEquals(this.MotoristaId, 0, "Motorista é obrigatório!");
            AssertionConcern.AssertArgumentNotEquals(this.DataSaida.Date, new DateTime(0001,01,01).Date, "A Data de saida é obrigatória!");
            AssertionConcern.AssertArgumentNotEquals(this.DataChegada.Date, new DateTime(0001,01,01).Date, "A Data de chegada é obrigatória!");
            AssertionConcern.AssertArgumentNotEquals(this.OrigemCidadeId, 0, "Cidade de origem é obrigatória!");
            AssertionConcern.AssertArgumentNotEquals(this.DestinoCidadeId, 0, "Cidade de destino é obrigatória!");
            AssertionConcern.AssertArgumentNotEquals(this.OrigemCidadeId, 0, "Cidade de origem é obrigatória!");
            AssertionConcern.AssertArgumentTrue(this.ToneladaPrecoUnitario > 1, "O preço unitário deve ser maior do que R$1,00!");
            AssertionConcern.AssertArgumentTrue(this.ToneladaCarga > 1, "A quantidade transportada deve ser maior do que 1!");
            AssertionConcern.AssertArgumentFalse(this.OrigemCidadeId == this.DestinoCidadeId, "As cidades devem ser diferentes!");
            AssertionConcern.AssertArgumentTrue(this.ValorTotalBruto == (this.ToneladaCarga * this.ToneladaPrecoUnitario), "Erro ao calcular o total bruto!");
            AssertionConcern.AssertArgumentTrue(this.ValorTotalLiquido == (this.ValorTotalBruto - this.ValorTotalDespesa), "Erro ao calcular o total liquido!");
            AssertionConcern.AssertArgumentFalse(this.DataSaida.Date > this.DataChegada.Date, "A data de chegada não pode ser menor que a data de saida!");

            if (this.despesas != null)    
                {
                    foreach (var item in this.despesas)
                    {
                        AssertionConcern.AssertArgumentEquals(this.DataChegada.Date, item.DataLancamento.Date, "A Data de Lançamento deve ser a mesma da chegada!");
                    }
                }

                if (this.combustivel != null) 
                {
                    foreach (var item in this.combustivel)
                    {
                        AssertionConcern.AssertArgumentEquals(this.DataChegada.Date, item.DataLancamento.Date, "A Data de Lançamento deve ser a mesma da chegada!");
                    }
                }
        }
    }
}