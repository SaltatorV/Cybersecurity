using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Projekt_ASP.DTO.Validator;
using Projekt_ASP.Interfaces;
using Projekt_ASP.Repository;
using Projekt_ASP.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder.AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("http://localhost:3000", "https://appname.azurestaticapps.net");
    });

});

services.AddControllers()
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<TestUserDtoValidator>());

services.AddEndpointsApiExplorer();
services.AddAuthentication();

services.AddAuthentication(opt =>
    {
        opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidIssuer = "test",
            ClockSkew = TimeSpan.FromMinutes(0),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("w+1alOGke7bSPTgeMVlDXS5FRg3jcjRxkBtG0u3NrOo="))
        };
    });



services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IUserService, UserService>();




var provider = services.BuildServiceProvider();



var app = builder.Build();




/*// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/
app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();
app.UseCors("CORSPolicy");
app.Run();
