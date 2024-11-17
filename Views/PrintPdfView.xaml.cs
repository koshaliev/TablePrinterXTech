using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TablePrinterXTech.ViewModels;

namespace TablePrinterXTech.Views;
public partial class PrintPdfView : Window
{
    public PrintPdfView()
    {
        InitializeComponent();
        var viewModel = App.Current.Services.GetRequiredService<PrintPdfViewModel>();

        DataContext = viewModel;
    }
}
