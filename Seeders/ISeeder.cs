using Codigo_examen.Models;

namespace Codigo_examen.Seeders
{
    public interface ISeeder<T>
    {
        T ApplySeed();
    }

    public interface ISeeder<T, K>
    {
        T ApplySeed(K seed);
    }
}
