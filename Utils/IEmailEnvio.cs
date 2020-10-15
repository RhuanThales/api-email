using System.Threading.Tasks;

namespace api_email.Utils
{
    public interface IEmailEnvio
    {
        Task EnviarEmailAsync(string email, string assunto, string mensagem);
    }
}