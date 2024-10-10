using ArtistInfoTracksAPI.Data;
using ArtistInfoTracksAPI.Endpoints;
using ArtistInfoTracksAPI.Repository;
using ArtistInfoTracksAPI.Repository.Interfaces;
using ArtistInfoTracksAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<ITrackRepository, TrackRepository>();
builder.Services.AddHttpClient();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureArtistsEndpoints();
app.ConfigureTrackEndpoints();

//app.UseAuthorization();

app.MapControllers();

app.Run();
