using Italbytz.Common.Abstractions;

namespace Italbytz.Music.Abstractions;

public interface ISearchTracksService : IService<ISearchTerm, List<ICollectionEntity>>
{
}
