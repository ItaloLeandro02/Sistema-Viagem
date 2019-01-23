using System;
using api.Models;
using api.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testes
{
    [TestClass]
    public class TesteViagem
    {
        private readonly DataDbContext context;
        private readonly ViagemController controller;
        public TesteViagem()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            optionsBuilder.UseSqlServer("Server=ADSTDFDES08; Database= Sistema-Viagem; User Id=sa; Password=IL0604#@;");
            context = new DataDbContext(optionsBuilder.Options);
            var repository = new ViagemRepository(context);
            controller = new ViagemController(repository);
        }

        [TestCategory("Regras de negócios Viagem")]
        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_motoristaId()
        {
            var viagem = new Viagem()
            {   
                VeiculoId                   = 1225,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,1), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
            };

            controller.Create(viagem);

            Assert.IsFalse(viagem.Id > 0);
        }

        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_veiculoId()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1153,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,1), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
            };

            controller.Create(viagem);

            Assert.IsFalse(viagem.Id > 0);
        }

        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_dataChegada()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1153,
                VeiculoId                   = 1225,
                DataSaida                   = new DateTime(2019,1,2), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
            };

            controller.Create(viagem);

            Assert.IsFalse(viagem.Id > 0);
        }

        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_dataSaida()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1153,
                VeiculoId                   = 1225,
                DataChegada                 = new DateTime(2019, 1, 1),
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
            };

            controller.Create(viagem);

            Assert.IsFalse(viagem.Id > 0);
        }

        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_origemCidadeId()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1153,
                VeiculoId                   = 1225,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,1), //yyyy/MM/dd
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
            };

            controller.Create(viagem);

            Assert.IsFalse(viagem.Id > 0);
        }

        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_destinoCidadeId()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1153,
                VeiculoId                   = 1225,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,1), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
            };

            controller.Create(viagem);

           Assert.IsFalse(viagem.Id > 0);
        }

        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_toneladaPrecoUnitaio()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1153,
                VeiculoId                   = 1225,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,1), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaCarga               = 150,
            };

            controller.Create(viagem);

            Assert.IsFalse(viagem.Id > 0);
        }

        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_toneladaCarga()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1153,
                VeiculoId                   = 1225,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,1), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
            };

            controller.Create(viagem);

            Assert.IsFalse(viagem.Id > 0);
        }

        [TestMethod]
        public void naoDeveSalvar_despesa_valor_maior_0()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1153,
                VeiculoId                   = 1225,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,1), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
                despesas                    = new List<ViagemDespesa>()
            };

            var despesa = new ViagemDespesa()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Lazer",
                Valor           = 0
            };

            viagem.despesas.Add(despesa);

            controller.Create(viagem);

            Assert.IsFalse(viagem.Id > 0);
        }

        [TestMethod]
        public void naoDeveSalvar_despesa_historico_maior_5_caracteres()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1153,
                VeiculoId                   = 1225,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,1), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
                despesas                    = new List<ViagemDespesa>()
            };

            var despesa = new ViagemDespesa()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Laze",
                Valor           = 150
            };

            viagem.despesas.Add(despesa);

            controller.Create(viagem);

            Assert.IsFalse(viagem.Id > 0);
        }

        [TestMethod]
        public void naoDeveSalvar_despesa_dataLancamento_diferente_dataChegada()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1153,
                VeiculoId                   = 1225,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,2), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
                despesas                    = new List<ViagemDespesa>()
            };

            var despesa = new ViagemDespesa()
            {
                DataLancamento  =  new DateTime(2019, 1, 3),
                Historico       = "Lazer",
                Valor           = 150
            };

            viagem.despesas.Add(despesa);

            controller.Create(viagem);

            Assert.IsFalse(viagem.Id > 0);
        }

        [TestMethod]
        public void naoDeveSalvar_dataLancamento_combustivel_diferente_dataChegada()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1153,
                VeiculoId                   = 1225,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,1), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
                despesas                    = new List<ViagemDespesa>(),
                combustivel                 = new List<CombustivelDTO>()

            };

            var despesa = new ViagemDespesa()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Lazer",
                Valor           = 150,
                Tipo            = 1
            };
            var combustivel = new CombustivelDTO()
            {
                DataLancamento  =  new DateTime(2019, 1, 12),
                Historico       = "Ipiranga",
                Valor           = 300,
                Tipo            = 2
            };

            viagem.despesas.Add(despesa);
            viagem.combustivel.Add(combustivel);

            controller.Create(viagem);

            Assert.IsFalse(viagem.Id > 0);
        }
        [TestMethod]
        public void naoDeveSalvar_combustivel_historico_menor_5_caracateres()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1153,
                VeiculoId                   = 1225,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,1), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
                despesas                    = new List<ViagemDespesa>(),
                combustivel                 = new List<CombustivelDTO>()

            };

            var despesa = new ViagemDespesa()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Lazer",
                Valor           = 150,
                Tipo            = 1
            };
            var combustivel = new CombustivelDTO()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Ipir",
                Valor           = 300,
                Tipo            = 2
            };

            viagem.despesas.Add(despesa);
            viagem.combustivel.Add(combustivel);

            controller.Create(viagem);

            Assert.IsFalse(viagem.Id > 0);
        }

        [TestMethod]
        public void naoDeveSalvar_combustivel_valor_0()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1153,
                VeiculoId                   = 1225,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,1), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
                despesas                    = new List<ViagemDespesa>(),
                combustivel                 = new List<CombustivelDTO>()

            };

            var despesa = new ViagemDespesa()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Lazer",
                Valor           = 150,
                Tipo            = 1
            };
            var combustivel = new CombustivelDTO()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Ipiranga",
                Valor           = 0,
                Tipo            = 2
            };

            viagem.despesas.Add(despesa);
            viagem.combustivel.Add(combustivel);

            controller.Create(viagem);

            Assert.IsFalse(viagem.Id > 0);
        }

        [TestMethod]
        public void deveSalvar_verificar_valor_combustivel()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1153,
                VeiculoId                   = 1225,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,1), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
                despesas                    = new List<ViagemDespesa>(),
                combustivel                 = new List<CombustivelDTO>()

            };

            var despesa = new ViagemDespesa()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Lazer",
                Valor           = 150,
                Tipo            = 1
            };
            var ipiranga = new CombustivelDTO()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Ipiranga",
                Valor           = 300,
                Tipo            = 2
            };
            var powerShell = new CombustivelDTO()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "PowerShell",
                Valor           = 500,
                Tipo            = 2
            };

            viagem.despesas.Add(despesa);
            viagem.combustivel.Add(ipiranga);
            viagem.combustivel.Add(powerShell);

            controller.Create(viagem);
            
            double total = 0;

            viagem.combustivel.ForEach(item => {
                total += item.Valor;
            });

            Assert.IsTrue(viagem.Valor_Total_Combustivel == total);
        }

        [TestMethod]
        public void deveSalvar_objeto_ok()
        {

            var despesa = new ViagemDespesa()
            {
                DataLancamento  =  new DateTime(2019, 1, 2),
                Historico       = "Lazer",
                Valor           = 150
            };

            var viagem = new Viagem()
            {   
                MotoristaId                 = 1153,
                VeiculoId                   = 1225,
                DataChegada                 = new DateTime(2019, 1, 2),
                DataSaida                   = new DateTime(2019,1,1), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
                despesas                    = new List<ViagemDespesa>()
            };

            viagem.despesas.Add(despesa);
          
            controller.Create(viagem);

            Assert.IsTrue(viagem.Id > 0);
        }

        [TestMethod]
        public void deveSalvar_verificar_preco_bruto()
        {

            var despesa = new ViagemDespesa()
            {
                DataLancamento  =  new DateTime(2019, 1, 2),
                Historico       = "Lazer",
                Valor           = 150
            };

            var viagem = new Viagem()
            {   
                MotoristaId                 = 1153,
                VeiculoId                   = 1225,
                DataChegada                 = new DateTime(2019, 1, 2),
                DataSaida                   = new DateTime(2019,1,1), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
                despesas                    = new List<ViagemDespesa>()
            };

            viagem.despesas.Add(despesa);
          
            controller.Create(viagem);

            Assert.IsTrue(viagem.ValorTotalBruto == (viagem.ToneladaPrecoUnitario * viagem.ToneladaCarga));
        }

        [TestMethod]
        public void deveSalvar_verificar_preco_liquido()
        {

            var lazer = new ViagemDespesa()
            {
                DataLancamento  =  new DateTime(2019, 1, 2),
                Historico       = "Lazer",
                Valor           = 150
            };

            var combustivel = new ViagemDespesa()
            {
                DataLancamento  =  new DateTime(2019, 1, 2),
                Historico       = "Combustível",
                Valor           = 400
            };

            var viagem = new Viagem()
            {   
                MotoristaId                 = 1153,
                VeiculoId                   = 1225,
                DataChegada                 = new DateTime(2019, 1, 2),
                DataSaida                   = new DateTime(2019,1,1), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
                despesas                    = new List<ViagemDespesa>()
            };

            viagem.despesas.Add(lazer);
            viagem.despesas.Add(combustivel);

            double despesas = 0;
            foreach (var item in viagem.despesas)
            {
                despesas += item.Valor;
            }
          
            controller.Create(viagem);

            Assert.IsTrue(viagem.ValorTotalLiquido == (viagem.ValorTotalBruto - despesas));
        }
    }
}