using System;

namespace api_email.Utils
{
    public class EmailSettings
    {
        public String ServidorSMTP { get; set; }
        public int ServidorPorta { get; set; }
        public String EmailUsuario { get; set; }
        public String SenhaUsuario { get; set; }
        public String Destinatario { get; set; }
    }
}