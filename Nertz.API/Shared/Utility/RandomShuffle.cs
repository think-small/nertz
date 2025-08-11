using Nertz.API.Shared.Interfaces;

namespace Nertz.API.Shared.Utility;

public class RandomShuffle : IShuffle
{
    public void Shuffle<T>(T[] elements)
    {
        Random.Shared.Shuffle(elements);
    }
}