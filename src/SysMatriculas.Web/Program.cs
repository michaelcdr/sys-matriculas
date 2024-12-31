using Microsoft.AspNetCore.Builder;
using SysMatriculas.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder
    .ConfigurarMvc()
    .ConfigurarIdentity()
    .ConfigurarServicos();
 
var app = builder
    .Build()
    .ConfigurarWebApp();