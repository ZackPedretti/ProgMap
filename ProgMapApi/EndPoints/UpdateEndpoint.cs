using ProgMapApi.utils;

namespace ProgMapApi.EndPoints;

public static class UpdateEndpoint
{
    public static void MapUpdateEndpoint(this WebApplication app)
    {
        app.MapGet("/update", async () =>
            {
                List<Artist> addedBands = [];
                return Results.Ok(addedBands);
            })
            .WithName("Update");
    }

    public static async Task<List<object>> UpdateDb()
    {
        var bands = new[]
        {
            new { Name = "Haken", Lat = 51.5074, Lon = -0.1278, Wiki = "https://en.wikipedia.org/wiki/Haken", LastFm = "", Discogs = "" },
            new { Name = "Dream Theater", Lat = 41.3083, Lon = -72.9279, Wiki = "https://en.wikipedia.org/wiki/Dream_Theater", LastFm = "", Discogs = "" }
        };
        
        var added = new List<object>();

        return added;
    }
}