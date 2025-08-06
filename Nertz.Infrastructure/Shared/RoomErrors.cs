using ErrorOr;

namespace Nertz.Infrastructure;

public class RoomErrors
{
    public static Error UnableToCreateRoom(Exception e)
    {
        var metadata = new Dictionary<string, object>() { { "Exception", e }  };
        return Error.Unexpected(
            code: "RoomErrors.UnableToCreateRoom",
            description: "Unable to create room.",
            metadata: metadata);
    }
    
    public static Error UnableToJoinRoom(Exception e)
    {
        var metadata = new Dictionary<string, object>() { { "Exception", e }  };
        return Error.Unexpected(
            code: "RoomErrors.UnableToJoinRoom",
            description: "Unable to join room.",
            metadata: metadata);
    }
    
    public static Error UnableToLeaveRoom(Exception e)
    {
        var metadata = new Dictionary<string, object>() { { "Exception", e }  };
        return Error.Unexpected(
            code: "RoomErrors.UnableToLeaveRoom",
            description: "Unable to leave room.",
            metadata: metadata);
    }
    public static Error UnableToDeleteRoom(Exception e)
    {
        var metadata = new Dictionary<string, object>() { { "Exception", e }  };
        return Error.Unexpected(
            code: "RoomErrors.UnableToDeleteRoom",
            description: "Unable to delete room.",
            metadata: metadata);
    }
    
    public static Error UnableToRetrieveOpenRooms(Exception e)
    {
        var metadata = new Dictionary<string, object>() { { "Exception", e }  };
        return Error.Unexpected(
            code: "RoomErrors.UnableToRetrieveOpenRooms",
            description: "Unable to retrieve all open rooms.",
            metadata: metadata);
    }
}