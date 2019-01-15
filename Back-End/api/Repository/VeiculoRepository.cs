using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly DataDbContext _context;
        public VeiculoRepository(DataDbContext ctx)
        {
            _context = ctx;
        }
        public void Add(Veiculo veiculo)
        {            
            using (var transaction = _context.Database.BeginTransaction())
            {
                try 
                {
                    _context.Veiculo.Add(veiculo);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e) 
                {
                    Console.WriteLine("Erro");
                    Console.WriteLine(e);
                    transaction.Rollback();
                    return;
                }
            }
        }

        public Veiculo Find(int id)
        {
            return _context.Veiculo.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Veiculo> GetAll()
        {
            return _context.Veiculo.AsNoTracking().ToList();
        }

        public void Remove(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try 
                {
                    var entity = _context.Veiculo.First(u => u.Id == id);
                    _context.Veiculo.Remove(entity);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e) 
                {
                    Console.WriteLine("Erro");
                    Console.WriteLine(e);
                    transaction.Rollback();
                    return;
                }
            }
        }

        public void Update(Veiculo form, Veiculo banco)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try 
                {                   
                    _context.Veiculo.Update(banco);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e) 
                {
                    Console.WriteLine("Erro");
                    Console.WriteLine(e);
                    transaction.Rollback();
                    throw new System.Net.WebException (string.Format("Falha ao atualizar dados do veiculo"));
                }
            }
        }
    }
}