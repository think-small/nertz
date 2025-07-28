using Nertz.Application.Shared.Interfaces;

namespace Nertz.Application.Shared.Utility;

public class RandomShuffle : IShuffle
{
    public void Shuffle<T>(T[] elements)
    {
        Random.Shared.Shuffle(elements);
    }
}