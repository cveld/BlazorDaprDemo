using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using OrderModels;

// Nice tutorial: https://exegete.io/2021/08/12/dapr-mini-api/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDaprClient();

var app = builder.Build();
app.UseCloudEvents();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapSubscribeHandler());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("/orderplaced", async ([FromBody]Order order, DaprClient daprClient) =>
{
    app.Logger.LogInformation("Order received for vacation {VacationId}", order.VacationId);
    
    // Simulate some heavy backend processing
    await Task.Delay(5000);

    await daprClient.PublishEventAsync(OrderApiConsts.DaprPubSub, OrderApiConsts.OrderProcessedTopic, new OrderConfirmation { OrderId = order.OrderId, Confirmed = true});

    app.Logger.LogInformation("Order confirmation published for vacation {VacationId}", order.VacationId);
})
.WithTopic(OrderApiConsts.DaprPubSub, OrderApiConsts.OrderPlacedTopic);

app.Run();