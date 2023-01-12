using Lab2_DirectOutput.Interfaces.Services;
using Lab2_DirectOutput.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lab2_DirectOutput;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // DI
        var services = new ServiceCollection()
            .AddTransient<IDialogService, DialogService>();

        using var serviceProvider = services.BuildServiceProvider();
        
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1(
            serviceProvider.GetRequiredService<IDialogService>()
            ));
    }
}