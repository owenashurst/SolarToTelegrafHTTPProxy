using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SolarToTelegrafHTTPProxy.Config;
using SolarToTelegrafHTTPProxy.CustomFormatters;
using SolarToTelegrafHTTPProxy.Features.Telegraf;
using SolarToTelegrafHTTPProxy.Services.Mqtt;
using SolarToTelegrafHTTPProxy.Services.Octopus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddControllers(o => o.InputFormatters.Insert(o.InputFormatters.Count, new TextPlainInputFormatter()));

builder.Services.AddHttpClient(TelegrafHttpService.HttpClientName, config =>
{
    var telegrafSettings = builder.Configuration.GetSection(nameof(TelegrafSettings)).Get<TelegrafSettings>();
    config.BaseAddress = new Uri(telegrafSettings.HttpListenerApiUrl);
});

builder.Services.AddHttpClient(OctopusClient.HttpClientName, config =>
{
    var octopusSettings = builder.Configuration.GetSection(nameof(OctopusSettings)).Get<OctopusSettings>();
    config.BaseAddress = new Uri(octopusSettings.BaseApiUrl);
});

builder.Services.AddMediatR(o =>
{
    o.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddOptions()
    .Configure<GeneralSettings>(builder.Configuration.GetSection(nameof(GeneralSettings)));

builder.Services.AddOptions()
    .Configure<TelegrafSettings>(builder.Configuration.GetSection(nameof(TelegrafSettings)));

builder.Services.AddOptions()
    .Configure<MqttSettings>(builder.Configuration.GetSection(nameof(MqttSettings)));

builder.Services.AddOptions()
    .Configure<OctopusSettings>(builder.Configuration.GetSection(nameof(OctopusSettings)));

builder.Services.AddSingleton<ITelegrafHttpService, TelegrafHttpService>();
builder.Services.AddSingleton<IMqttService, MqttService>();
builder.Services.AddSingleton<IOctopusClient, OctopusClient>();
builder.Services.AddSingleton<IOctopusService, OctopusService>();

var app = builder.Build();

app.UseCors(policyBuilder =>
{
    policyBuilder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();