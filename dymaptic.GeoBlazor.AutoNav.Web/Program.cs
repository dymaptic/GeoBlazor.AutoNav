using dymaptic.GeoBlazor.AutoNav.Web.Components;
using dymaptic.GeoBlazor.AutoNav.Shared.Services;
using dymaptic.GeoBlazor.Pro;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Add device-specific services used by the dymaptic.GeoBlazor.AutoNav.Shared project
builder.Services.AddSingleton<NavigationState>();
builder.Services.AddScoped<NavigationService>();

builder.Services.AddGeoBlazorPro(builder.Configuration);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();


app.MapStaticAssets();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(dymaptic.GeoBlazor.AutoNav.Shared._Imports).Assembly,
        typeof(dymaptic.GeoBlazor.AutoNav.Web.Client._Imports).Assembly);
app.Run();
