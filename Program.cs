using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewAcupuntura;
using NewAcupuntura.Commands;
using NewAcupuntura.Entities;
using NewAcupuntura.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AcupunturaDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AcupunturaDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IAuthenticate, AuthenticateService>();

builder.Services.AddControllers();
builder.Services.AddScoped<PopularHorario>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();