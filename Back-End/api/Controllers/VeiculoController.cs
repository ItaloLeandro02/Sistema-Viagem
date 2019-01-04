using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[Controller]")]
    //[Authorize()]
    public class VeiculoController : Controller
    {
        private readonly IVeiculoRepository _veiculoRepository;
        public VeiculoController(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }
            [HttpGet]
            public ActionResult<RetornoView<Veiculo>> GetAll()
            {
                return Ok (
                    new {
                        data = _veiculoRepository.GetAll()
                    });
            }

            [HttpGet("{id}", Name = "GetVeiculo")]
            public ActionResult<RetornoView<Veiculo>> GetById(int id)
            {
                var veiculo = _veiculoRepository.Find(id);

                    if (veiculo == null)
                    {
                        return NotFound();
                    }
                        return Ok(
                            new {
                                data = veiculo
                        });
            }

            [HttpPost]
            public ActionResult<RetornoView<Veiculo>> Create([FromBody] Veiculo veiculo)
            {
                if (veiculo == null)
                {
                    return BadRequest();
                }
                    _veiculoRepository.Add(veiculo);

                        if (veiculo.Id > 0) 
                        {
                            var resultado = new RetornoView<Veiculo>() { data = veiculo, sucesso = true };
                                return CreatedAtRoute("GetVeiculo", new { id = veiculo.Id}, resultado);    
                        }
                        else 
                        {
                            var resultado = new RetornoView<Veiculo>() { sucesso = false };
                                return BadRequest(resultado);
                        }
            }

            [HttpPut("{id}")]
            public ActionResult<RetornoView<Veiculo>> Update(int id, [FromBody] Veiculo veiculo)
            {
                if (veiculo == null) 
                {
                    return BadRequest();
                }

                //Verifica se o ano de fabricação é maior do que 2000
                //Verifica se o ano do modelo é maior do que o de fabicação
                if ((veiculo.AnoFabricacao < 2000) || (veiculo.AnoModelo < veiculo.AnoFabricacao))
                {
                    return BadRequest();
                }
                    var _veiculo = _veiculoRepository.Find(id);
                    
                        if(_veiculo == null) 
                        {
                            return NotFound();
                        }
                            //veiculo     = variável vinda do form
                            //_veiculo    = variável vinda do banco
                            _veiculoRepository.Update(veiculo, _veiculo);

                                if (_veiculoRepository.Find(id).Equals(_veiculo))
                                {
                                    var resultado = new RetornoView<Veiculo>() { data = _veiculo, sucesso = true };
                                        return resultado;
                                }
                                else 
                                {
                                    var resultado = new RetornoView<Veiculo>() { sucesso = false };
                                        return BadRequest(resultado);
                                } 
            }

            [HttpDelete("{id}")]
            public ActionResult<RetornoView<Veiculo>> Delete(int id) 
            {
                var veiculo  = _veiculoRepository.Find(id);

                    if (veiculo == null) 
                    {
                        return NotFound();
                    }
                        _veiculoRepository.Remove(id);
                        
                            if (_veiculoRepository.Find(id) == null) 
                            {
                                var resultado = new RetornoView<Veiculo>() { sucesso = true };
                                    return resultado;
                            }
                            else 
                            {
                                var resultado = new RetornoView<Veiculo>() { sucesso = false };
                                    return resultado;
                            }
            }
    }
}