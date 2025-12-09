# Getting Started with the GeoBlazor Maui and Web Template

1. Get an API Key from the [ArcGIS Location Platform](https://location.arcgis.com/). 
2. Place this in your `appsetting.json` and `appsetting.Development.json` files, once per rendering version of the application.
   If you chose render-mode `Auto`, you should have it in the `.Web` and `.Web.Client.wwwroot` folders, as well as the `.Maui` folder.
   There should be a file with placeholders in all the expected locations.

   ```json
   {
       "ArcGISApiKey": "YourArcGISApiKey"
   }
   ```


3. Register at [licensing.dymaptic.com](https://licensing.dymaptic.com) for a free GeoBlazor Core Registration key,
   or to purchase a GeoBlazor Pro license key with additional features and support.
   Add the key to all the `appsettings.json`/`appsettings.Development.json`/`secrets.json` files:

    ```json
        {
            "ArcGISApiKey": "YourArcGISApiKey",
            "GeoBlazor": {
                // GeoBlazor Core
                "RegistrationKey": "YourGeoBlazorRegistrationKey"
                // GeoBlazor Pro
                "LicenseKey": "YourGeoBlazorProLicenseKey"
            }
        }
    ```

4. Run the MAUI or Web project. You should see interactive maps on both the `Home` and `Counter` pages.
5. To speed up local development, we have added settings in `csproj`, `App.razor`/`index.html`, and `Program.cs` that
   disable the .NET 9+ static asset compression and the new [MapStaticAssets](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/static-files?view=aspnetcore-9.0) feature.
   This allows for faster builds using the simpler, uncompressed assets like in previous versions of Blazor.
   To re-activate the compression for a production build, add the environment variable `ENABLE_COMPRESSION=true` to your
   build pipeline or local environment.

    ```bash
    dotnet restore
    dotnet build GeoBlazorWebApp.csproj --configuration Release --no-restore /p:ENABLE_COMPRESSION=true
    dotnet publish GeoBlazorWebApp.csproj --configuration Release --no-restore --no-build /p:ENABLE_COMPRESSION=true
    ```

   Alternatively, you can remove the custom code in `csproj`, `App.razor`/`index.html`, and `Program.cs`, setting
   them to match the default .NET 9+ Blazor templates.