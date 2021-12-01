using Microsoft.Extensions.Options;
using MusicalogWeb;
using MusicalogWeb.Interfaces.Services;
using MusicalogWeb.Services;

var builder = WebApplication.CreateBuilder(args);

Action<GlobalOptions> globals = (g => {
    g.MusicalogAPI = builder.Configuration.GetConnectionString("MusicalogAPI");
});

builder.Services.Configure(globals);

builder.Services.AddSingleton(s => s.GetRequiredService<IOptions<GlobalOptions>>().Value);
builder.Services.AddSingleton<IMusicalogAPIService, MusicalogAPIService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
