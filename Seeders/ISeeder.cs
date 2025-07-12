namespace Codigo_examen.Seeders
{
// Interface para herencia para aplicar datos a la base de datos. Se usa template para que sea de manera general
    public interface ISeeder<T>
    {
        T ApplySeed();
    }
    // Interface para herencia para aplicar datos a la base de datos. Se usa template para que sea de manera general, se usan dos para la relacion
    public interface ISeeder<T, K>
    {
        T ApplySeed(K seed);
    }
}
