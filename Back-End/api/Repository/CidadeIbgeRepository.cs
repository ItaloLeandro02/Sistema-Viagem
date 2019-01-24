using System;
using System.Collections.Generic;
using System.Linq;
using api.Models;
using Microsoft.EntityFrameworkCore;

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

        public IEnumerable<CidadeIbge> Get(int coluna)
        {
            var result =  _context.CidadeIbge
            .FromSql("SELECT * FROM cidadeibge "  
	                    + "ORDER BY id "
		                    + "OFFSET " + coluna + " Rows" 
			                    + " FETCH NEXT 10 ROWS ONLY")
            .DefaultIfEmpty()
            .AsEnumerable();

            return result;
        }

        public IEnumerable<CidadeIbge> Get(string cidade)
        {
            var result =  _context.CidadeIbge
            .FromSql("SELECT * FROM cidadeibge "  
                        +"WHERE cidade like '%" + cidade + "%' "
                            + "ORDER BY id "
                                + "OFFSET 0 Rows" 
                                    + " FETCH NEXT 10 ROWS ONLY")
            .DefaultIfEmpty()
            .AsEnumerable();

            return result;
        }
    }
}