using System;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace api.Testes
{
    public class Class1
    {
        private readonly DataDbContext _context;
        public Class1(DataDbContext ctx) 
        {
            _context = ctx;
        }

        [Fact]
        public void Teste1()
        {
            var motorista = new Motorista() 
            {
                Nome = "a"
            };

                _context.Motorista.Add(motorista);
                    _context.SaveChanges();

                    Assert.IsType<Motorista>(motorista);    
        }
    }
}
