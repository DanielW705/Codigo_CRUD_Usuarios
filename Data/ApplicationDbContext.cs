using Codigo_examen.Models;
using Codigo_examen.Seeders;
using Microsoft.EntityFrameworkCore;

namespace Codigo_examen.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly UserSeeder _userSeeder;
        private readonly DatosExtraSeeder _datosExtraSeeder;
        //Declaracion de las tablas
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<DatosExtra> DatosExtras { get; set; }
        //constructor para ser inyectado con los servicios

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, UserSeeder userSeeder, DatosExtraSeeder datosExtraSeeder)
            : base(options)
        {
            _userSeeder = userSeeder;
            _datosExtraSeeder = datosExtraSeeder;
        }
        //Sobre escritura del metodo para la configuracion de la base de datos
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Inicializado de los usuarios
            Usuarios[] usuariosSeed = _userSeeder.ApplySeed();

            //Inicializado de los datos extra
            DatosExtra[] datosExtrasSeed = _datosExtraSeeder.ApplySeed(usuariosSeed);

            //De la entidad usuario se aplicacran las reglas
            builder.Entity<Usuarios>(usuarios =>
            {
                //Tiene una llave primaria y se generara al crear una entidad
                usuarios.HasKey(u => u.Id);
                usuarios.Property(u => u.Id)
                .ValueGeneratedOnAdd();
                //El nombre es requerido y no debe superar los 30 caracteres
                usuarios.Property(u => u.NombreUsuario)
                .HasMaxLength(30)
                .IsRequired();
                //La contraseña es requerido y no debe superar a los 10 caracteres
                usuarios.Property(u => u.Contrasena)
                .HasMaxLength(10)
                .IsRequired();
                //Cada consulta se hara un filtrado donde se muestre los que han sido borrados
                usuarios.HasQueryFilter(u => !u.IsDeleted);
                //Se agregaran los datos a la base de datos
                usuarios.HasData(usuariosSeed);
            }
            );
            //De la entidad datoExtra se aplicaran las reglas
            builder.Entity<DatosExtra>(datoExtra =>
            {
                //Tiene una llave primaria llave primaria
                datoExtra.HasKey(de => de.Id);
                //Se enlaza, se dice que se tiene un usuario, con un dato extra. Donde la llave es DatosExtra y tiene nombre la relacion
                datoExtra.HasOne(de => de.usuario)
                .WithOne(u => u.DatosExtra)
                .HasForeignKey<DatosExtra>(de => de.DatosExtraDelUsuario)
                .HasConstraintName("FK_Datos_extra_usuario");
                //El apellido paterno es requerido y no debe superar a los 30 caracteres
                datoExtra.Property(de => de.ApellidoPaterno)
                .HasMaxLength(30)
                .IsRequired();
                //El apellido materno es opcional y no debe superar a los 30 caracteres
                datoExtra.Property(de => de.ApellidoMaterno)
                .HasMaxLength(30);
                //La calle es requerido y no debe superar a los 30 caracteres
                datoExtra.Property(de => de.Calle)
                .HasMaxLength(30)
                .IsRequired();
                //El numero exterior es opcional y va de 1 a 999
                datoExtra.Property(de => de.NumeroExterior)
                .HasPrecision(1, 999);
                //La colonia es requerido y no debe superar a los 50 caracteres
                datoExtra.Property(de => de.Colonia)
                .HasMaxLength(50)
                .IsRequired();
                datoExtra.Property(de => de.CodigoPostal)
                .HasPrecision(1000, 99999)
                .IsRequired();
                //El municipio paterno es opcional y no debe superar a los 50 caracteres
                datoExtra.Property(de => de.Municipio)
                .HasMaxLength(50);
                //El estado es opcional y no debe superar a los 50 caracteres
                datoExtra.Property(de => de.Estado)
                .HasMaxLength(50);
                //El correo es requerido
                datoExtra.Property(de => de.Email)
                .IsRequired();
                //Cada consulta se hara un filtrado donde se muestre los que han sido borrados
                datoExtra.HasQueryFilter(de => !de.usuario.IsDeleted);
                //Se agregaran los datos a la base de datos
                datoExtra.HasData(datosExtrasSeed);
            });

            base.OnModelCreating(builder);
        }
    }
}
