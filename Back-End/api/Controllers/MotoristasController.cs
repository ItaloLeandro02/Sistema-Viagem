using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[Controller]")]
    //[Authorize()]
    public class MotoristasController : Controller
    {
        private readonly IMotoristaRepository _motoristaRepository;
        public MotoristasController(IMotoristaRepository motoristaRepository)
        {
            _motoristaRepository = motoristaRepository;
        }
            [HttpGet]
            public ActionResult<RetornoView<Motorista>> GetAll()
            {
                return Ok (
                    new {
                        data = _motoristaRepository.GetAll()
                    });
            }

            [HttpGet("{id}", Name = "GetMotorista")]
            public ActionResult<RetornoView<Motorista>> GetById(int id)
            {
                var motorista = _motoristaRepository.Find(id);

                    if (motorista == null)
                    {
                        return NotFound();
                    }
                        return Ok(
                            new {
                                data = motorista
                        });
            }

            [HttpPost]
            public ActionResult<RetornoView<Motorista>> Create([FromBody] Motorista motorista)
            {
                if (motorista == null)
                {
                    return BadRequest();
                }
                    _motoristaRepository.Add(motorista);

                        if (motorista.Id > 0) 
                        {
                            var resultado = new RetornoView<Motorista>() { data = motorista, sucesso = true };
                                return CreatedAtRoute("GetMotorista", new { id = motorista.Id}, resultado);    
                        }
                        else 
                        {
                            var resultado = new RetornoView<Motorista>() { sucesso = false };
                                return BadRequest(resultado);
                        }
            }

            [HttpPut("{id}")]
            public ActionResult<RetornoView<Motorista>> Update(int id, [FromBody] Motorista motorista)
            {
                if (motorista == null) 
                {
                    return BadRequest();
                }

                //Verifica se o nome passado no formulário tem no mínimo 3 caracteres
                if (motorista.Nome.Length < 3) 
                {
                    var resultado = new RetornoView<Motorista>() { sucesso = false };
                        return BadRequest(resultado);
                }
                
                    var _motorista = _motoristaRepository.Find(id);
                    
                        if(_motorista == null) 
                        {
                            return NotFound();
                        }
                            //motorista     = variável vinda do form
                            //_motorista    = variável vinda do banco
                            _motoristaRepository.Update(motorista, _motorista);

                                if (_motoristaRepository.Find(id).Equals(_motorista))
                                {
                                    var resultado = new RetornoView<Motorista>() { data = _motorista, sucesso = true };
                                        return resultado;
                                }
                                else 
                                {
                                    var resultado = new RetornoView<Motorista>() { sucesso = false };
                                        return BadRequest(resultado);
                                } 
            }

            [HttpDelete("{id}")]
            public ActionResult<RetornoView<Motorista>> Delete(int id) 
            {
                var motorista  = _motoristaRepository.Find(id);

                    if (motorista == null) 
                    {
                        return NotFound();
                    }
                        _motoristaRepository.Remove(id);
                        
                            if (_motoristaRepository.Find(id) == null) 
                            {
                                var resultado = new RetornoView<Motorista>() { sucesso = true };
                                    return resultado;
                            }
                            else 
                            {
                                var resultado = new RetornoView<Motorista>() { sucesso = false };
                                    return resultado;
                            }
            }
    }
}