using Codigo_examen.Data;
using Codigo_examen.Models;
using Codigo_examen.Seeders;
using Codigo_examen.UseCase;
using Microsoft.EntityFrameworkCore;

public class Startup
{
    public readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    //Configuracion de los servicios que hay en la aplicacion
    public void ConfigureServices(IServiceCollection services)
    {
        //Obtencion de la cadena de conexion
        string ConnectionString = _configuration.GetConnectionString("ApplicationConnectionString") ??
                                  throw new InvalidOperationException("No existe manera de conectarse a la base de datos");
        //Inyeccion de dependencias para los seeders
        services.AddScoped<UserSeeder>();
        services.AddScoped<DatosExtraSeeder>();
        //Configuracion de la base de datos
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConnectionString));
        //Inyeccion de los servicios que hay en el proyecto
        services.AddScoped<GetUserCase>();
        services.AddScoped<ValidateUserCase>();
        services.AddScoped<AddUserCase>();
        services.AddScoped<SelectAllUserByUserCase>();
        services.AddScoped<GetPaginatedClassUseCase>();
        services.AddScoped<DeleteUserCase>();
        services.AddScoped<UpdateUserUserCase>();
        //Configuracion de MVC, eliminando los enrutamientos por endpoint
        services.AddMvc(options => options.EnableEndpointRouting = false);
    }
    //Metodo para configuracion
    public void Configure(IApplicationBuilder app, IHostEnvironment env, ApplicationDbContext context)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();
        else
            app.UseExceptionHandler("/Home/Error");
        //Habilitamos el redireccionamiento
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        //Habilitamos el enrutamiento
        app.UseRouting();
        app.UseAuthorization();
        app.UseMvc(routes =>
        {
            routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}");
        });
    }
}