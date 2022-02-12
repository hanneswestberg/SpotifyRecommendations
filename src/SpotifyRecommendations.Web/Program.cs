using SpotifyRecommendations.Application;
using SpotifyRecommendations.Application.Spotify.Options;
using SpotifyRecommendations.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add options
builder.Services.Configure<SpotifyOptions>(builder.Configuration.GetSection(nameof(SpotifyOptions)));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();