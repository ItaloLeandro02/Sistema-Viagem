using System.Collections.Generic;
using api.Models;

namespace api.Repository
{
    public interface ICidadeIbgeRepository
    {
        IEnumerable<CidadeIbge> Get(int coluna);
        IEnumerable<CidadeIbge> Get(string cidade);
        CidadeIbge Find(int id);
    }
}