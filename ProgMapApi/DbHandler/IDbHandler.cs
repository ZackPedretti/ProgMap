using ProgMapApi.entities;

namespace ProgMapApi.DbHandler;

public interface IDbHandler
{
    public Task<string[]> GetAllBandNames();

    public void InsertBands(Artist[] bands);

    public MinimalBandInformation[] GetAllBands();
    
    public Artist GetBand (MinimalBandInformation band); // By MinimalBandInformation
    
    public Artist GetBand (int band); // By ID
}