using Italbytz.Common.Abstractions;

namespace Italbytz.Music.Abstractions;

public interface ITunesSearchEngine
{
    Task<Result<List<ITrackEntity>>> GetSongs(string term);
}
