using CD.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CdLibraryContext>();

var app = builder.Build();

const string rootUrl = "/aspdotnet/moment32";

app.UseRouting();
app.UseStaticFiles(rootUrl);

app.MapControllerRoute(
    "artist",
    "{controller=Artist}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    "cd",
    "{controller=Cd}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    "user",
    "{controller=User}/{action=Index}/{id?}"
);


app.Run();