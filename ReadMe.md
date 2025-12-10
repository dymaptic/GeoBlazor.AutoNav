# AutoNav - GeoBlazor Vehicle Navigation Demo

A turn-by-turn vehicle navigation application built with [GeoBlazor Pro](https://geoblazor.com), demonstrating real-time routing and navigation capabilities using ArcGIS services.

## Features

- **Turn-by-turn navigation** with voice-ready direction maneuvers
- **Live GPS tracking** with automatic route recalculation
- **Address search** with autocomplete suggestions
- **Cross-platform support** - runs on Web (Blazor Server/WebAssembly) and Mobile (MAUI)

## Architecture

| Project | Description |
|---------|-------------|
| `AutoNav.Shared` | Shared Blazor components, pages, and services |
| `AutoNav.Web` | ASP.NET Core Blazor Server host |
| `AutoNav.Web.Client` | Blazor WebAssembly client |
| `AutoNav.Maui` | .NET MAUI Blazor Hybrid app (Android, iOS, Windows, Mac) |

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [ArcGIS API Key](https://location.arcgis.com/)
- [GeoBlazor Pro License](https://licensing.dymaptic.com)

### Configuration

Add your API keys to `appsettings.json`, `appsettings.Development.json`, or User Secrets in `AutoNav.Web/`, `AutoNav.Web.Client/wwwroot/`, and `AutoNav.Maui/`.
The `appsettings.Development.json` files have been added to `.gitignore` to allow for use in MAUI and WASM as safe local secrets files.

```json
{
    "ArcGISApiKey": "your-arcgis-api-key",
    "GeoBlazor": {
        "LicenseKey": "your-geoblazor-pro-license-key"
    }
}
```

### Build and Run

```bash
# Run web application
dotnet run --project AutoNav.Web/AutoNav.Web.csproj -lp https

# Run MAUI app on Windows
dotnet run --project AutoNav.Maui/AutoNav.Maui.csproj -f net10.0-windows10.0.19041.0

# Run MAUI app on Android
dotnet run --project AutoNav.Maui/AutoNav.Maui.csproj -f net10.0-android

# Run MAUI app on Mac
dotnet run --project AutoNav.Maui/AutoNav.Maui.csproj -f net10.0-maccatalyst

# Run MAUI app on iOS
dotnet run --project AutoNav.Maui/AutoNav.Maui.csproj -f net10.0-ios
```

## Technology Stack

- .NET 10
- GeoBlazor Pro 4.3.0
- ArcGIS World Route Service
- Blazor Server + WebAssembly (Interactive Auto rendering)
- .NET MAUI Blazor Hybrid
