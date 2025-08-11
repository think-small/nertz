namespace Nertz.API.Shared.Interfaces;

public interface IShuffle
{
    void Shuffle<T>(T[] elements);
}