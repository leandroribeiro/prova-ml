using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProvaML.API.Model;
using ProvaML.Application;
using ProvaML.Domain.Entities;
using ProvaML.Domain.Repositories;

namespace ProvaML.API.Controllers
{
    [ApiController]
    [Route("login")]
    public class AutenticacaoController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult Post([FromBody] AutenticarRequest request)
        {
            if(request.Usuario == "teste" && request.Senha == "teste")
                return Ok(new { success = true });
            else
                return BadRequest("Usuário e/ou senha inválido(s)!");
        }
    }
}