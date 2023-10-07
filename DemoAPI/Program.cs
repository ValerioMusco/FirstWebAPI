using DemoAPI.Tools;
using DemoASPMVC_DAL.Interface;
using DemoASPMVC_DAL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.Text;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<SqlConnection>( pc => new( builder.Configuration.GetConnectionString( "default" ) ) );
builder.Services.AddScoped<IGameService, GameDBService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<TokenManager>();

// Ajout de la sécurite par JWT
// Création des rôles
builder.Services.AddAuthorization(options => {

    options.AddPolicy( "AdminPolicy", o => o.RequireRole( "Admin" ) );
    options.AddPolicy( "ModoPolicy", o => o.RequireRole( "Admin", "Modo") );
    options.AddPolicy( "IsConnected", o => o.RequireAuthenticatedUser() );
} );

builder.Services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme ).AddJwtBearer(

    options => options.TokenValidationParameters = new() {
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidIssuer = "monserverapi.com",
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(TokenManager._secretKey)
        ),
        ValidateAudience = false
    }
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if( app.Environment.IsDevelopment() ) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
