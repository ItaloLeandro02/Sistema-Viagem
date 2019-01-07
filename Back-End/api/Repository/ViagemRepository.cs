using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using api.Views;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ViagemRepository : IViagemRepository
    {
        public  DbQuery<DashboardFaturamento> queryViagem{get; set;}
        private readonly DataDbContext _context;
        public ViagemRepository(DataDbContext ctx) 
        {
            _context = ctx;
        }
        public void Add(Viagem viagem)
        {
            var transaction = _context.Database.BeginTransaction();
                try 
                {
                    viagem.ValorTotalDespesa = 0;

                        if ((viagem.DataChegada < viagem.DataSaida) || (viagem.OrigemCidadeId == viagem.DestinoCidadeId) || (viagem.ToneladaPrecoUnitario < 1) || (viagem.ToneladaCarga < 1)) 
                        {
                            transaction.Rollback();
                                return;
                        }

                        if (viagem.despesas != null) 
                        {
                            foreach (var item in viagem.despesas)
                            {
                                //Valida as regras de negócio para despesas
                                if ((item.Historico.Length < 5) || (item.Valor < 0) || (item.DataLancamento < viagem.DataSaida) || (item.DataLancamento > viagem.DataChegada)) 
                                {
                                    transaction.Rollback();
                                        return;
                                }

                                    viagem.ValorTotalDespesa += item.Valor;
                            }
                        }
                        
                            viagem.ValorTotalBruto   = (viagem.ToneladaCarga * viagem.ToneladaPrecoUnitario);
                            viagem.ValorTotalLiquido = (viagem.ValorTotalBruto - viagem.ValorTotalDespesa);  

                            _context.Viagem.Add(viagem);
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

        public IEnumerable<DashboardFaturamento> Dashboard()
        {
            // var query = queryViagem.FromSql("SELECT * FROM motorista").DefaultIfEmpty().AsEnumerable();
            //     return query; 

            return _context.Teste.FromSql("SELECT count(veiculo.id) Id, veiculo.modelo Modelo, SUM(viagem.valorTotalLiquido) Total FROM veiculo, viagem WHERE veiculo.id = viagem.veiculoId  GROUP BY(veiculo.modelo)").DefaultIfEmpty().AsEnumerable();

            //var viagem = _context.Database.SqlQuery<DashboardFaturamento>().ToList();
            // return _context.Viagem
            // .Include(x => x.veiculo)
            // .Select(x => new DashboardFaturamento {
            //     Total = x.ValorTotalLiquido
            // })
            // .AsNoTracking()
            // .ToList();
        }

        public Viagem Find(int id)
        {
            //Preciso trabalhar aqui para não retornar dados sensíveis
            return _context.Viagem
            .Include(v => v.veiculo)
            .Include(m => m.motorista)
            .Include(o => o.cidadeOrigem)
            .Include(d => d.cidadeDestino)
            .Include(e => e.despesas)
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
            .Include(e => e.despesas)
            .ToList();
        }

        public void Remove(int id)
        {
            var transaction = _context.Database.BeginTransaction();

                try {

                    var viagem = _context.Viagem
                    .Where(v => v.Id == id)
                    .First();

                    var despesa = _context.ViagemDespesa
                    .Where(d => d.ViagemId == viagem.Id)
                    .First();

                        _context.Viagem.Remove(viagem);
                        _context.ViagemDespesa.Remove(despesa);
                            _context.SaveChanges();
                                transaction.Commit();
                }
                catch (Exception e) {
                    Console.WriteLine("Erro:");
                        Console.WriteLine(e);
                            transaction.Rollback();
                }
        }

        //Precisa ser melhorado?
        public void Update(Viagem form, Viagem banco)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (form.despesas != null) 
                    {
                        banco.ValorTotalDespesa = 0;
                            for (int i = 0; i < form.despesas.Count(); i++)
                            {
 
                                banco.despesas[i].DataLancamento  = form.despesas[i].DataLancamento;
                                banco.despesas[i].Historico       = form.despesas[i].Historico;
                                banco.despesas[i].Valor           = form.despesas[i].Valor;

                                banco.ValorTotalDespesa += form.despesas[i].Valor;
                            }
                    }

                    //Confirmar quais dados serão atualizados
                    banco.OrigemCidadeId            = form.OrigemCidadeId;
                    banco.DestinoCidadeId           = form.DestinoCidadeId;
                    banco.MotoristaId               = form.MotoristaId;
                    banco.ToneladaCarga             = form.ToneladaCarga;
                    banco.ToneladaPrecoUnitario     = form.ToneladaPrecoUnitario;
                    banco.DataChegada               = form.DataChegada;
                    banco.DataSaida                 = form.DataSaida;
                    
                    banco.ValorTotalBruto   = (banco.ToneladaCarga * banco.ToneladaPrecoUnitario);
                    banco.ValorTotalLiquido = (banco.ValorTotalBruto - banco.ValorTotalDespesa);

                        _context.Viagem.Update(banco);
                            _context.SaveChanges();
                                transaction.Commit();
                }
                //Preciso tratar para não precisar retornar erro 500
                catch (Exception e) 
                {
                    Console.WriteLine("Erro");
                        Console.WriteLine(e);
                            transaction.Rollback();
                                throw new System.Net.WebException (string.Format("Falha ao atualizar dados da viagem"));
                }
            }     
        }
    }
}