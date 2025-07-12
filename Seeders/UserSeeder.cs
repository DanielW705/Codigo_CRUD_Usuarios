using Codigo_examen.Models;

namespace Codigo_examen.Seeders
{
    public class UserSeeder : ISeeder<Usuarios[]>
    {
        //Se hace el vaciado de los datos
        public Usuarios[] ApplySeed()
        {
            return new Usuarios[] {
                new Usuarios {Id = Guid.Parse("e8332afa-665a-4692-a08d-623f06c7cfe3"), NombreUsuario = "Daniel", Contrasena="12345678"},
                new Usuarios {Id = Guid.Parse("206c93fb-1500-40a0-9396-c2df231240b7"), NombreUsuario = "Juanito", Contrasena="01234567"},
                new Usuarios {Id = Guid.Parse("e6e0f6fc-0000-4e24-adec-8de808a9d75e"), NombreUsuario = "Pepito", Contrasena="23456789"}

            };
        }
    }
}
