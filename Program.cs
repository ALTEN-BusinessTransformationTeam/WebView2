using Microsoft.Extensions.DependencyInjection;
using ReactWebView2_Template;
using ReactWebView2_Template.Models;
using Serilog;
using System.Threading;
using System.Windows;


public class Program : Application
{
    private readonly WebApplication app;
    private readonly CancellationTokenSource cancellationToken;

    public IServiceProvider ServiceProvider { get; }
    private readonly IServiceScope serviceScope;
    
    [STAThread]
    public static void Main()
    {
        new Program().Run();
    }

    public Program()
    {
        Log.Logger = new LoggerConfiguration().WriteTo.File($".\\Logs\\logfile.txt").CreateLogger();

        try
        {
            Log.Information("Starting web application");
            cancellationToken = new CancellationTokenSource();

            var builder = WebApplication.CreateBuilder();

            // Add services to the container.
            builder.Services.AddSingleton<MainWindow>(); // Creates the WPF Main Window
            builder.Services.AddControllersWithViews();
            builder.Logging.AddSerilog();

            app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html"); ;

            app.Urls.Add("https://localhost:10000");

            serviceScope = app.Services.CreateScope();
            ServiceProvider = serviceScope.ServiceProvider;

            // DataBase Initializaction
            // var db = new TemplateContext();
            // db.Intitialize();

        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }

    }

    protected override async void OnStartup(StartupEventArgs e)
    { 
        await app.StartAsync(cancellationToken.Token);

        Application.Current.Dispatcher.Invoke((Action)delegate {
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        });

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        serviceScope?.Dispose();
        await app.StopAsync(cancellationToken.Token);
        await app.DisposeAsync();
        base.OnExit(e);
    }

}
