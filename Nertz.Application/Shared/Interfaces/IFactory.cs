namespace Nertz.Application.Shared.Interfaces;

public interface IFactory<T, U, V>
{
    T Create(U type, V[]? options = null);
}