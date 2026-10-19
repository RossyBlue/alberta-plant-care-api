using Microsoft.EntityFrameworkCore;
using PlantInventoryApi.Data;
using PlantInventoryApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<PlantDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IPlantRepository, PlantRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PlantDbContext>();
    await db.Database.MigrateAsync();
    await DbSeeder.SeedAsync(db);
}

app.UseExceptionHandler();
app.UseStatusCodePages();

// Swagger tanto en desarrollo como en contenedor (queda en /swagger)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Alberta Plants API v1");
});

app.UseDefaultFiles();   // sirve index.html en la raíz
app.UseStaticFiles();    // sirve lo que esté en wwwroot

app.UseAuthorization();
app.MapControllers();

app.Run();