using System.Collections.Generic;
using api.Models;
using api.Views;

namespace api.Repository
{
    public interface IViagemRepository
    {
        void Add(Viagem viagem);
        IEnumerable<Viagem> GetAll();
        IEnumerable<DashboardFaturamento> DashboardFaturamentoVeiculo();
        IEnumerable<DashboardComissao> DashboardComissao(string dataInicial, string dataFinal);
        IEnumerable<DashboardComissao> DashboardComissao();
        Viagem Find(int id);
        void Remove(int id);
        void Update(Viagem form, Viagem banco);
    }
}