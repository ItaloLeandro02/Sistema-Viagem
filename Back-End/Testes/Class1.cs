using System;
using api.Models;
using api.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using api.Controllers;

namespace api.Testes
{
    public class Class1
    {
        private readonly DataDbContext context;
        private readonly MotoristaController controller;
        public Class1()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-UMB18DT; Database= Sistema-Viagem; User Id=sa; Password=IL0604#@;");
            context = new DataDbContext(optionsBuilder.Options);
            var repository = new MotoristaRepository(context);
            controller = new MotoristaController(repository);
        }        
           
        [Fact]
        public void nomeValido()
        {
            var motorista = new Motorista()
            {
                Nome = "aaa"
            };

            var Repository = new MotoristaRepository(context);

            controller.Create(motorista);

            Assert.NotEqual(0, motorista.Id);
        }

        [Fact]
        public void nomeInvalido()
        {
            var motorista = new Motorista()
            {
                Nome = "aa"
            };

            controller.Create(motorista);

            Assert.NotEqual(0, motorista.Id);
        }
    }
}
