using System;
using api.Models;
using api.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using Xunit;

namespace api.Testes
{
    public class Class1
    {
        public Class1()
        {
            
        }
        [Fact]
        public void PassingTest()
        {
            var motorista = new Motorista()
            {
                Nome = ""
            };

            var motoristaRepository = new Mock<IMotoristaRepository>();

            motoristaRepository.Setup(x => x.Add(motorista)); //no return as it's a void method
            motoristaRepository.Object.Add(motorista);
            motoristaRepository.Verify(x => x.Add(motorista), Times.Once()); //Assert that the Add method was called once
        }

        [Fact]
        public void Get_Person_By_Id()
        {
        var motorista = new Motorista { Id = 1};
        var motoristaRepository = new Mock<IMotoristaRepository>();

        motoristaRepository.Setup(x => x.Find(1))
        .Returns(motorista); //return Person
        motoristaRepository.Object.Find(1).ShouldBe(motorista); //Assert expected value equal to actual value
        motoristaRepository.Verify(x => x.Find(motorista.Id), Times.Once()); //Assert that the Get method was called once

        }
    }
}
