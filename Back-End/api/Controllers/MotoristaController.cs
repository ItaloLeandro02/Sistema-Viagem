using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Formatting;

namespace api.Controllers
{
    [Route("api/[Controller]")]
    //[Authorize()]
    public class MotoristaController : Controller
    {
        private readonly IMotoristaRepository _motoristaRepository;
        public MotoristaController(IMotoristaRepository motoristaRepository)
        {
            _motoristaRepository = motoristaRepository;
        }
        [HttpGet]
        public ActionResult<RetornoView<Motorista>> GetAll()
        {
            return Ok (new {data = _motoristaRepository.GetAll()});
        }

        [HttpGet("{id}", Name = "GetMotorista")]
        public ActionResult<RetornoView<Motorista>> GetById(int id)
        {
            var motorista = _motoristaRepository.Find(id);

            if (motorista == null)
            {
                return NotFound();
            }
            return Ok(new {data = motorista});
        }

        [HttpPost]
        [Route("")]
        public ActionResult<RetornoView<Motorista>> Create([FromBody] Motorista motorista)
        {
           try
           {
               motorista.validacoes();
               _motoristaRepository.Add(motorista);
           }
           catch (Exception ex)
           {
               var resultado = new RetornoView<Motorista>() { sucesso = false, erro = ex.Message };
               return BadRequest(resultado);
           }
  
            var result = new RetornoView<Motorista>() { data = motorista, sucesso = true };
            return CreatedAtRoute("GetMotorista", new { id = motorista.Id}, result);    
        }

        [HttpPut("{id}")]
        public ActionResult<RetornoView<Motorista>> Update(int id, [FromBody] Motorista motorista)
        {
            
        try 
        {
            motorista.validacoes();
            var _motorista = _motoristaRepository.Find(id);
            _motoristaRepository.Update(motorista, _motorista);
        }
        catch(Exception ex)
        {
            var result = new RetornoView<Motorista>() { sucesso = false, erro = ex.Message };
            return BadRequest(result);
        }
            
        var resultado = new RetornoView<Motorista>() { data = _motoristaRepository.Find(id), sucesso = true };
        return resultado;
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