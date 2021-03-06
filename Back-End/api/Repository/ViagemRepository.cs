using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using api.Models;
using api.Views;
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

            using (var transaction = _context.Database.BeginTransaction())
            {
                try 
                {
                    _context.Viagem.Add(viagem);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e) 
                {
                    Console.WriteLine("Erro");
                    Console.WriteLine(e);
                    transaction.Rollback();
                }
            }
        }

        public IEnumerable<DashboardComissao> DashboardComissao(string dataInicial, string dataFinal)
        {   
            return _context.Comissao
            .FromSql("SELECT ROW_NUMBER() OVER(ORDER BY mo.nome ASC) Id, NULL Mes, " +
            "mo.nome Nome, SUM(vi.valorTotalLiquido) Total, (SUM(vi.valorTotalLiquido) * 0.1) Comissao FROM viagem vi JOIN motorista mo " +
            " ON vi.motoristaId = mo.id WHERE vi.dataChegada BETWEEN '" + dataInicial + "' AND '" + dataFinal + "' GROUP BY mo.nome")
            .DefaultIfEmpty()
            .AsEnumerable();
        }

        public IEnumerable<DashboardComissao> DashboardComissao()
        {
            return _context.Comissao
            .FromSql("SELECT ROW_NUMBER() OVER(ORDER BY mo.nome ASC) Id, NULL Mes, mo.nome Nome, SUM(vi.valorTotalLiquido) Total," +
            " (SUM(vi.valorTotalLiquido) * 0.1) Comissao FROM viagem vi JOIN motorista mo ON vi.motoristaId = mo.id GROUP BY mo.nome")
            .DefaultIfEmpty()
            .AsEnumerable();
        }

        public IEnumerable<DashboardFaturamentoUf> DashboardFaturamentoUf(string dataInicial, string dataFinal)
        {
            return _context.FaturamentoUf
            .FromSql("SELECT ROW_NUMBER() OVER(ORDER BY ci.uf ASC) Id, SUM(vi.valorTotalBruto) Faturamento, SUM(vi.valorTotalDespesa) Despesa, ci.uf Uf" +
            " FROM viagem vi JOIN cidadeibge ci ON vi.destinoCidadeId = ci.id WHERE vi.dataChegada BETWEEN '" + dataInicial + "' AND '" + dataFinal + "' GROUP BY ci.uf")
            .DefaultIfEmpty()
            .AsEnumerable();
        }

        public IEnumerable<DashboardFaturamento> DashboardFaturamentoVeiculo()
        {
            return _context.Faturamento
            .FromSql($"SELECT  ROW_NUMBER() OVER(ORDER BY ve.modelo ASC) Id, (SELECT DATEPART ( MONTH, vi.dataChegada)) Mes, ve.modelo Modelo, " +
            "SUM(vi.valorTotalLiquido) Total FROM veiculo as ve, viagem as vi WHERE ve.id = vi.veiculoId  GROUP BY vi.dataChegada, ve.modelo")
            .DefaultIfEmpty()
            .AsEnumerable();
        }

        public IEnumerable<DashboardMapaBrasil> DashboardMapaBrasil()
        {
            return _context.MapaBrasil
            .FromSql("SELECT ROW_NUMBER() OVER(ORDER BY ci.uf ASC) Id, SUM(vi.valorTotalBruto) Faturamento," +
            " SUM(vi.valorTotalDespesa) Despesa, SUM(vi.valorTotalLiquido) Liquido, ci.uf Uf FROM viagem vi JOIN cidadeibge ci ON vi.destinoCidadeId = ci.id  GROUP BY ci.uf")
            .DefaultIfEmpty()
            .AsEnumerable();
        }

        public IEnumerable<DashboarFaturamentoDespesasCombustivel> DashboardFaturamentoDespesasCombustivel(string dataInicial, string dataFinal)
        {
            return _context.FaturamentoDespesasCombustivel
            .FromSql("SELECT ROW_NUMBER() OVER(ORDER BY vi.dataChegada ASC) Id, SUM(vi.valorTotalBruto) Bruto, SUM(vi.valorTotalDespesa) Despesas, SUM(vi.valor_total_combustivel) Combustivel, (SELECT DATEPART ( MONTH, vi.dataChegada)) Mes" +
            " FROM viagem vi WHERE vi.dataChegada BETWEEN '" + dataInicial + "' AND '" + dataFinal + "' GROUP BY vi.dataChegada")
            .DefaultIfEmpty()
            .AsEnumerable();
        }

        public Viagem Find(int id)
        {
            var teste = _context.Viagem
            .Include(v => v.veiculo)
            .Include(m => m.motorista)
            .Include(o => o.cidadeOrigem)
            .Include(d => d.cidadeDestino)
            .Include(e => e.despesas)
            .Include(e => e.combustivel)
            .FirstOrDefault(u => u.Id == id);
            
            if (teste == null)
                return teste;

            var viagem = new Viagem();
            viagem.Id = teste.Id;
            viagem.MotoristaId = teste.MotoristaId;
            viagem.motorista = teste.motorista;
            viagem.VeiculoId = teste.VeiculoId;
            viagem.veiculo = teste.veiculo;
            viagem.OrigemCidadeId = teste.OrigemCidadeId;
            viagem.cidadeOrigem = teste.cidadeOrigem;
            viagem.DestinoCidadeId = teste.DestinoCidadeId;
            viagem.cidadeDestino = teste.cidadeDestino;
            viagem.despesas = new List<ViagemDespesa>();
            viagem.combustivel = new List<CombustivelDTO>();

            teste.despesas.ForEach(item => {
                if (item.Tipo == 1)
                {
                    viagem.despesas.Add(item);
                }
            });

            teste.combustivel.ForEach(item => {
                if (item.Tipo == 2)
                {
                    viagem.combustivel.Add(item);
                }
            });

            return viagem;
        }

        public IEnumerable<Viagem> GetAll()
        {
            var teste = _context.Viagem
            .Include(v => v.veiculo)
            .Include(m => m.motorista)
            .Include(o => o.cidadeOrigem)
            .Include(d => d.cidadeDestino)
            .Include(e => e.despesas)
            .Include(e => e.combustivel)
            .ToList();

            var viagens = new List<Viagem>();

            teste.ForEach(item => {

                var viagem = new Viagem();
                viagem.Id = item.Id;
                viagem.MotoristaId = item.MotoristaId;
                viagem.motorista = item.motorista;
                viagem.VeiculoId = item.VeiculoId;
                viagem.veiculo = item.veiculo;
                viagem.OrigemCidadeId = item.OrigemCidadeId;
                viagem.cidadeOrigem = item.cidadeOrigem;
                viagem.DestinoCidadeId = item.DestinoCidadeId;
                viagem.cidadeDestino = item.cidadeDestino;
                viagem.despesas = new List<ViagemDespesa>();
                viagem.combustivel = new List<CombustivelDTO>();

                item.despesas.ForEach(despesa => {
                    if (despesa.Tipo == 1)
                    {
                        viagem.despesas.Add(despesa);
                    }
                });

                item.combustivel.ForEach(combustivel => {
                    if (combustivel.Tipo == 2)
                    {
                        viagem.combustivel.Add(combustivel);
                    }
                });

                viagens.Add(viagem);
            });
          

            return viagens;
        }

        public void Remove(int id)
        {
            using (var transaction = _context.Database.BeginTransaction()) 
            {
                try {

                    _context.Database.ExecuteSqlCommand("DELETE FROM viagem WHERE id = " + id);

                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e) {
                    Console.WriteLine("Erro:");
                    Console.WriteLine(e);
                    transaction.Rollback();
                }
            }
        }

        //Precisa ser melhorado?
        public void Update(Viagem viagem)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Viagem.Update(viagem);
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