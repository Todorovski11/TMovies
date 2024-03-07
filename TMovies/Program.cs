using Microsoft.AspNetCore.Identity;
using TMovies.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.ConfigureIdentity();

builder.Services.ConfigureEntityFramework(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.ConfigureTransientServices();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseCookiePolicy();

app.UseCors(builder =>
    builder.WithOrigins("http://localhost:7154")
           .AllowAnyHeader()
           .AllowAnyMethod()
    );

app.Run();