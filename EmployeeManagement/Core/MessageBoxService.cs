using System.Windows;

namespace EmployeeManagement.Core;

public static class MessageBoxService
{
    public static void Info(string text) =>
        MessageBox.Show(text, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
}
