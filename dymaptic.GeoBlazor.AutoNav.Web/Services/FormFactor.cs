using dymaptic.GeoBlazor.AutoNav.Shared.Services;

namespace dymaptic.GeoBlazor.AutoNav.Web.Services;

public class FormFactor : IFormFactor
{
    public string GetFormFactor()
    {
        return "Web";
    }

    public string GetPlatform()
    {
        return Environment.OSVersion.ToString();
    }
}
