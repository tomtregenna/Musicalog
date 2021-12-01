using Microsoft.EntityFrameworkCore;
using MusicalogAPI.Interfaces.Musicalog.Repositories;
using MusicalogAPI.Models.Musicalog;
using MusicalogAPI.Repositories.Musicalog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

builder.Services.AddScoped<IRepository<Album>, AlbumRepository>();
builder.Services.AddScoped<IRepository<Artist>, ArtistRepository>();
builder.Services.AddScoped<IReadOnlyRepository<Format>, FormatRepository>();

builder.Services.AddDbContext<MusicalogContext>(o => {
    o.UseSqlServer(builder.Configuration["MusicalogDB"]);
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new() { Title = "MusicalogAPI", Version = "v1" });
});

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
