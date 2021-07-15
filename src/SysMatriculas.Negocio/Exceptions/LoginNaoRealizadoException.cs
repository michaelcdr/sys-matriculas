using System;
using System.Collections.Generic;
using System.Text;

namespace SysMatriculas.Negocio.Exceptions
{
    public class LoginNaoRealizadoException : Exception
    {
        public LoginNaoRealizadoException(string msg):base(msg)
        {

        }
    }
}
