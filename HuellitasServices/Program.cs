using Microsoft.EntityFrameworkCore;
using Huellitas.Services.Administration;

var builder = WebApplication.CreateBuilder(args);

////production
var connectionString = Environment.GetEnvironmentVariable("HuellitasDB");

////local
//var connectionString = Environment.GetEnvironmentVariable("HuellitasDB", EnvironmentVariableTarget.Machine);

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
builder.Services.AddControllers();
builder.Services.AddDbContext<AdministrationContext>(options => options.UseSqlServer(connectionString)).AddTransient<AdministrationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}
app.UseSwagger();
    app.UseSwaggerUI();
app.UseHttpsRedirection();

// Usar CORS aquí antes de Authorization
app.UseCors("AllowAll");


app.UseAuthorization();

app.MapControllers();

app.Run();