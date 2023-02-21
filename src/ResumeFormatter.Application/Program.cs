using ResumeFormatter.Infra.CrossCutting.IOC;

var builder = WebApplication.CreateBuilder(args);

DependenciesContainer.RegisterServices(builder.Services);
DependenciesContainer.RegisterRepositories(builder.Services);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// dotnet user-jwts
builder.Services.AddAuthentication().AddJwtBearer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
