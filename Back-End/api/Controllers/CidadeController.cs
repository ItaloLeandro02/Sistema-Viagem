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
                return Ok (
                    new {
                        data = _cidadeRepository.GetAll()
                    });
            }

            [HttpGet("{id}", Name = "GetCidade")]
            public ActionResult<RetornoView<CidadeIbge>> GetById(int id)
            {
                var cidade = _cidadeRepository.Find(id);

                    if (cidade == null)
                    {
                        return NotFound();
                    }
                        return Ok(
                            new {
                                data = cidade
                        });
            }
    }
}