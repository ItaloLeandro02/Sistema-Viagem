using Microsoft.AspNetCore.Mvc;
using api.Repository;
using api.Models;

namespace api.Controllers
{
    [Route("api/[Controller]")]
    //[Authorize()]
    public class DespesaController : Controller
    {
        private readonly IViagemDespesaRepository _despesaRepository;
        public DespesaController(IViagemDespesaRepository despesaRepository)
        {
            _despesaRepository = despesaRepository;
        }
            [HttpPost]
            public ActionResult<RetornoView<ViagemDespesa>> Create([FromBody] ViagemDespesa despesa)
            {
                if (despesa == null)
                {
                    return BadRequest();
                }
                    _despesaRepository.Add(despesa);

                        if (despesa.Id > 0) 
                        {
                            var resultado = new RetornoView<ViagemDespesa>() { data = despesa, sucesso = true };
                                return CreatedAtRoute("GetDespesa", new { id = despesa.Id}, resultado);    
                        }
                        else 
                        {
                            var resultado = new RetornoView<ViagemDespesa>() { sucesso = false };
                                return BadRequest(resultado);
                        }
            }

            [HttpPut("{id}")]
            public ActionResult<RetornoView<ViagemDespesa>> Update(int id, [FromBody] ViagemDespesa despesa)
            {
                if (despesa == null) 
                {
                    return BadRequest();
                }

                //Verifica se os dados passados correspondem com as regras de negócio
                if ((despesa.Historico.Length < 5) || (despesa.Valor < 0))
                {
                    return BadRequest();
                }
                
                    var _despesa = _despesaRepository.Find(id);
                    
                        if(_despesa == null) 
                        {
                            return NotFound();
                        }
                            //despesa     = variável vinda do form
                            //_despesa    = variável vinda do banco
                            _despesaRepository.Update(despesa, _despesa);

                                if (_despesaRepository.Find(id) == _despesa)
                                {
                                    var resultado = new RetornoView<ViagemDespesa>() { data = _despesa, sucesso = true };
                                        return resultado;
                                }
                                else 
                                {
                                    var resultado = new RetornoView<ViagemDespesa>() { sucesso = false };
                                        return BadRequest(resultado);
                                } 
            }

            [HttpDelete("{id}")]
            public ActionResult<RetornoView<ViagemDespesa>> Delete(int id) 
            {
                var despesa  = _despesaRepository.Find(id);

                    if (despesa == null) 
                    {
                        return NotFound();
                    }
                        _despesaRepository.Remove(id);
                        
                            if (_despesaRepository.Find(id) == null) 
                            {
                                var resultado = new RetornoView<ViagemDespesa>() { sucesso = true };
                                    return resultado;
                            }
                            else 
                            {
                                var resultado = new RetornoView<ViagemDespesa>() { sucesso = false };
                                    return resultado;
                            }
            }
    }
}