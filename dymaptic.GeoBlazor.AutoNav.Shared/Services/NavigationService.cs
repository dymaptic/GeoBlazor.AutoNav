using dymaptic.GeoBlazor.Core.Components;
using dymaptic.GeoBlazor.Core.Components.Geometries;
using dymaptic.GeoBlazor.Core.Model;
using dymaptic.GeoBlazor.Pro.Components;
using dymaptic.GeoBlazor.Pro.Model;
using dymaptic.GeoBlazor.Pro.Results;

namespace AutoNav.Shared.Services;

public class NavigationService(RouteService routeService, LocationService locationService)
{
    public async Task<RouteSolveResult?> CalculateRouteAsync(Point origin, Point destination)
    {
        try
        {
            // Create route parameters
            List<Graphic> stops = [new Graphic(origin), new Graphic(destination)];
            _routeParameters.FeatureSetStops = new FeatureSet { Features = stops };

            // Solve the route
            // Note: The actual API may differ - this is based on typical ArcGIS routing patterns
            // We'll need to adjust based on the actual GeoBlazor Pro API
            RouteSolveResult result = await routeService.Solve(_routeUrl, _routeParameters);

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error calculating route: {ex.Message}");
            return null;
        }
    }

    public List<DirectionStep> GetDirections(RouteSolveResult route)
    {
        List<DirectionStep> directions = new List<DirectionStep>();

        try
        {
            // Parse route directions from result
            // This will depend on the actual structure of GeoBlazor Pro's route result
            if (route?.RouteResults is not null && route.RouteResults.Any(r => r.Directions is not null))
            {
                foreach (RouteResult result in route.RouteResults)
                {
                    foreach (Graphic feature in result.Directions!.Features!)
                {
                    directions.Add(new DirectionStep
                    {
                        Text = feature.Attributes["Text"]?.ToString() ?? string.Empty,
                        Distance = feature.Attributes["Length"] as double? ?? 0,
                        Time = feature.Attributes["Time"] as double? ?? 0,
                        ManeuverType = feature.Attributes["ManeuverType"]?.ToString() ?? string.Empty
                    });
                }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing directions: {ex.Message}");
        }

        return directions;
    }

    public async Task<List<AddressCandidate>> SearchAddressAsync(string searchText)
    {
        try
        {
            return await locationService.AddressToLocations(searchText);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error searching address: {ex.Message}");
            return [];
        }
    }

    public async Task<Point?> GeocodeAddressAsync(string address)
    {
        try
        {
            List<AddressCandidate> results = await locationService.AddressToLocations(address);
            AddressCandidate? firstResult = results.FirstOrDefault();

            if (firstResult?.Location != null)
            {
                return firstResult.Location;
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error geocoding address: {ex.Message}");
            return null;
        }
    }

    public double CalculateDistance(Point point1, Point point2)
    {
        // Haversine formula for distance between two lat/lon points
        const double R = 6371000; // Earth's radius in meters

        double lat1 = point1.Latitude!.Value * Math.PI / 180;
        double lat2 = point2.Latitude!.Value * Math.PI / 180;
        double deltaLat = (point2.Latitude.Value - point1.Latitude.Value) * Math.PI / 180;
        double deltaLon = (point2.Longitude!.Value - point1.Longitude!.Value) * Math.PI / 180;

        double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                   Math.Cos(lat1) * Math.Cos(lat2) *
                   Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return R * c;
    }

    private readonly RouteParameters _routeParameters = new()
    {
        FeatureSetStops = new FeatureSet
        {
            Features = []
        },
        OutSpatialReference = new SpatialReference(3857),
        ReturnDirections = true
    };

    private readonly string _routeUrl = "https://route-api.arcgis.com/arcgis/rest/services/World/Route/NAServer/Route_World";
}
