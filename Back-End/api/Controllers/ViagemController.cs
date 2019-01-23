using System.Collections.Generic;
using System.Linq;
using api.Models;
using api.Views;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

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

        [HttpGet("bruto-despesas-combustivel")]
        public ActionResult<RetornoView<DashboarFaturamentoDespesasCombustivel>> DashboardFaturamentoDespesasCombustivel()
        {
            string dataInicial  = HttpContext.Request.Query["dataInicial"];
            string dataFinal    = HttpContext.Request.Query["dataFinal"];

            return Ok (new {data = _viagemRepository.DashboardFaturamentoDespesasCombustivel(dataInicial, dataFinal)});
        }

        [HttpPost]
        public ActionResult<RetornoView<Viagem>> Create([FromBody] Viagem viagem)
        {
            try
            { 

                viagem.ValorTotalDespesa = 0;

                if (viagem.despesas != null)    
                {
                    foreach (var item in viagem.despesas)
                    {
                        item.Tipo = 1;
                        item.validacoes();

                        viagem.ValorTotalDespesa += item.Valor;
                    }
                }

                if (viagem.combustivel != null) 
                {
                    foreach (var item in viagem.combustivel)
                    {
                        item.Tipo = 2;
                        item.validacoes();
                        viagem.Valor_Total_Combustivel += item.Valor;
                    }
                }

                viagem.ValorTotalBruto   = (viagem.ToneladaCarga * viagem.ToneladaPrecoUnitario);
                viagem.ValorTotalLiquido = (viagem.ValorTotalBruto - viagem.ValorTotalDespesa);
                viagem.Valor_Imposto = ((viagem.ValorTotalBruto * 18) / 100);
                viagem.Validacoes(); 
                
                _viagemRepository.Add(viagem);
            }
            catch (Exception ex) 
            {
                var result = new RetornoView<Viagem>() { sucesso = false, erro = ex.Message };
                return BadRequest(result);
            }

            var resultado = new RetornoView<Viagem>() { data = viagem, sucesso = true };
            return CreatedAtRoute("GetViagem", new { id = viagem.Id}, resultado);    
        }

        [HttpPut("{id}")]
        public ActionResult<RetornoView<Viagem>> Update(int id, [FromBody] Viagem viagem)
        {

            var _viagem = _viagemRepository.Find(id);
                    
            if(_viagem == null) 
            {
                return NotFound();
            }

            try
            {
                
                viagem.ValorTotalDespesa = 0;
                 
                if (viagem.despesas != null) 
                {
                    foreach (var item in viagem.despesas)
                    {
                        item.validacoes();

                        viagem.ValorTotalDespesa += item.Valor;
                    }
                }
                
                _viagem.OrigemCidadeId            = viagem.OrigemCidadeId;
                _viagem.DestinoCidadeId           = viagem.DestinoCidadeId;
                _viagem.MotoristaId               = viagem.MotoristaId;
                _viagem.ToneladaCarga             = viagem.ToneladaCarga;
                _viagem.ToneladaPrecoUnitario     = viagem.ToneladaPrecoUnitario;
                _viagem.DataChegada               = viagem.DataChegada;
                _viagem.DataSaida                 = viagem.DataSaida;
                
                _viagem.ValorTotalBruto   = (_viagem.ToneladaCarga * _viagem.ToneladaPrecoUnitario);
                _viagem.ValorTotalLiquido = (_viagem.ValorTotalBruto - _viagem.ValorTotalDespesa);

                _viagem.Validacoes();

                _viagemRepository.Update(_viagem);
            }
            catch (Exception ex)
            {
                var result = new RetornoView<Viagem>() { sucesso = false, erro = ex.Message };
                return BadRequest(result);
            }

            var resultado = new RetornoView<Viagem>() { data = _viagem, sucesso = true };
            return resultado;
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
                return BadRequest(resultado);
            }
        }
    }
}