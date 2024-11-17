using Microsoft.Extensions.DependencyInjection;
using NetworkScannerXTech.Services;
using QuestPDF.Infrastructure;
using System.Windows;
using TablePrinterXTech.Services;
using TablePrinterXTech.ViewModels;

namespace TablePrinterXTech;

public partial class App : Application
{
    public IServiceProvider Services { get; }

    public new static App Current => (App)Application.Current;

    public App()
    {
        QuestPDF.Settings.License = LicenseType.Community;

        Services = ConfigureServices();
        InitializeComponent();
    }

    private IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddTransient<IPrintService, PrintPdfService>(); 
        services.AddTransient<IMessageService, MessageService>();

        services.AddTransient<PrintPdfViewModel>();

        return services.BuildServiceProvider();
    }
}

