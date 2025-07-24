using Npgsql;
using ProgMapApi.entities;

namespace ProgMapApi.DbHandler;

public class PgDbHandler(string connStr) : IDbHandler
{
    public async Task<string[]> GetAllBandNames()
    {
        var query = "SELECT name FROM BAND ORDER BY name;";
        try
        {
            await using var datasource = new NpgsqlConnection(connStr);
            
            await using var cmd = new NpgsqlCommand(query, datasource);
            
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                return
                [
                    reader.GetString(0)
                ];
            }
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        return [];
    }

    public void InsertBands(Artist[] bands)
    {
        throw new NotImplementedException();
    }

    public MinimalBandInformation[] GetAllBands()
    {
        throw new NotImplementedException();
    }

    public Artist GetBand(MinimalBandInformation band)
    {
        throw new NotImplementedException();
    }

    public Artist GetBand(int band)
    {
        throw new NotImplementedException();
    }
}