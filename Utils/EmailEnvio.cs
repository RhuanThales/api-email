using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace api_email.Utils
{
    public class EmailEnvio: IEmailEnvio
    {
        public EmailSettings _emailSettings {get;}
        public EmailEnvio(IOptions<EmailSettings> EmailSettings)
        {
                _emailSettings = EmailSettings.Value;
        }

        public Task EnviarEmailAsync(string email, string assunto, string mensagem)
        {
            try
            {
                Enviar(email, assunto, mensagem).Wait();
                return Task.FromResult(0);
            }

            catch(Exception)
            {
                throw;
            }
        }

        public async Task Enviar(string email, string assunto, string mensagem)
        {
            try
            {
                string destinatario = email;

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.EmailUsuario, "Prefeitura Municipal de Patos de Minas")
                };

                mail.To.Add(new MailAddress(destinatario));
                mail.Body = mensagem;
                mail.Subject = assunto;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.ServidorSMTP, _emailSettings.ServidorPorta))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.EmailUsuario, _emailSettings.SenhaUsuario);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERRO NA CLASSE EMAILENVIO => "+ ex);
            }
        }
    }
}