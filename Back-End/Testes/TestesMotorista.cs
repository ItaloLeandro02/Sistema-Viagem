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
    public class TestesMotorista
    {
        private readonly DataDbContext context;
        private readonly MotoristaController controller;
        public TestesMotorista()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            optionsBuilder.UseSqlServer("Server=ADSTDFDES08; Database= Sistema-Viagem; User Id=sa; Password=IL0604#@;");
            context = new DataDbContext(optionsBuilder.Options);
            var repository = new MotoristaRepository(context);
            controller = new MotoristaController(repository);
        }    
            
        [TestCategory("Regras de negócios Motorista")]
        [TestMethod]
        public void deveSalvar_nomeValido()
        {
            var motorista = new Motorista()
            {
                Nome = "João"
            };

            controller.Create(motorista);

            Assert.AreNotEqual(0, motorista.Id);
        }

        [TestMethod]
        public void naoDeveSalvar_nome_menor_3_caracteres()
        {
            var motorista = new Motorista()
            {
                Nome = "aa"
            };

            controller.Create(motorista);

            Assert.AreEqual(0, motorista.Id);
        }
        [TestMethod]
        public void naoDeveSalvar_objeto_null()
        {
            var motorista = new Motorista();
        
            controller.Create(motorista);

            Assert.AreEqual(0, motorista.Id);
        }

        [TestMethod]
        public void naoDeveSalvar_campos_obrigatorios()
        {
            var motorista = new Motorista()
            {
                Apelido = "aasssss",
            };

            controller.Create(motorista);

            Assert.AreEqual(0, motorista.Id);
        }
        [TestMethod]
        public void deveSalvar_campos_obrigatorios()
        {
            var motorista = new Motorista()
            {
                Nome = "Teste Unitário",
                Apelido = "Unitário"

            };

            controller.Create(motorista);

            Assert.AreNotEqual(0, motorista.Id);
        }

        [TestMethod]
        public void naoDeveAtualizar_nome_menor_3_caracteres()
        {
            var motorista = new Motorista()
            {
                Nome = "Motorista",
            };

            var motorista1 = new Motorista()
            {
                Nome = "Mo",
            };

            controller.Create(motorista);
            controller.Update(motorista.Id, motorista1);

            Assert.AreNotEqual(motorista.Nome, motorista1.Nome);
        }
        [TestMethod]
        public void deveAtualiazar_nome()
        {
            var motorista = new Motorista()
            {
                Nome = "Teste Unitário"
            };

            var motorista1 = new Motorista()
            {
                Nome = "João Da Silva"
            };

            controller.Create(motorista);
            controller.Update(motorista.Id, motorista1);
            Assert.AreEqual(motorista.Nome, motorista1.Nome);
        }
    }
}
