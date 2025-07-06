namespace ProgMapApi;
using Npgsql;

public static class UpdateEndpoint
{
    public static void MapUpdateEndpoint(this WebApplication app)
    {
        app.MapGet("/update", async (NpgsqlDataSource dataSource) =>
            {
                var addedBands = await UpdateDb(dataSource);
                return Results.Ok(addedBands);
            })
            .WithName("Update");
    }

    public static async Task<List<object>> UpdateDb(NpgsqlDataSource dataSource)
    {
        var bands = new[]
        {
            new { Name = "Haken", Lat = 51.5074, Lon = -0.1278, Wiki = "https://en.wikipedia.org/wiki/Haken", LastFm = "", Discogs = "" },
            new { Name = "Dream Theater", Lat = 41.3083, Lon = -72.9279, Wiki = "https://en.wikipedia.org/wiki/Dream_Theater", LastFm = "", Discogs = "" }
        };
        
        var added = new List<object>();

        using var conn = await dataSource.OpenConnectionAsync();
        foreach (var band in bands)
        {
            using var cmd = new NpgsqlCommand(
                @"INSERT INTO band (name, lat, lon, wiki, lastfm, discogs)
                  VALUES (@name, @lat, @lon, @wiki, @lastfm, @discogs)
                  ON CONFLICT (name) DO NOTHING
                  RETURNING id;", conn);

            cmd.Parameters.AddWithValue("name", band.Name);
            cmd.Parameters.AddWithValue("lat", band.Lat);
            cmd.Parameters.AddWithValue("lon", band.Lon);
            cmd.Parameters.AddWithValue("wiki", band.Wiki ?? "");
            cmd.Parameters.AddWithValue("lastfm", band.LastFm ?? "");
            cmd.Parameters.AddWithValue("discogs", band.Discogs ?? "");

            var id = await cmd.ExecuteScalarAsync();
            if (id != null)
            {
                added.Add(new { id, band.Name });
            }
        }

        return added;
    }
}