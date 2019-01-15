using System;
using api.Models;
using Testes;
using api.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace api.Testes
{
    public class Class1
    {
           
        [Fact]
        public void PassingTest()
        {
             var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            optionsBuilder.UseSqlServer("Server=ADSTDFDES08; Database= Sistema-Viagem; User Id=sa; Password=IL0604#@;");

            var context = new DataDbContext(optionsBuilder.Options);
            
            var motorista = new Motorista()
            {
                Nome = "aa"
            };

            var Repository = new MotoristaRepository(context);

                Repository.Add(motorista);
        }
    }
}
