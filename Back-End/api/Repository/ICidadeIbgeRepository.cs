using System.Collections.Generic;
using api.Models;

namespace api.Repository
{
    public interface ICidadeIbgeRepository
    {
        IEnumerable<CidadeIbge> GetAll();
        CidadeIbge Find(int id);
    }
}