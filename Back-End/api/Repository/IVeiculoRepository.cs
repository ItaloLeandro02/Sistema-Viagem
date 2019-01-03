using System.Collections.Generic;
using api.Models;

namespace api.Repository
{
    public interface IVeiculoRepository
    {
        void Add(Veiculo veiculo);
        IEnumerable<Veiculo> GetAll();
        Veiculo Find(int id);
        void Remove(int id);
        void Update(Veiculo form, Veiculo banco);
    }
}