using System.Collections.Generic;
using api.Models;

namespace api.Repository
{
    public interface IViagemDespesaRepository
    {
        void Add(ViagemDespesa despesa);
        ViagemDespesa Find(int id);
        void Remove(int id);
        void Update(ViagemDespesa form, ViagemDespesa banco);
    }
}