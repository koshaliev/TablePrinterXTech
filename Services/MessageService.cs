using System.Windows;

namespace NetworkScannerXTech.Services;
public class MessageService : IMessageService
{
    public void ShowMessage(string message, string title)
    {
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
    }
}
