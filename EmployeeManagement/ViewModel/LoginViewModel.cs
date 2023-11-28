using System.Windows.Input;
using EmployeeManagement.Command;
using EmployeeManagement.Core;
using EmployeeManagement.DbService;
using EmployeeManagement.View;

namespace EmployeeManagement.ViewModel;

public class LoginViewModel : BaseViewModel
{
    private string? _password;
    private string? _login;

    public string? Password { get => _password; set => SetProperty(ref _password, value); }
    public string? Login { get => _login; set => SetProperty(ref _login, value); }

    public ICommand SignInCommand { get; private set; }

    public LoginViewModel()
    {
        SignInCommand = new DelegateCommand(SignIn);
    }

    private async void SignIn(object obj)
    {
        var admin = await AuthorizationService.LoginAsync(Login, Password);

        CurrentUser.User = admin;
        WindowManager.Open<DashboardWindow, LoginWindow>();
    }
}
