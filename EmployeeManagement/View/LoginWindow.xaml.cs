using System.Windows;
using System.Windows.Controls;
using EmployeeManagement.ViewModel;

namespace EmployeeManagement.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class LoginWindow
{
    public LoginWindow() => InitializeComponent();

    private void PasswordChanged(object sender, RoutedEventArgs e) =>
        (DataContext as LoginViewModel)!.Password = ((PasswordBox)sender).Password;
}
