using MetaBrainz.MusicBrainz;
using MbArtist = MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist;
using ProgMapApi.utils;

namespace ProgMapApi.BandFetcher;

public class MusicBrainzBandFetcher :  IBandFetcher
{
    public Task<Artist[]> GetAllBands()
    {
        return GetBandsByKeyword("progressive metal");
    }

    public async Task<Artist?> GetBandByName(string name)
    {
        var q = new Query();
        var results = await q.FindArtistsAsync(name);

        var artist = results.Results[0].Item;
        var beginAreaString = artist.BeginArea?.ToString();
        
        if (artist.Name == null || beginAreaString == null || artist.Country == null)
        {
            return null;
        }
        
        Console.WriteLine($"id: {artist.Id} \nName: {artist.Name}");

        return new Artist(
            artist.Id.ToString(),
            artist.Name,
            new Position(0, 0),
            artist.Country,
            beginAreaString,
            null,
            null,
            null
            );
    }

    private async Task<Artist[]> GetBandsByKeyword(string keyword)
    {
        
        List<Artist> artists = [];
        
        var q = new Query();
        var results = await q.FindArtistsAsync($"tag:\"{keyword}\"", limit: 100);

        artists.AddRange(results.Results.Select(result => result.Item)
            .Select(BuildArtistFromArtistObject)
            .OfType<Artist>());

        return artists.ToArray();
    }

    private Artist? BuildArtistFromArtistObject(MbArtist artist)
    {
        var beginAreaString = artist.BeginArea?.ToString();
        
        if (artist.Name == null || beginAreaString == null || artist.Country == null)
        {
            return null;
        }

        return new Artist(
            artist.Id.ToString(),
            artist.Name,
            new Position(0, 0),
            artist.Country,
            beginAreaString,
            null,
            null,
            null
        );
    }
}