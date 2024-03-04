
using Microsoft.EntityFrameworkCore;
using TMovies.Data;
using TMovies.Repository.Abstract;
using TMovies.Repository.Concrete;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


// This method gets called by the runtime. Use this method to add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); ;


builder.Services.AddTransient<IMovieRepository, MovieRepository>();
builder.Services.AddTransient<IActorsRepository, ActorsRepository>();
builder.Services.AddTransient<ITvShowRepository, TvShowRepository>();
// Add other services as needed
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute( 
    name: "default",
   pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
