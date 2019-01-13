using System.Collections.Generic;
using System.Linq;
using api.Models;
using api.Views;
using api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[Controller]")]
    //[Authorize()]
    public class ViagemController : Controller
    {
        private readonly IViagemRepository _viagemRepository;
        public ViagemController(IViagemRepository viagemRepository)
        {
            _viagemRepository = viagemRepository;
        }
            [HttpGet]
            public ActionResult<RetornoView<Viagem>> GetAll()
            {
                return Ok (new {data = _viagemRepository.GetAll()});
            }

            [HttpGet("{id}", Name = "GetViagem")]
            public ActionResult<RetornoView<Viagem>> GetById(int id)
            {
                var viagem = _viagemRepository.Find(id);

                if (viagem == null)
                {
                    return NotFound();
                }
                return Ok(new {data = viagem});
            }

            [HttpGet("faturamento-veiculo")]
            public ActionResult<RetornoView<DashboardFaturamento>> DashboardFaturamentoVeiculo()
            {
                return Ok (new {data = _viagemRepository.DashboardFaturamentoVeiculo()});
            }

            [HttpGet("comissao")]
            public ActionResult<RetornoView<DashboardComissao>> DashboardComissao()
            {

                string dataInicial  = HttpContext.Request.Query["dataInicial"];
                string dataFinal    = HttpContext.Request.Query["dataFinal"];

                if (dataInicial == null && dataFinal == null) {
                    return Ok (new {data = _viagemRepository.DashboardComissao()});
                }
                else
                {
                    return Ok (new {data = _viagemRepository.DashboardComissao(dataInicial, dataFinal)});
                }
            }

            [HttpGet("faturamento-uf")]
            public ActionResult<RetornoView<DashboardFaturamentoUf>> DashboardFaturamentoUf()
            {

                string dataInicial  = HttpContext.Request.Query["dataInicial"];
                string dataFinal    = HttpContext.Request.Query["dataFinal"];

                return Ok (new {data = _viagemRepository.DashboardFaturamentoUf(dataInicial, dataFinal)});
            }

            [HttpGet("mapa-brasil")]
            public ActionResult<RetornoView<DashboardMapaBrasil>> DashboardMapaBrasil()
            {
                return Ok (new {data = _viagemRepository.DashboardMapaBrasil()});
            }

            [HttpPost]
            public ActionResult<RetornoView<Viagem>> Create([FromBody] Viagem viagem)
            {
                if (viagem == null)
                {
                    return BadRequest();
                }

                if ((viagem.DataChegada < viagem.DataSaida) || (viagem.OrigemCidadeId == viagem.DestinoCidadeId) || (viagem.ToneladaPrecoUnitario <= 1) || (viagem.ToneladaCarga <= 1)) 
                {
                    return BadRequest();
                }

                //Por padrão será 0, caso tenha despesas vinculadas o valor será atualizado
                viagem.ValorTotalDespesa = 0;

                if (viagem.despesas != null) 
                {
                    foreach (var item in viagem.despesas)
                    {
                        //Valida as regras de negócio para despesas
                        if ((item.Historico.Length < 5) || (item.Valor <= 0) || (item.DataLancamento < viagem.DataSaida) || (item.DataLancamento > viagem.DataChegada)) 
                        {
                            return BadRequest();
                        }

                        viagem.ValorTotalDespesa += item.Valor;
                    }
                }
                
                _viagemRepository.Add(viagem);

                if (viagem.Id > 0) 
                {
                    var resultado = new RetornoView<Viagem>() { data = viagem, sucesso = true };
                    return CreatedAtRoute("GetViagem", new { id = viagem.Id}, resultado);    
                }
                else 
                {
                    var resultado = new RetornoView<Viagem>() { sucesso = false };
                    return BadRequest(resultado);
                }
            }

            [HttpPut("{id}")]
            public ActionResult<RetornoView<Viagem>> Update(int id, [FromBody] Viagem viagem)
            {
                if (viagem == null) 
                {
                    return BadRequest();
                }

                //Verifica se os dados passados correspondem com as regras de negócio
                if ((viagem.DataChegada < viagem.DataSaida) || (viagem.OrigemCidadeId == viagem.DestinoCidadeId) || (viagem.ToneladaPrecoUnitario <= 1) || (viagem.ToneladaCarga <= 1)) 
                {
                    return BadRequest();
                }

                if (viagem.despesas != null) 
                {
                    foreach (var item in viagem.despesas)
                    {
                        //Valida as regras de negócio para despesas
                        if ((item.Historico.Length < 5) || (item.Valor <= 0) || (item.DataLancamento < viagem.DataSaida) || (item.DataLancamento > viagem.DataChegada)) 
                        {
                            return BadRequest();
                        }
                    }
                }
                
                var _viagem = _viagemRepository.Find(id);
                        
                if(_viagem == null) 
                {
                    return NotFound();
                }

                if (viagem.despesas != null) 
                {
                    _viagem.ValorTotalDespesa = 0;
                    
                    for (int i = 0; i < viagem.despesas.Count(); i++)
                    {

                    _viagem.despesas[i].DataLancamento  = viagem.despesas[i].DataLancamento;
                    _viagem.despesas[i].Historico       = viagem.despesas[i].Historico;
                    _viagem.despesas[i].Valor           = viagem.despesas[i].Valor;

                    _viagem.ValorTotalDespesa += viagem.despesas[i].Valor;
                    }
                }
                //viagem     = variável vinda do form
                //_viagem    = variável vinda do banco
                _viagemRepository.Update(viagem, _viagem);

                if (_viagemRepository.Find(id) == _viagem)
                {
                    var resultado = new RetornoView<Viagem>() { data = _viagem, sucesso = true };
                    return resultado;
                }
                else 
                {
                    var resultado = new RetornoView<Viagem>() { sucesso = false };
                    return BadRequest(resultado);
                } 
            }

            [HttpDelete("{id}")]
            public ActionResult<RetornoView<Viagem>> Delete(int id) 
            {
                var viagem  = _viagemRepository.Find(id);

                if (viagem == null) 
                {
                    return NotFound();
                }
                _viagemRepository.Remove(id);
                        
                if (_viagemRepository.Find(id) == null) 
                {
                    var resultado = new RetornoView<Viagem>() { sucesso = true };
                    return resultado;
                }
                else 
                {
                    var resultado = new RetornoView<Viagem>() { sucesso = false };
                    return resultado;
                }
            }
    }
}