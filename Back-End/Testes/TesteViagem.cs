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
                VeiculoId                   = 1015,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,2), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
            };

            controller.Create(viagem);

            Assert.AreEqual(0, viagem.Id);
        }

        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_veiculoId()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1033,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,2), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
            };

            controller.Create(viagem);

            Assert.AreEqual(0, viagem.Id);
        }

        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_dataChegada()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1033,
                VeiculoId                   = 1015,
                DataSaida                   = new DateTime(2019,1,2), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
            };

            controller.Create(viagem);

            Assert.AreEqual(0, viagem.Id);
        }

        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_dataSaida()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1033,
                VeiculoId                   = 1015,
                DataChegada                 = new DateTime(2019, 1, 1),
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
            };

            controller.Create(viagem);

            Assert.AreEqual(0, viagem.Id);
        }

        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_origemCidadeId()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1033,
                VeiculoId                   = 1015,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,2), //yyyy/MM/dd
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
            };

            controller.Create(viagem);

            Assert.AreEqual(0, viagem.Id);
        }

        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_destinoCidadeId()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1033,
                VeiculoId                   = 1015,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,2), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                ToneladaPrecoUnitario       = 200,
                ToneladaCarga               = 150,
            };

            controller.Create(viagem);

            Assert.AreEqual(0, viagem.Id);
        }

        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_toneladaPrecoUnitaio()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1033,
                VeiculoId                   = 1015,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,2), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaCarga               = 150,
            };

            controller.Create(viagem);

            Assert.AreEqual(0, viagem.Id);
        }

        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_toneladaCarga()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1033,
                VeiculoId                   = 1015,
                DataChegada                 = new DateTime(2019, 1, 1),
                DataSaida                   = new DateTime(2019,1,2), //yyyy/MM/dd
                OrigemCidadeId              = 263,
                DestinoCidadeId             = 1500,
                ToneladaPrecoUnitario       = 200,
            };

            controller.Create(viagem);

            Assert.AreEqual(0, viagem.Id);
        }

        [TestMethod]
        public void naoDeveSalvar_despesa_valor_maior_0()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1033,
                VeiculoId                   = 1015,
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
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Lazer",
                Valor           = 0
            };

            viagem.despesas.Add(despesa);

            controller.Create(viagem);

            Assert.AreEqual(0, viagem.Id);
        }

        [TestMethod]
        public void naoDeveSalvar_despesa_historico_maior_5_caracteres()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1033,
                VeiculoId                   = 1015,
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
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Laze",
                Valor           = 150
            };

            viagem.despesas.Add(despesa);

            controller.Create(viagem);

            Assert.AreEqual(0, viagem.Id);
        }

        [TestMethod]
        public void naoDeveSalvar_despesa_dataLancamento_igual_dataChegada()
        {
            var viagem = new Viagem()
            {   
                MotoristaId                 = 1033,
                VeiculoId                   = 1015,
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

            Assert.AreEqual(0, viagem.Id);
        }

        [TestMethod]
        public void deveSalvar_objeto_ok()
        {

            var despesa = new ViagemDespesa()
            {
                DataLancamento  =  new DateTime(2019, 1, 1),
                Historico       = "Lazer",
                Valor           = 150
            };

            var viagem = new Viagem()
            {   
                MotoristaId                 = 1033,
                VeiculoId                   = 1015,
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

            Assert.AreNotEqual(0, viagem.Id);
        }
    }
}