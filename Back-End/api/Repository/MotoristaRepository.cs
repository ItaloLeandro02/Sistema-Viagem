using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class MotoristaRepository : IMotoristaRepository
    {
        private readonly DataDbContext _context;
        public MotoristaRepository(DataDbContext ctx) 
        {
            _context = ctx;
        }
        public void Add(Motorista motorista)
        {
            var transaction = _context.Database.BeginTransaction();
                try {

                    //Verifica se o nome tem no mínimo 3 caracteres
                    if (motorista.Nome.Length < 3) 
                    {
                        transaction.Rollback();
                            return;
                    }

                    //Caso o apelido não seja informado receberá o primeiro nome
                    //Apenas um teste, procurar um forma melhor de realizar este procedimento
                    if (string.IsNullOrEmpty(motorista.Apelido)) 
                    {
                        string[] nome = motorista.Nome.Split(" ");
                            for (int i = 0; i < nome.Length; i++)
                            {
                                motorista.Apelido = nome[0];    
                            }
                    }

                        motorista.Desativado = 0;

                            _context.Motorista.Add(motorista);
                                _context.SaveChanges();
                                    transaction.Commit();
                }
                catch (Exception e) {
                    Console.WriteLine("Erro");
                         Console.WriteLine(e);
                            transaction.Rollback();
                                return;
                }
        }

        public Motorista Find(int id)
        {
            return _context.Motorista.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Motorista> GetAll()
        {
            return _context.Motorista.AsNoTracking().ToList();
        }

        public void Remove(int id)
        {
            var transaction = _context.Database.BeginTransaction();
                try {
                    var motorista = _context.Motorista.First(u => u.Id == id);
                        _context.Motorista.Remove(motorista);
                            _context.SaveChanges();
                                transaction.Commit();
                }
                catch (Exception e) {
                    Console.WriteLine("Erro");
                        Console.WriteLine(e);
                            transaction.Rollback();
                                return;
                }
        }

        public void Update(Motorista form, Motorista banco)
        {
            var transaction = _context.Database.BeginTransaction();
                try {

                    //Caso o apelido não seja informado receberá o primeiro nome
                    //Apenas um teste, procurar um forma melhor de realizar este procedimento
                    if (string.IsNullOrEmpty(form.Apelido)) 
                    {
                        string[] nome = form.Nome.Split(" ");
                            for (int i = 0; i < nome.Length; i++)
                            {
                                form.Apelido = nome[0];    
                            }
                    }

                        banco.Nome       = form.Nome;
                        banco.Apelido    = form.Apelido;
                        banco.Desativado = form.Desativado;
                        
                            _context.Motorista.Update(banco);
                                _context.SaveChanges();
                                    transaction.Commit();
                } 
                catch (Exception e) {
                    Console.WriteLine("Erro");
                        Console.WriteLine(e);
                            transaction.Rollback();
                                throw new System.Net.WebException (string.Format("Falha ao atualizar dados do motorista"));
                }
        }
    }
}