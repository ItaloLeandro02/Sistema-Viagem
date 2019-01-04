using System.Collections.Generic;
using System.Linq;
using api.Models;

namespace api.Repository
{
    public class CidadeIbgeRepository : ICidadeIbgeRepository
    {
        private readonly DataDbContext _context;
        public CidadeIbgeRepository(DataDbContext ctx)
        {
            _context = ctx;
        }
        public CidadeIbge Find(int id)
        {
            return _context.CidadeIbge.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<CidadeIbge> GetAll()
        {
            return _context.CidadeIbge.ToList();
        }
    }
}