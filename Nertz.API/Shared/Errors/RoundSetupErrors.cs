using ErrorOr;

namespace Nertz.API.Shared.Errors;

public static class RoundSetupErrors
{
    public static Error UnableToInitializeCommonPile = Error.Unexpected(
        code: "RoundSetupErrors.UnableToInitializeCommonPile",
        description: "Unable to initialize common pile.");
}