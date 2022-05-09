using BlazorDaprDemo.Data;
using BlazorDaprDemo.Services;
using Dapr.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Diagnostics;

Activity.DefaultIdFormat = ActivityIdFormat.W3C;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

// Tye way
builder.Services.AddHttpClient<VacationTyeAgent>(client =>
{
    client.BaseAddress = builder.Configuration.GetServiceUri("vacationapi");
});
builder.Services.AddTransient<IVacationAgent, VacationDaprAgent>();

// Dapr way
builder.Services.AddSingleton(_ =>
{
    var builder = new DaprClientBuilder();
    return builder.Build();
});
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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
