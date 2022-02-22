using Songs.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MusicContext>();

const string rootUrl = "aspdotnet/moment3.3";

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger(options =>
{
    options.RouteTemplate = $"{rootUrl}/swagger/{{documentName}}/swagger.json";
});

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint($"swagger/v1/swagger.json", "swagger endpoint");
    options.RoutePrefix = $"{rootUrl}";
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", (context) =>
{
    context.Response.Redirect($"{rootUrl}/");
    return Task.FromResult(0);
});

app.Run();