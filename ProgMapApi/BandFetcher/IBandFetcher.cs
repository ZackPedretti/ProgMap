using ProgMapApi.utils;

namespace ProgMapApi.BandFetcher;

public interface IBandFetcher
{
    Task<Artist[]> GetAllBands();
    Task<Artist?> GetBandByName (string name);
}