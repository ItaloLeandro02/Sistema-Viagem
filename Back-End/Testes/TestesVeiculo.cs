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
        private readonly MotoristaController controller;
        public TestesVeiculo()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            optionsBuilder.UseSqlServer("Server=ADSTDFDES08; Database= Sistema-Viagem; User Id=sa; Password=IL0604#@;");
            context = new DataDbContext(optionsBuilder.Options);
            var repository = new MotoristaRepository(context);
            controller = new MotoristaController(repository);
        }
        
    }
}