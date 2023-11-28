using System.Reflection;
using System.Windows;

namespace EmployeeManagement.View;

public class BaseWindow : Window
{
    protected BaseWindow()
    {
        Title = Assembly.GetEntryAssembly()?.GetName().Name;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }
}
