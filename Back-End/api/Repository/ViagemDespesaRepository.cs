using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ViagemDespesaRepository : IViagemDespesaRepository
    {
        private readonly DataDbContext _context;
        public ViagemDespesaRepository(DataDbContext ctx) 
        {
            _context = ctx;
        }
        public void Add(ViagemDespesa despesa)
        {
            var transaction = _context.Database.BeginTransaction();
                try {

                    var viagem = _context.Viagem.First(u => u.Id == despesa.ViagemId);

                    if ((despesa.Historico.Length < 5) || (despesa.Valor < 0) || (despesa.DataLancamento < viagem.DataSaida) || (despesa.DataLancamento < viagem.DataChegada)) 
                    {
                        transaction.Rollback();
                            return;
                    }

                        //Inclue as despesas na viagem
                        //Soma a cada despesa adicionada
                        //viagem.ValorTotalDespesa += despesa.Valor;
                        viagem.ValorTotalDespesa = despesa.Valor;
                        viagem.ValorTotalLiquido = (viagem.ValorTotalBruto - viagem.ValorTotalDespesa); 

                            _context.ViagemDespesa.Add(despesa);
                            _context.Viagem.Update(viagem);
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

        public ViagemDespesa Find(int id)
        {
            //Preciso trabalhar aqui para não retornar dados sensíveis
            return _context.ViagemDespesa
            //.Include(v => v.viagem.veiculo)
            //.Include(v => v.viagem.motorista)
            //.Include(v => v.viagem.cidadeOrigem)
            //.Include(v => v.viagem.cidadeDestino)
            .FirstOrDefault(u => u.Id == id);
        }
        
        public void Remove(int id)
        {
            var transaction = _context.Database.BeginTransaction();

                try {

                    var despesa = _context.ViagemDespesa
                    .Where(v => v.Id == id)
                    .First();
                    
                    var viagem = _context.Viagem
                    .Where(u => u.Id == despesa.ViagemId)
                    .Include(u => u.despesas)
                    .First();

                        viagem.ValorTotalDespesa = 0;

                            foreach (var item in viagem.despesas)
                            {
                                viagem.ValorTotalDespesa += item.Valor; 
                            }

                                viagem.ValorTotalDespesa = viagem.ValorTotalDespesa - despesa.Valor;
                                viagem.ValorTotalLiquido = (viagem.ValorTotalBruto - viagem.ValorTotalDespesa);

                                    _context.ViagemDespesa.Remove(despesa);
                                    _context.Viagem.Update(viagem);
                                        _context.SaveChanges();
                                            transaction.Commit();
                }
                catch (Exception e) {
                    Console.WriteLine("Erro:");
                        Console.WriteLine(e);
                            transaction.Rollback();
                }
        }

        public void Update(ViagemDespesa form, ViagemDespesa banco)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    //Confirmar quais dados serão atualizados
                    banco.DataLancamento      = form.DataLancamento;
                    banco.Historico           = form.Historico;
                    banco.Valor               = form.Valor;
                    

                        _context.ViagemDespesa.Update(banco);
                            _context.SaveChanges();
                                transaction.Commit();
                }
                //Preciso tratar para não precisar retornar erro 500
                catch (Exception e) 
                {
                    Console.WriteLine("Erro");
                        Console.WriteLine(e);
                            transaction.Rollback();
                                throw new System.Net.WebException (string.Format("Falha ao atualizar dados da despesa"));
                }
            }     
        }
    }
}