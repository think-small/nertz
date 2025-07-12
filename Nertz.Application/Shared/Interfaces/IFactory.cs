namespace Nertz.Application.Nertz.Shared.Interfaces;

public interface IFactory<T, U, V>
{
    T Create(U type, V[]? options = null);
}