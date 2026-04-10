namespace Italbytz.Music.Abstractions;

public interface ICollectionEntity
{
    string Name { get; set; }

    List<ITrackEntity> Tracks { get; set; }
}
