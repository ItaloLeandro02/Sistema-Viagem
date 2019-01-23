using System;
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
            return Ok (new {data = _veiculoRepository.GetAll()});
        }

        [HttpGet("{id}", Name = "GetVeiculo")]
        public ActionResult<RetornoView<Veiculo>> GetById(int id)
        {
            var veiculo = _veiculoRepository.Find(id);

            if (veiculo == null)
            {
                return NotFound();
            }
            return Ok(new {data = veiculo});
        }

        [HttpPost]
        public ActionResult<RetornoView<Veiculo>> Create([FromBody] Veiculo veiculo)
        {
            try
            {
                veiculo.validacoes();
                _veiculoRepository.Add(veiculo);

            }
            catch (Exception ex)
            {
                var result = new RetornoView<Veiculo>() { sucesso = false, erro = ex.Message };
                return BadRequest(result);
            }

                var resultado = new RetornoView<Veiculo>() { data = veiculo, sucesso = true };
                return CreatedAtRoute("GetVeiculo", new { id = veiculo.Id}, resultado);   
        }

        [HttpPut("{id}")]
        public ActionResult<RetornoView<Veiculo>> Update(int id, [FromBody] Veiculo veiculo)
        {
            var _veiculo = _veiculoRepository.Find(id);
            if(_veiculo == null) 
            {
                return NotFound();
            }
            
            try
            {
                veiculo.validacoes();
                
                _veiculo.AnoFabricacao = veiculo.AnoFabricacao;
                _veiculo.AnoModelo     = veiculo.AnoModelo;
                _veiculo.Desativado    = veiculo.Desativado;
                _veiculo.Fabricante    = veiculo.Fabricante;
                _veiculo.Modelo        = veiculo.Modelo;
                _veiculoRepository.Update(veiculo, _veiculo);
            }
            catch (Exception ex)
            {
                var result = new RetornoView<Veiculo>() { sucesso = false, erro = ex.Message };
                return BadRequest(result);
            }

            var resultado = new RetornoView<Veiculo>() { data = _veiculo, sucesso = true };
            return resultado;
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