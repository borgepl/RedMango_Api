using RedMango_Api.Extensions;
using RedMango_Api.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);


// Add our own services
builder.Services.AddMyAppServices(builder.Configuration);
builder.Services.AddMyIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CorsPolicyAllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.UseStripe(builder.Configuration);

SeedDatabase();

app.MapControllers();

app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
        dbInitializer.InitializeAsync();
    }
}
