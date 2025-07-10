using ProgMapApi.utils;

namespace ProgMapApi.BandFetcher;

public interface IBandFetcher
{
    Band[] GetAllBands();
    Band GetBand(int id);
}