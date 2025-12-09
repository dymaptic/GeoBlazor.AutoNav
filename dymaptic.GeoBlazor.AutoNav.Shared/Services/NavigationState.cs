using dymaptic.GeoBlazor.Core.Components.Geometries;

namespace AutoNav.Shared.Services;

public class NavigationState
{
    private Point? _userLocation;
    private Point? _origin;
    private Point? _destination;
    private Polyline? _routeGeometry;
    private List<DirectionStep> _directions = new();
    private NavigationMode _mode = NavigationMode.Idle;
    private int _currentStepIndex = 0;

    public event Action? OnStateChanged;

    public Point? UserLocation
    {
        get => _userLocation;
        set
        {
            _userLocation = value;
            NotifyStateChanged();
        }
    }

    public Point? Origin
    {
        get => _origin;
        set
        {
            _origin = value;
            NotifyStateChanged();
        }
    }

    public Point? Destination
    {
        get => _destination;
        set
        {
            _destination = value;
            NotifyStateChanged();
        }
    }

    public Polyline? RouteGeometry
    {
        get => _routeGeometry;
        set
        {
            _routeGeometry = value;
            NotifyStateChanged();
        }
    }

    public List<DirectionStep> Directions
    {
        get => _directions;
        set
        {
            _directions = value;
            _currentStepIndex = 0;
            NotifyStateChanged();
        }
    }

    public NavigationMode Mode
    {
        get => _mode;
        set
        {
            _mode = value;
            NotifyStateChanged();
        }
    }

    public int CurrentStepIndex
    {
        get => _currentStepIndex;
        set
        {
            _currentStepIndex = value;
            NotifyStateChanged();
        }
    }

    public DirectionStep? CurrentStep =>
        _directions.Count > 0 && _currentStepIndex < _directions.Count
            ? _directions[_currentStepIndex]
            : null;

    public double? TotalDistance => _directions.Sum(d => d.Distance);
    public double? TotalTime => _directions.Sum(d => d.Time);

    public void ClearRoute()
    {
        Origin = null;
        Destination = null;
        RouteGeometry = null;
        Directions = new List<DirectionStep>();
        Mode = NavigationMode.Idle;
        _currentStepIndex = 0;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}

public enum NavigationMode
{
    Idle,
    Searching,
    Routing,
    Navigating
}

public class DirectionStep
{
    public string Text { get; set; } = string.Empty;
    public double Distance { get; set; } // in meters
    public double Time { get; set; } // in seconds
    public Point? Location { get; set; }
    public string ManeuverType { get; set; } = string.Empty;
}
