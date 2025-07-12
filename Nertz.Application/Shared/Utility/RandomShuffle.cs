using Nertz.Application.Nertz.Shared.Interfaces;

namespace Nertz.Application.Nertz.Shared.Utility;

public class RandomShuffle : IShuffle
{
    public void Shuffle<T>(T[] elements)
    {
        Random.Shared.Shuffle(elements);
    }
}