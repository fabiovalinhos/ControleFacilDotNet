using System.Security.Authentication;
using ControleFacil.Api.Contract.Usuario;
using ControleFacil.Api.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Api.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            this._usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Autenticar(UsuarioLoginRequestContract contrato)
        {

            try
            {
                return Ok(await _usuarioService.Autenticar(contrato));
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(new
                {
                    statusCode = 401,
                    message = ex.Message
                });
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Adicionar(UsuarioRequestContract contrato)
        {

            try
            {
                return Created("", await _usuarioService.Adicionar(contrato, 0));
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Obter()
        {

            try
            {
                return Ok(await _usuarioService.Obter(0));
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
                return Ok(await _usuarioService.Obter(id, 0));
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(long id, UsuarioRequestContract contrato)
        {

            try
            {
                return Ok(await _usuarioService.Atualizar(id, contrato, 0));
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
                await _usuarioService.Inativar(id, 0);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}