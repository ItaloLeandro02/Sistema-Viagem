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
            optionsBuilder.UseSqlServer("Server=DESKTOP-UMB18DT; Database= Sistema-Viagem; User Id=sa; Password=IL0604#@;");
            context = new DataDbContext(optionsBuilder.Options);
            var repository = new ViagemRepository(context);
            controller = new ViagemController(repository);
        }

        [TestCategory("Regras de negócios Viagem")]
        [TestMethod]
        public void naoDeveSalvar()
        {
            var viagem = new Viagem()
            {
                MotoristaId         = 2,
                VeiculoId           = 2,
                DataChegada         = new DateTime(2019, 1, 1),
                DataSaida           = new DateTime(2019,01,02), //yyyy/MM/dd
            };

            controller.Create(viagem);

            Assert.AreNotEqual(0, viagem.Id);
        }
    }
}