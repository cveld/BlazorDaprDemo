using BlazorDaprDemo.Const;
using BlazorDaprDemo.Data;
using BlazorDaprDemo.Entities;
using BlazorDaprDemo.FakeUser;
using BlazorDaprDemo.Services;
using BlazorDaprDemo.State;
using BlazorInfrastructure.CrossCircuitCommunication;
using BlazorServerSide.Data;
using Dapr.Client;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using OrderModels;
using System.Diagnostics;
using System.Text.Json;

Activity.DefaultIdFormat = ActivityIdFormat.W3C;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMatBlazor();
builder.Services.AddSingleton<ICrossCircuitCommunication, DummyCrossCircuitCommunication>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<FavoritesAgent>();
builder.Services.AddScoped<FavoritesState>();
builder.Services.AddSingleton<OrdersEventBroker>();
var auth = builder.Services.AddAuthentication(ApplicationConsts.DummyAuthScheme);
auth.AddScheme<FakeUserOptions, FakeUserAuthenticationHandler>(ApplicationConsts.DummyAuthScheme, null);
builder.Services.AddHttpContextAccessor();

// Tye way
//builder.Services.AddHttpClient<IVacationAgent, VacationTyeAgent>(client =>
//{
//    client.BaseAddress = builder.Configuration.GetServiceUri("vacationapi");
//});



// Dapr way
builder.Services.AddDaprClient();
//builder.Services.AddSingleton(_ =>
//{
//    var builder = new DaprClientBuilder();
//    return builder.Build();
//});

builder.Services.AddHttpClient(ApplicationConsts.VacationApiDaprAppId, c =>
{
    c.BaseAddress = new Uri("http://vacationapi");
}).AddHttpMessageHandler(() => new InvocationHandler());
builder.Services.AddHttpClient(ApplicationConsts.FavoritesApiDaprAppId, c =>
{
    c.BaseAddress = new Uri("http://favoritesapi");
}).AddHttpMessageHandler(() => new InvocationHandler());
builder.Services.AddTransient<IVacationAgent, VacationDaprAgent>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseCloudEvents();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.UseEndpoints(endpoints =>
{
    endpoints.MapSubscribeHandler();
    var broker = endpoints.ServiceProvider.GetRequiredService<OrdersEventBroker>();
    endpoints.MapPost("/orderprocessed", async context =>
    {
        var confirmation = await JsonSerializer.DeserializeAsync<OrderConfirmation>(context.Request.Body, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
        ArgumentNullException.ThrowIfNull(confirmation);
        broker.Complete(confirmation);
    }).WithTopic(OrderApiConsts.DaprPubSub, OrderApiConsts.OrderProcessedTopic);
});
app.Run();



