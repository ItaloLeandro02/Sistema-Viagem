using System.Collections.Generic;
using api.Models;

namespace api.Repository
{
    public interface IViagemRepository
    {
        void Add(Viagem viagem);
        IEnumerable<Viagem> GetAll();
        Viagem Find(int id);
        void Remove(int id);
        void Update(Viagem form, Viagem banco);
    }
}