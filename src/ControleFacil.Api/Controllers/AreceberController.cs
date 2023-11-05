using ControleFacil.Api.Contract.Areceber;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Api.Controllers
{
    [ApiController]
    [Route("titulos-areceber")]
    public class AreceberController : BaseController
    {

        private readonly IService<AreceberRequestContract, AreceberResponseContract, long> _areceberService;
        private long _idUsuario;

        public AreceberController(
            IService<AreceberRequestContract, AreceberResponseContract, long> areceberService)
        {
            this._areceberService = areceberService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Adicionar(AreceberRequestContract contrato)
        {

            try
            {
                this._idUsuario = ObterIdUsuarioLogado();
                return Created("", await _areceberService.Adicionar(contrato, _idUsuario));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(RetornarModelBadRequest(ex));
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
                return Ok(await _areceberService.Obter(_idUsuario));
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
                return Ok(await _areceberService.Obter(id, _idUsuario));
            }
            catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(long id, AreceberRequestContract contrato)
        {

            try
            {
                this._idUsuario = ObterIdUsuarioLogado();
                return Ok(await _areceberService.Atualizar(id, contrato, _idUsuario));
            }
            catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(RetornarModelBadRequest(ex));
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
                await _areceberService.Inativar(id, _idUsuario);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}