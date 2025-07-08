namespace ProgMapApi;

public interface IDbHandler
{
    public string[] GetAllBandNames();

    public void InsertBands(Band[] bands);

    public MinimalBandInformation[] getAllBands();
    
    public Band getBandByMinimalInformation (MinimalBandInformation bandInformation);
}