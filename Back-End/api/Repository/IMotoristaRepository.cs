using System.Collections.Generic;
using api.Models;

namespace api.Repository
{
    public interface IMotoristaRepository
    {
        void Add(Motorista motorista);
        IEnumerable<Motorista> GetAll();
        Motorista Find(int id);
        void Remove(int id);
        void Update(Motorista form, Motorista banco);
    }
}