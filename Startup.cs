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

    public void ConfigureServices(IServiceCollection services)
    {
        string ConnectionString = _configuration.GetConnectionString("ApplicationConnectionString") ??
                                  throw new InvalidOperationException("No existe manera de conectarse a la base de datos");
        services.AddScoped<UserSeeder>();
        services.AddScoped<DatosExtraSeeder>();
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConnectionString));

        services.AddScoped<GetUserCase>();
        services.AddScoped<ValidateUserCase>();
        services.AddScoped<AddUserCase>();
        services.AddScoped<SelectAllUserByUserCase>();
        services.AddScoped<GetPaginatedClassUseCase>();
        services.AddScoped<DeleteUserCase>();
        services.AddScoped<UpdateUserUserCase>();
        services.AddMvc(options => options.EnableEndpointRouting = false);
    }

    public void Configure(IApplicationBuilder app, IHostEnvironment env, ApplicationDbContext context)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();
        else
            app.UseExceptionHandler("/Home/Error");

        app.UseHttpsRedirection();
        app.UseStaticFiles();
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