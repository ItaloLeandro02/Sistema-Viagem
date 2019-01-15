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
            //Verifica se o ano de fabricação é maior do que 2000
            //Verifica se o ano do modelo é maior do que o de fabicação
            if ((veiculo.AnoFabricacao < 2000) || (veiculo.AnoModelo < veiculo.AnoFabricacao) || (veiculo.Modelo != null)  || (veiculo.Fabricante != null))
            {                
                return;
            }
            
            var transaction = _context.Database.BeginTransaction();
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
            var transaction = _context.Database.BeginTransaction();
            
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

        public void Update(Veiculo form, Veiculo banco)
        {
            var transaction = _context.Database.BeginTransaction();
                try 
                {
                    
                    banco.AnoFabricacao = form.AnoFabricacao;
                    banco.AnoModelo     = form.AnoModelo;
                    banco.Desativado    = form.Desativado;
                    banco.Fabricante    = form.Fabricante;
                    banco.Modelo        = form.Modelo;

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