using ProgMapApi.DbHandler;

namespace ProgMapApi.EndPoints;

public static class BandEndpoint
{
    public static void MapBandsEndpoints(this WebApplication app, IDbHandler dbHandler)
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
        
        app.MapGet("/band-names", async () => await dbHandler.GetAllBandNames())
        .WithName("GetBandNames");
    }
}