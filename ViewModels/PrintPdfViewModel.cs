using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetworkScannerXTech.Services;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using TablePrinterXTech.Models;
using TablePrinterXTech.Services;

namespace TablePrinterXTech.ViewModels;
public partial class PrintPdfViewModel : ObservableObject
{
    private readonly IPrintService _printService;
    private readonly IMessageService _messageService;

    [ObservableProperty]
    private ObservableCollection<DocumentDataRow> userData;
    [ObservableProperty]
    private string title = string.Empty;

    public RelayCommand PrintCommand { get; }
    
    public PrintPdfViewModel(IPrintService printService, IMessageService messageService)
    {
        UserData = new();
        _printService = printService;
        _messageService = messageService;
        PrintCommand = new RelayCommand(PrintToPdf);
    }

    private void PrintToPdf()
    {
        if (string.IsNullOrEmpty(Title))
        {
            Title = "DefaultDocumentTitle";
        }
        string invalidCharsPattern = @"[""?:*/\|<>]";
        string[] reservedWords = { "CON", "PRN", "AUX", "NUL", 
                                        "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", 
                                        "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };

        string result = Regex.Replace(Title, invalidCharsPattern, "_");
        
        foreach (string word in reservedWords)
        {
            if (result.Equals(word, StringComparison.OrdinalIgnoreCase))
            {
                result = word + "_";
                break;
            }
        }
        string filename = Directory.GetCurrentDirectory() + "\\" + result + ".pdf";
        var document = new Document() { Data = UserData, Title = Title, FileName = filename };
        _printService.Print(document);
        _messageService.ShowMessage($"Путь до файла: {filename}", "Файл сохранен");
    }
}
