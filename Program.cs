using Microsoft.AspNetCore;
public class Program
{
    public static void Main(string[] args)
    {
        //Creamos el Servidor web, incluyendo la apertura de puertos
        CreateWebHostBuilder(args).Build().Run();
    }
    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>();
}