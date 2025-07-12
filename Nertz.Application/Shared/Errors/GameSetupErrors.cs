using ErrorOr;

namespace Nertz.Application.Shared.Errors;

public static class GameSetupErrors
{
    public static Error MissingConfiguration = Error.Unexpected(
        code: "GameSetupErrors.MissingConfiguration",
        description: "Required configuration settings are missing.");
    
    public static Error InsufficientPlayerCount = Error.Validation(
        code: "GameSetupErrors.InsufficientPlayerCount",
        description: "The minimum number players was not met.");

    public static Error MaximumPlayerCountExceeded = Error.Validation(
        code: "GameSetupErrors.MaximumPlayerCountExceeded",
        description: "The maximum number of players for this game has been exceeded.");

    public static Error TargetScoreInvalid = Error.Validation(
        code: "GameSetupErrors.TargetScoreInvalid",
        description: "The target score is not valid.");
}