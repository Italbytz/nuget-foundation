namespace Italbytz.Music.Abstractions;

public interface ITrackEntity : IComparable<ITrackEntity>
{
    string ArtistName { get; set; }

    string CollectionName { get; set; }

    string TrackName { get; set; }

    int TrackNumber { get; set; }

    int DiscNumber { get; set; }

    Uri ArtworkUrl { get; set; }
}
