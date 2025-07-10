using ProgMapApi.utils;

namespace ProgMapApi.DbHandler;

public interface IDbHandler
{
    public Task<string[]> GetAllBandNames();

    public void InsertBands(Band[] bands);

    public MinimalBandInformation[] GetAllBands();
    
    public Band GetBand (MinimalBandInformation band); // By MinimalBandInformation
    
    public Band GetBand (int band); // By ID
}