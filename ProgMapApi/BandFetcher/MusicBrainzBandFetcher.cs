using MetaBrainz.MusicBrainz;
using MbArtist = MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist;
using MetaBrainz.MusicBrainz.Interfaces.Searches;
using ProgMapApi.entities;

namespace ProgMapApi.BandFetcher;

public class MusicBrainzBandFetcher : IBandFetcher
{
    public const string SOURCE = "MUSICBRAINZ";

    public async Task<Artist[]> GetAllBands()
    {
        return await GetBandsByKeywords([
            "prog",
            "progressive metal",
            "progressive rock",
            "progressive jazz",
            "progressive"
        ]);
    }

    public async Task<Artist?> GetBandByName(string name)
    {
        var q = new Query();
        var results = await q.FindArtistsAsync(name);

        var artist = results.Results[0].Item;
        return BuildArtistFromArtistObject(artist);
    }

    private async Task<Artist[]> GetBandsByKeyword(string keyword)
    {
        HashSet<Artist> artists = [];

        var results = await GetAllSearchResults($"tag:\"{keyword}\"");

        foreach (var artist in results.Select(result => result.Item)
                     .Select(BuildArtistFromArtistObject)
                     .OfType<Artist>())
        {
            artists.Add(artist);
        }

        return artists.ToArray();
    }

    private Artist? BuildArtistFromArtistObject(MbArtist artist)
    {
        var beginAreaString = artist.BeginArea?.ToString();

        if (artist.Name == null || beginAreaString == null || artist.Country == null || artist.LifeSpan == null ||
            artist.LifeSpan.Begin == null)
        {
            return null;
        }

        List<Link> links = [];
        if (artist.Relationships != null)
        {
            foreach (var rel in artist.Relationships)
            {
                if (rel.TargetType != EntityType.Url) continue;
                Console.WriteLine($"Type: {rel.Type}, URL: {rel.Url}, UrlToString: {rel.Url.ToString()}");
                if (rel is not { Type: not null, Url: not null }) continue;
                var urlString = rel.Url.ToString();
                if (urlString is null) continue;

                var link = new Link(rel.Type, urlString);
                links.Add(link);
            }
        }

        return new Artist(
            artist.Id.ToString(),
            artist.Name,
            "(No description provided)",
            new Position(0, 0),
            artist.Country,
            beginAreaString,
            (int)((DateTimeOffset)artist.LifeSpan.Begin.NearestDate).ToUnixTimeSeconds(),
            links.ToArray(),
            SOURCE
        );
    }

    private async Task<ISearchResult<MbArtist>[]> GetAllSearchResults(string query)
    {
        List<ISearchResult<MbArtist>> allResults = [];

        var q = new Query();
        const int limit = 100;
        IReadOnlyList<ISearchResult<MbArtist>> pageResults;

        do
        {
            var results = await q.FindArtistsAsync(query, limit: limit, offset: allResults.Count);
            pageResults = results.Results;
            allResults.AddRange(pageResults);
        } while (pageResults.Count == limit);

        return allResults.ToArray();
    }

    private async Task<Artist[]> GetBandsByKeywords(string[] keywords)
    {
        HashSet<Artist> artists = [];
        foreach (var keyword in keywords)
        {
            var results = await GetBandsByKeyword(keyword);
            foreach (var result in results)
            {
                artists.Add(result);
            }
        }
        return artists.ToArray();
    }
}