﻿using System;
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
           
        [Fact]
        public void deveSalvar_nomeValido()
        {
            var motorista = new Motorista()
            {
                Nome = "João"
            };

            controller.Create(motorista);

            Assert.NotEqual(0, motorista.Id);
        }

        [Fact]
        public void naoDeveSalvar_nome_menor_3_caracteres()
        {
            var motorista = new Motorista()
            {
                Nome = "aa"
            };

            controller.Create(motorista);

            Assert.Equal(0, motorista.Id);
        }
        [Fact]
        public void naoDeveSalvar_objeto_null()
        {
            var motorista = new Motorista();
        
            controller.Create(motorista);

            Assert.Equal(0, motorista.Id);
        }

        [Fact]
        public void naoDeveSalvar_campos_obrigatorios()
        {
            var motorista = new Motorista()
            {
                Apelido = "aasssss",
            };

            controller.Create(motorista);

            Assert.Equal(0, motorista.Id);
        }
        [Fact]
        public void deveSalvar_campos_obrigatorios()
        {
            var motorista = new Motorista()
            {
                Nome = "Teste Unitário",
                Apelido = "Unitário"

            };

            controller.Create(motorista);

            Assert.NotEqual(0, motorista.Id);
        }
    }
}