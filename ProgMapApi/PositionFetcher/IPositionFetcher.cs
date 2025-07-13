using ProgMapApi.utils;

namespace ProgMapApi.PositionFetcher;

public interface IPositionFetcher
{
    Task<Position> GetPositionFromPlaceName(string placeName);
}