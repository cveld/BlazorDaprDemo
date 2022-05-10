using Dapr.Client;
using FavoritesAPI;
using FavoritesAPI.Const;
using FavoritesAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDaprClient();
builder.Services.AddTransient<FavoritesDaprAgent>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/favorites/{user}", async (string user, FavoritesDaprAgent agent) =>
{
    return (await agent.GetFavorites(user)).Value;
});

app.MapPost("/favorites/{user}/add/{vacationid}", async (string user, int vacationid, FavoritesDaprAgent agent) => {
    await agent.AddFavorite(user, vacationid);
    return Results.NoContent();
});
app.MapPost("/favorites/{user}/remove/{vacationid}", async (string user, int vacationid, FavoritesDaprAgent agent) =>
{
    await agent.RemoveFavorite(user, vacationid);
    return Results.NoContent();
});
app.MapPost("/favorites/{user}/clear", async (string user, FavoritesDaprAgent agent) =>
{
    await agent.ClearFavorites(user);
    return Results.NoContent();
});
app.Run();
