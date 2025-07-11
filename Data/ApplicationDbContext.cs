using Codigo_examen.Models;
using Codigo_examen.Seeders;
using Microsoft.EntityFrameworkCore;

namespace Codigo_examen.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly UserSeeder _userSeeder;
        private readonly DatosExtraSeeder _datosExtraSeeder;
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<DatosExtra> DatosExtras { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, UserSeeder userSeeder, DatosExtraSeeder datosExtraSeeder)
            : base(options)
        {
            _userSeeder = userSeeder;
            _datosExtraSeeder = datosExtraSeeder;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            Usuarios[] usuariosSeed = _userSeeder.ApplySeed();

            DatosExtra[] datosExtrasSeed = _datosExtraSeeder.ApplySeed(usuariosSeed);

            builder.Entity<Usuarios>(usuarios =>
            {
                usuarios.HasKey(u => u.Id);
                usuarios.Property(u => u.Id)
                .ValueGeneratedOnAdd();
                usuarios.Property(u => u.NombreUsuario)
                .HasMaxLength(30)
                .IsRequired();
                usuarios.Property(u => u.Contrasena)
                .HasMaxLength(8)
                .IsRequired();
                usuarios.HasQueryFilter(u => !u.IsDeleted);
                usuarios.HasData(usuariosSeed);
            }
            );

            builder.Entity<DatosExtra>(datoExtra =>
            {
                datoExtra.HasKey(de => de.Id);
                datoExtra.HasOne(de => de.usuario)
                .WithOne(u => u.DatosExtra)
                .HasForeignKey<DatosExtra>(de => de.DatosExtraDelUsuario)
                .HasConstraintName("FK_Datos_extra_usuario");
                datoExtra.Property(de => de.ApellidoPaterno)
                .HasMaxLength(30)
                .IsRequired();
                datoExtra.Property(de => de.ApellidoMaterno)
                .HasMaxLength(30);
                datoExtra.Property(de => de.Calle)
                .HasMaxLength(30)
                .IsRequired();
                datoExtra.Property(de => de.NumeroExterior)
                .HasPrecision(1, 999);
                datoExtra.Property(de => de.Colonia)
                .HasMaxLength(50)
                .IsRequired();
                datoExtra.Property(de => de.CodigoPostal)
                .HasPrecision(1000, 99999)
                .IsRequired();
                datoExtra.Property(de => de.Municipio)
                .HasMaxLength(50);
                datoExtra.Property(de => de.Estado)
                .HasMaxLength(50);
                datoExtra.Property(de => de.Email)
                .IsRequired();
                datoExtra.HasQueryFilter(de => !de.usuario.IsDeleted);
                datoExtra.HasData(datosExtrasSeed);
            });

            base.OnModelCreating(builder);
        }
    }
}
