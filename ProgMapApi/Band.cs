namespace ProgMapApi;

public class Band(int id, string name, float lat, float lon, string wiki, string lastfm, string discogs)
{
    public readonly int Id = id;
    public readonly string Name = name;
    public readonly float Lat = lat;
    public readonly float Lon = lon;
    public readonly string Wiki = wiki;
    public readonly string Lastfm = lastfm;
    public readonly string Discogs = discogs;
}