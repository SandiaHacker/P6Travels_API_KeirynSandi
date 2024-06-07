using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using P6Travels_API_KeirynSandi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        //agregamos codigo que permite la inyección de la cadena
        //de conexión contida en appsettings.json

        //1. Obtener el valor de la cadena de conexión en appsettings
        var CnnStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("CnnStr"));

        //2.Como en la cadena de conexión eliminamos el password, lo vamos
        //a incluir directamente en este código fuente.
        CnnStrBuilder.Password = "123Queso";

        //3. Creamos un string con la informacion de la cadena de conexión.
        string cnnStr = CnnStrBuilder.ConnectionString;

        //4. Vamos asignar el valor de esta cadena de conexión al 
        //BD Contex que esta en Models
        builder.Services.AddDbContext<P620242travelsContext>(Options => Options.UseSqlServer(cnnStr));





        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}