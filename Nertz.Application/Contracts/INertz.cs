using ErrorOr;
using Microsoft.Extensions.Options;
using Nertz.Application.Nertz;

namespace Nertz.Application.Contracts;

public interface INertz
{
    ErrorOr<Game> SetupGame();
}