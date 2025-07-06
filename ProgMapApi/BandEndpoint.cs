namespace ProgMapApi;

public static class BandEndpoint
{
    public static void MapBandsEndpoints(this WebApplication app)
    {
        app.MapGet("/band-positions", () =>
            {
                var bands = new[]
                {
                    new { band = "Haken", pos = "London" }
                };
                return bands;
            })
            .WithName("GetBandPositions");
    }
}