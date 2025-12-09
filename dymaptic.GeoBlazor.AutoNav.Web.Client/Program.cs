using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AutoNav.Shared.Services;
using AutoNav.Web.Client.Services;
using dymaptic.GeoBlazor.Pro;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the AutoNav.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddSingleton<NavigationState>();
builder.Services.AddScoped<NavigationService>();

builder.Services.AddGeoBlazorPro(builder.Configuration);

await builder.Build().RunAsync();
