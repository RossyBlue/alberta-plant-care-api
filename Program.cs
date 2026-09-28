var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar Swagger tanto en desarrollo como en contenedor

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Alberta Plants API v1");
    c.RoutePrefix = string.Empty; // <-- Esto hace que abra directo en la raíz (http://localhost:5248)
});

app.UseAuthorization();
app.MapControllers();

app.Run();