using CD.Data;
using Microsoft.AspNetCore.Mvc;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CdLibraryContext>();

var app = builder.Build();

const string rootUrl = "/aspdotnet/moment3.2";

app.UseRouting();
app.UseStaticFiles(rootUrl);

app.MapGet("/", (context) =>
{
    context.Response.Redirect(rootUrl);
    return Task.FromResult(0);
});

app.MapControllerRoute(
    "cd",
    $"{rootUrl}/{{controller=Cd}}/{{action=Index}}/{{id?}}"
);

app.MapControllerRoute(
    "artist",
    $"{rootUrl}/{{controller=Artist}}/{{action=Index}}/{{id?}}"
);

app.MapControllerRoute(
    "user",
    $"{rootUrl}/{{controller=User}}/{{action=Index}}/{{id?}}"
);


app.Run();