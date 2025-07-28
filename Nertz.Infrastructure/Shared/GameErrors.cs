using ErrorOr;

namespace Nertz.Infrastructure;

public static class GameErrors
{
    public static Error UnableToCreateGame(Exception e)
    {
        var metadata = new Dictionary<string, object> { { "Exception", e } };
        return Error.Unexpected(
            code: "GameErrors.UnableToCreateGame",
            description: "Unable to create game.",
            metadata: metadata);
    }
}