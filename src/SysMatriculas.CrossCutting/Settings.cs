namespace SysMatriculas.CrossCutting
{
    public static class Settings
    {
        public static string ConnStr
            //=> @"Data Source=mika-note;Initial Catalog=SysMatriculas;User ID=michael;Password=giacom;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            => @"Data Source=MIKA-DESK\SQLEXPRESS;Initial Catalog=SysMatriculas;User ID=michael;Password=giacom;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    }
}
