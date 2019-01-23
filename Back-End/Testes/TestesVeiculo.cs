using System;
using api.Models;
using api.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace api.Testes
{
    [TestClass]
    public class TestesVeiculo
    {
        private readonly DataDbContext context;
        private readonly VeiculoController controller;
        public TestesVeiculo()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            optionsBuilder.UseSqlServer("Server=ADSTDFDES08; Database= Sistema-Viagem; User Id=sa; Password=IL0604#@;");
            context = new DataDbContext(optionsBuilder.Options);
            var repository = new VeiculoRepository(context);
            controller = new VeiculoController(repository);
        }

        [TestCategory("Regras de negócios Veiculo")]
        [TestMethod]
        public void naoDeveSalvar_objeto_null()
        {
            var veiculo = new Veiculo();

            controller.Create(veiculo);

            Assert.IsFalse(veiculo.Id > 0);
        }

        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_fabricante()
        {
            var veiculo = new Veiculo()
            {
                Modelo          = "Toro",
                AnoFabricacao   = 2017,
                AnoModelo       = 2018
            };

            controller.Create(veiculo);

            Assert.IsFalse(veiculo.Id > 0);
        }
        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_modelo()
        {
            var veiculo = new Veiculo()
            {
                Fabricante      = "Fiat",
                AnoFabricacao   = 2017,
                AnoModelo       = 2018
            };

            controller.Create(veiculo);

            Assert.IsFalse(veiculo.Id > 0);
        }
        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_AnoFabricacao()
        {
            var veiculo = new Veiculo()
            {
                Fabricante      = "Fiat",
                Modelo          = "Toro",
                AnoModelo       = 2018
            };

            controller.Create(veiculo);

            Assert.IsFalse(veiculo.Id > 0);
        }
        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios_falta_AnoModelo()
        {
            var veiculo = new Veiculo()
            {
                Fabricante      = "Fiat",
                Modelo          = "Toro",
                AnoFabricacao   = 2017
            };

            controller.Create(veiculo);

            Assert.IsFalse(veiculo.Id > 0);
        }

        [TestMethod]
        public void naoDeveSalvar_anoFabricacao_maior_anoModelo()
        {
            var veiculo = new Veiculo()
            {
                Fabricante      = "Fiat",
                Modelo          = "Toro",
                AnoFabricacao   = 2019,
                AnoModelo       = 2018
            };

            controller.Create(veiculo);

            Assert.IsFalse(veiculo.Id > 0);
        }

        [TestMethod]
        public void naoDeveSalvar_anoFabricacao_menor_2000()
        {
            var veiculo = new Veiculo()
            {
                Fabricante      = "Fiat",
                Modelo          = "Toro",
                AnoFabricacao   = 1999,
                AnoModelo       = 2018
            };

            controller.Create(veiculo);

            Assert.IsFalse(veiculo.Id > 0);
        }

        [TestMethod]
        public void naoDeveSalvar_anoModelo_menor_2000()
        {
            var veiculo = new Veiculo()
            {
                Fabricante      = "Fiat",
                Modelo          = "Toro",
                AnoFabricacao   = 2019,
                AnoModelo       = 1999
            };

            controller.Create(veiculo);

            Assert.IsFalse(veiculo.Id > 0);
        }

        [TestMethod]
        public void deveSalvar_objeto_ok()
        {
            var veiculo = new Veiculo()
            {
                Fabricante      = "Fiat",
                Modelo          = "Toro",
                AnoFabricacao   = 2017,
                AnoModelo       = 2018
            };

            controller.Create(veiculo);

            Assert.IsTrue(veiculo.Id > 0);
        }

        [TestMethod]
        public void naoDeveAtualizar_AnoFabricante_menor_2000()
        {
            var veiculo = new Veiculo()
            {
                Fabricante      = "Fiat",
                Modelo          = "Gol",
                AnoFabricacao   = 2017,
                AnoModelo       = 2018
            };

            var veiculo1 = new Veiculo()
            {
                Fabricante      = "Fiat",
                Modelo          = "Fusca",
                AnoFabricacao   = 1999,
                AnoModelo       = 2016
            };

            controller.Create(veiculo);
            controller.Update(veiculo.Id, veiculo1);

            Assert.IsFalse(veiculo1.AnoModelo == veiculo.AnoModelo);
        }

        [TestMethod]
        public void deveAtualizar_AnoFabricante()
        {
            var veiculo = new Veiculo()
            {
                Fabricante      = "Fiat",
                Modelo          = "Toro",
                AnoFabricacao   = 2030,
                AnoModelo       = 2030
            };

            var veiculo1 = new Veiculo()
            {
                Fabricante      = "Fiat",
                Modelo          = "Toro",
                AnoFabricacao   = 2029,
                AnoModelo       = 2030
            };

            controller.Create(veiculo);
            controller.Update(veiculo.Id, veiculo1);

            Assert.IsTrue(veiculo1.AnoFabricacao == veiculo.AnoFabricacao);
        }

        [TestMethod]
        public void naoDeveAtualizar_AnoModelo_menor_2000()
        {
            var veiculo = new Veiculo()
            {
                Fabricante      = "Teste",
                Modelo          = "Carro",
                AnoFabricacao   = 2019,
                AnoModelo       = 2019
            };

            var veiculo1 = new Veiculo()
            {
                Fabricante      = "Fiat",
                Modelo          = "Toro",
                AnoFabricacao   = 2019,
                AnoModelo       = 1999
            };

            controller.Create(veiculo);
            controller.Update(veiculo.Id, veiculo1);

            Assert.IsFalse(veiculo1.AnoModelo == veiculo.AnoModelo);
        }

        [TestMethod]
        public void deveAtualizar_AnoModelo()
        {
            var veiculo = new Veiculo()
            {
                Fabricante      = "Fiat",
                Modelo          = "Toro",
                AnoFabricacao   = 2018,
                AnoModelo       = 2018
            };

            var veiculo1 = new Veiculo()
            {
                Fabricante      = "Fiat",
                Modelo          = "Toro",
                AnoFabricacao   = 2018,
                AnoModelo       = 2019
            };

            controller.Create(veiculo);
            controller.Update(veiculo.Id, veiculo1);

            Assert.IsTrue(veiculo1.AnoFabricacao == veiculo.AnoFabricacao);
        }

        [TestMethod]
        public void naoDeveAtualizar_fabricante()
        {
            var veiculo = new Veiculo()
            {
                Fabricante      = "Jeep",
                Modelo          = "Renegade",
                AnoFabricacao   = 2017,
                AnoModelo       = 2018
            };

            var veiculo1 = new Veiculo()
            {
                Fabricante      = "Je",
                Modelo          = "Teste",
                AnoFabricacao   = 2017,
                AnoModelo       = 2016
            };

            controller.Create(veiculo);
            controller.Update(veiculo.Id, veiculo1);

            Assert.IsFalse(veiculo1.AnoModelo == veiculo.AnoModelo);
        }

        [TestMethod]
        public void deveAtualizar_fabricante()
        {
            var veiculo = new Veiculo()
            {
                Fabricante      = "Fiat",
                Modelo          = "Toro",
                AnoFabricacao   = 2017,
                AnoModelo       = 2017
            };

            var veiculo1 = new Veiculo()
            {
                Fabricante      = "Jeep",
                Modelo          = "Toro",
                AnoFabricacao   = 2016,
                AnoModelo       = 2017
            };

            controller.Create(veiculo);
            controller.Update(veiculo.Id, veiculo1);

           Assert.IsTrue(veiculo1.AnoFabricacao == veiculo.AnoFabricacao);
        }
    }
}