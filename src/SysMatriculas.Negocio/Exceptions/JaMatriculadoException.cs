using System;

namespace SysMatriculas.Negocio.Exceptions
{
    public class JaMatriculadoException:Exception
    {
        public JaMatriculadoException() : base("Você ja está matriculado no currículo.")
        {

        }
    }
}
