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
            if (viagem.DataSaida.Date == new DateTime(0001,1,1,0,0,0).Date) 
            {
                var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "A Data de chegada e/ou data de saída não podem ser nulas." };
                return BadRequest(resultado);
            }

            if (viagem.DataChegada < viagem.DataSaida) 
            {
                var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "A Data de chegada não pode ser menor que a data de saída." };
                return BadRequest(resultado);
            }

            if (viagem.OrigemCidadeId == viagem.DestinoCidadeId)
            {
                var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "A cidade de origem não pode ser a mesma de destino." };
                return BadRequest(resultado);
            }

            if (!(viagem.OrigemCidadeId > 0) || !(viagem.DestinoCidadeId > 0))
            {
                var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "A cidade de origem e/ou cidade de destino não podem ser nulas." };
                return BadRequest(resultado);
            }

            if (!(viagem.MotoristaId > 0))
            {
                var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "Informe o motorista." };
                return BadRequest(resultado);
            }

            if (!(viagem.VeiculoId > 0))
            {
                var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "Informe o veiculo." };
                return BadRequest(resultado);
            }

            if (viagem.ToneladaPrecoUnitario <= 1)
            {
                var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "O preço da tonelada deve ser maior do que 1." };
                return BadRequest(resultado);
            }

            if (viagem.ToneladaCarga <= 1)
            {
                var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "A quantidade carregada deve ser maior do que 1." };
                return BadRequest(resultado);
            }

            //Por padrão será 0, caso tenha despesas vinculadas o valor será atualizado
            viagem.ValorTotalDespesa = 0;

            if (viagem.despesas != null) 
            {
                foreach (var item in viagem.despesas)
                {
                    item.Tipo = 1;
                    //Valida as regras de negócio para despesas
                    if (item.Historico.Length < 5) 
                    {
                        var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "O histórico deve conter no mínimo 5 caracteres." };
                        return BadRequest(resultado);
                    }

                    if (item.DataLancamento > viagem.DataChegada)
                    {
                        var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "A data de lançamento não pode ser maior do que a data de chegada." };
                        return BadRequest(resultado);
                    }

                    if (item.DataLancamento < viagem.DataSaida)
                    {
                        var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "A data de lançamento não pode ser menor do que a data de chegada." };
                        return BadRequest(resultado);
                    }

                    if (item.Valor <= 0)
                    {
                        var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "O valor da despesa deve ser maior do que 0." };
                        return BadRequest(resultado);
                    }

                    viagem.ValorTotalDespesa += item.Valor;
                }
            }

            if (viagem.combustivel != null) 
            {
                foreach (var item in viagem.combustivel)
                {
                    item.Tipo = 2;
                    //Valida as regras de negócio para despesas
                    if (item.Historico.Length < 5) 
                    {
                        var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "O histórico deve conter no mínimo 5 caracteres." };
                        return BadRequest(resultado);
                    }

                    if (item.DataLancamento > viagem.DataChegada)
                    {
                        var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "A data de lançamento não pode ser maior do que a data de chegada." };
                        return BadRequest(resultado);
                    }

                    if (item.DataLancamento < viagem.DataSaida)
                    {
                        var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "A data de lançamento não pode ser menor do que a data de chegada." };
                        return BadRequest(resultado);
                    }

                    if (item.Valor <= 0)
                    {
                        var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "O valor da despesa deve ser maior do que 0." };
                        return BadRequest(resultado);
                    }

                    viagem.Valor_Total_Combustivel += item.Valor;
                }
            }

            viagem.Valor_Imposto = ((viagem.ValorTotalBruto * 18) / 100);
            
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

            if (viagem.DataSaida.Date == new DateTime(0001,1,1,0,0,0).Date) 
            {
                var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "A Data de chegada e/ou data de saída não podem ser nulas." };
                return BadRequest(resultado);
            }

            if (viagem.DataChegada < viagem.DataSaida) 
            {
                var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "A Data de chegada não pode ser menor que a data de saída." };
                return BadRequest(resultado);
            }

            if (viagem.OrigemCidadeId == viagem.DestinoCidadeId)
            {
                var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "A cidade de origem não pode ser a mesma de destino." };
                return BadRequest(resultado);
            }

            if (!(viagem.OrigemCidadeId > 0) || !(viagem.DestinoCidadeId > 0))
            {
                var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "A cidade de origem e/ou cidade de destino não podem ser nulas." };
                return BadRequest(resultado);
            }

            if (!(viagem.MotoristaId > 0))
            {
                var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "Informe o motorista." };
                return BadRequest(resultado);
            }

            if (!(viagem.VeiculoId > 0))
            {
                var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "Informe o veiculo." };
                return BadRequest(resultado);
            }

            if (viagem.ToneladaPrecoUnitario <= 1)
            {
                var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "O preço da tonelada deve ser maior do que 1." };
                return BadRequest(resultado);
            }

            if (viagem.ToneladaCarga <= 1)
            {
                var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "A quantidade carregada deve ser maior do que 1." };
                return BadRequest(resultado);
            }

            //Por padrão será 0, caso tenha despesas vinculadas o valor será atualizado
            viagem.ValorTotalDespesa = 0;

            if (viagem.despesas != null) 
            {
                foreach (var item in viagem.despesas)
                {
                    //Valida as regras de negócio para despesas
                    if (item.Historico.Length < 5) 
                    {
                        var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "O histórico deve conter no mínimo 5 caracteres." };
                        return BadRequest(resultado);
                    }

                    if (item.DataLancamento > viagem.DataChegada)
                    {
                        var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "A data de lançamento não pode ser maior do que a data de chegada." };
                        return BadRequest(resultado);
                    }

                    if (item.DataLancamento < viagem.DataSaida)
                    {
                        var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "A data de lançamento não pode ser menor do que a data de chegada." };
                        return BadRequest(resultado);
                    }

                    if (item.Valor <= 0)
                    {
                        var resultado = new RetornoView<Motorista>() { sucesso = false, erro = "O valor da despesa deve ser maior do que 0." };
                        return BadRequest(resultado);
                    }

                    viagem.ValorTotalDespesa += item.Valor;
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