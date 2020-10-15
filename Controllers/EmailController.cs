using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api_email.DataAccess.DTO;
using api_email.Utils;

namespace api_email.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailEnvio _emailEnvio;

        public EmailController(IEmailEnvio emailEnvio)
        {
            _emailEnvio = emailEnvio;
        }

        [HttpPost]
        public IActionResult EnviaEmail([FromBody] EmailObjetoDTO emailObjeto)
        {
            EmailObjetoDTO emailEnviar = new EmailObjetoDTO(); 

            emailEnviar.assunto = emailObjeto.assunto;
            emailEnviar.mensagem = emailObjeto.mensagem;
            emailEnviar.destinatario = emailObjeto.destinatario;
                    
            try
            {
                EnviarEmail(emailEnviar.destinatario, emailEnviar.assunto, emailEnviar.mensagem).GetAwaiter();
            }

            catch(Exception)
            {
                return BadRequest(new JsonResult("Erro ao envial e-mail."));
            }

            return Ok();
        }

        protected async Task EnviarEmail(string email, string assunto, string mensagem)
        {
            try
            {
                await _emailEnvio.EnviarEmailAsync(email, assunto, mensagem);
            }

            catch(Exception ex)
            {
                Console.WriteLine("ERRO NA CONTROLLER ENVIAR EMAIL => " + ex);
            }
        }
    }
}