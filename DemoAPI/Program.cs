using DemoASPMVC_DAL.Interface;
using DemoASPMVC_DAL.Services;
using System.Data;
using System.Data.SqlClient;

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


var app = builder.Build();

// Configure the HTTP request pipeline.
if( app.Environment.IsDevelopment() ) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
