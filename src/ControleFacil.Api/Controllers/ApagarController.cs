using ControleFacil.Api.Contract.Apagar;
using ControleFacil.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Api.Controllers
{
    [ApiController]
    [Route("titulos-apagar")]
    public class ApagarController : BaseController
    {

        private readonly IService<ApagarRequestContract, ApagarResponseContract, long> _apagarService;
        private long _idUsuario;

        public ApagarController(
            IService<ApagarRequestContract, ApagarResponseContract, long> apagarService)
        {
            this._apagarService = apagarService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Adicionar(ApagarRequestContract contrato)
        {

            try
            {
                this._idUsuario = ObterIdUsuarioLogado();
                return Created("", await _apagarService.Adicionar(contrato, _idUsuario));
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Obter()
        {

            try
            {
                this._idUsuario = ObterIdUsuarioLogado();
                return Ok(await _apagarService.Obter(_idUsuario));
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Obter(long id)
        {

            try
            {
                this._idUsuario = ObterIdUsuarioLogado();
                return Ok(await _apagarService.Obter(id, _idUsuario));
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(long id, ApagarRequestContract contrato)
        {

            try
            {
                this._idUsuario = ObterIdUsuarioLogado();
                return Ok(await _apagarService.Atualizar(id, contrato, _idUsuario));
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Deletar(long id)
        {

            try
            {
                this._idUsuario = ObterIdUsuarioLogado();
                await _apagarService.Inativar(id, _idUsuario);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}