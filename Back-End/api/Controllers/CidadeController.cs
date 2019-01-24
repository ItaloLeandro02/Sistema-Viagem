using System;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[Controller]")]
    //[Authorize()]
    public class CidadeController : Controller
    {
        private readonly ICidadeIbgeRepository _cidadeRepository;
        public CidadeController(ICidadeIbgeRepository cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }
        [HttpGet]
        public ActionResult<RetornoView<CidadeIbge>> GetAll()
        {
            string coluna = HttpContext.Request.Query["coluna"];
            int column = Convert.ToInt32(coluna);

            return Ok (new {data = _cidadeRepository.Get(column)});
        }

        [HttpGet("return-cidade")]
        public ActionResult<RetornoView<CidadeIbge>> GetCidade()
        {
            string cidade = HttpContext.Request.Query["cidade"];

            return Ok (new {data = _cidadeRepository.Get(cidade)});
        }

        [HttpGet("{id}", Name = "GetCidade")]
        public ActionResult<RetornoView<CidadeIbge>> GetById(int id)
        {
            var cidade = _cidadeRepository.Find(id);

            if (cidade == null)
            {
                return NotFound();
            }
            return Ok(new {data = cidade});
        }
    }
}