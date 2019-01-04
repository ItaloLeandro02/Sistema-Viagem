using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ViagemRepository : IViagemRepository
    {
        private readonly DataDbContext _context;
        public ViagemRepository(DataDbContext ctx) 
        {
            _context = ctx;
        }
        public void Add(Viagem viagem)
        {
            var transaction = _context.Database.BeginTransaction();
                try {

                    if ((viagem.DataChegada < viagem.DataSaida) || (viagem.OrigemCidadeId == viagem.DestinoCidadeId) || (viagem.ToneladaPrecoUnitario < 1) || (viagem.ToneladaCarga < 1)) 
                    {
                        transaction.Rollback();
                            return;
                    }

                        viagem.ValorTotalBruto   = (viagem.ToneladaCarga * viagem.ToneladaPrecoUnitario); 
                        viagem.ValorTotalLiquido = (viagem.ValorTotalBruto - viagem.ValorTotalDespesa);

                            _context.Viagem.Add(viagem);
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

        public Viagem Find(int id)
        {
            //Preciso trabalhar aqui para não retornar dados sensíveis
            return _context.Viagem
            .Include(v => v.veiculo)
            .Include(m => m.motorista)
            .Include(o => o.cidadeOrigem)
            .Include(d => d.cidadeDestino)
            .FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Viagem> GetAll()
        {
            //Preciso trabalhar aqui para não retornar dados sensíveis
            return _context.Viagem
            .Include(v => v.veiculo)
            .Include(m => m.motorista)
            .Include(o => o.cidadeOrigem)
            .Include(d => d.cidadeDestino)
            .ToList();
        }

        public void Remove(int id)
        {
            var transaction = _context.Database.BeginTransaction();

                try {

                    var viagem = _context.Viagem
                    .Where(v => v.Id == id)
                    .First();

                    var veiculo = _context.Veiculo
                    .Where(vei => vei.Id == viagem.VeiculoId)
                    .First();

                    var motorista = _context.Motorista
                    .Where(m => m.Id == viagem.MotoristaId)
                    .First();

                        _context.Viagem.Remove(viagem);
                        _context.Veiculo.Remove(veiculo);
                        _context.Motorista.Remove(motorista);
                            _context.SaveChanges();
                                transaction.Commit();
                }
                catch (Exception e) {
                    Console.WriteLine("Erro:");
                        Console.WriteLine(e);
                            transaction.Rollback();
                }
        }

        public void Update(Viagem form, Viagem banco)
        {
            var transaction = _context.Database.BeginTransaction();
                try{

                    //Confirmar quais dados serão atualizados
                    banco.OrigemCidadeId            = form.OrigemCidadeId;
                    banco.DestinoCidadeId           = form.DestinoCidadeId;
                    banco.MotoristaId               = form.MotoristaId;
                    banco.ToneladaCarga             = form.ToneladaCarga;
                    banco.ToneladaPrecoUnitario     = form.ToneladaPrecoUnitario;
                    banco.ValorTotalDespesa         = form.ValorTotalDespesa;
                    banco.DataChegada               = form.DataChegada;
                    banco.DataSaida                 = form.DataSaida;

                        _context.Viagem.Update(banco);
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
    }
}