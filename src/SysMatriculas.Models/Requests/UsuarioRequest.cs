namespace SysMatriculas.Dominio
{
    public class UsuarioRequest
    {
        public UsuarioRequest(
            string nome, string sobreNome, string email, 
            string senha, string login, string cPF, string sexo)
        {
            Nome = nome;
            SobreNome = sobreNome;
            Email = email;
            Senha = senha;
            Login = login;
            CPF = cPF;
            Sexo = sexo;
        }

        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Login { get; set; }
        public string CPF { get; set; }
        public string TipoDeUsuario { get; set; }
        public string Sexo { get; set; }
        public string UsuarioId { get; set; }
    }


}
