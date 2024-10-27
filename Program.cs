using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewAcupuntura;
using NewAcupuntura.Commands;
using NewAcupuntura.Entities;
using NewAcupuntura.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configura o banco de dados com SQLite
builder.Services.AddDbContext<AcupunturaDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura Identity para autenticação e controle de usuários
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AcupunturaDbContext>()
    .AddDefaultTokenProviders();

var keyValue = builder.Configuration["Jwt:Key"];

if (keyValue == null)
{
    throw new InvalidOperationException("JWT Key is not configured.");
}
var key = Encoding.ASCII.GetBytes(keyValue);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Adiciona serviços de autenticação personalizados
builder.Services.AddScoped<IAuthenticate, AuthenticateService>();

// Adiciona controladores para o MVC
builder.Services.AddControllers();

// Serviços adicionais
builder.Services.AddScoped<PopularHorario>();

// Configura o Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5500") // Origem permitida (frontend)
                   .AllowAnyHeader() // Permite qualquer cabeçalho
                   .AllowAnyMethod(); // Permite qualquer método (GET, POST, etc.)
        });
});

var app = builder.Build();

// Se o ambiente for de desenvolvimento, ativa o Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ativa o redirecionamento de HTTPS
app.UseHttpsRedirection();


app.UseCors("AllowSpecificOrigin");
// Ativa a autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

// Aplica a política de CORS antes de mapear os controladores


// Mapeia os controladores
app.MapControllers();

// Executa o aplicativo
app.Run();
