using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ControleFacil.Api.Contract;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected long ObterIdUsuarioLogado()
        {
            var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            long.TryParse(id, out long IdUsuario);

            return IdUsuario;
        }

        protected ModelErrorContract RetornarModelBadRequest(Exception ex)
        {
            return new ModelErrorContract
            {
                StatusCode = 400,
                Title = "Bad request",
                Message = ex.Message,
                DateTime = DateTime.Now
            };
        }

        protected ModelErrorContract RetornarModelNotFound(Exception ex)
        {
            return new ModelErrorContract
            {
                StatusCode = 404,
                Title = "NotFound",
                Message = ex.Message,
                DateTime = DateTime.Now
            };
        }

        protected ModelErrorContract RetornarModelUnauthorized(Exception ex)
        {
            return new ModelErrorContract
            {
                StatusCode = 401,
                Title = "Unauthorized",
                Message = ex.Message,
                DateTime = DateTime.Now
            };
        }
    }
}