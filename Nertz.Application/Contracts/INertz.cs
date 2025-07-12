using ErrorOr;
using Nertz.Application.Nertz;

namespace Nertz.Application.Contracts;

public interface INertz
{
    ErrorOr<Game> SetupGame(int targetScore, int maxPlayerCount, IEnumerable<Guid> playerIds);
}