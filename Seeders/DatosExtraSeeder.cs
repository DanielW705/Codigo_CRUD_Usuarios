using Codigo_examen.Models;

namespace Codigo_examen.Seeders
{
    public class DatosExtraSeeder : ISeeder<DatosExtra[], Usuarios[]>
    {
        public DatosExtra[] ApplySeed(Usuarios[] seed)
        {
            DatosExtra[] output = new DatosExtra[seed.Length];
            foreach (var (value, i) in seed.Select((value, i) => (value, i)))
                output[i] = new DatosExtra
                {
                    Id = i + 1,
                    ApellidoPaterno = "Example",
                    ApellidoMaterno = "Example",
                    Calle = "Example",
                    NumeroExterior = i + 1,
                    Colonia = "Example",
                    CodigoPostal = 1000 + i,
                    Municipio = "Example",
                    Estado = "Example",
                    Email = $"Example@example{i + 1}.com",
                    DatosExtraDelUsuario = value.Id
                };
            return output;
        }
    }
}
