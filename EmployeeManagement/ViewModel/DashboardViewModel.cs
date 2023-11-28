using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EmployeeManagement.Command;
using EmployeeManagement.Core;
using EmployeeManagement.DbService;
using EmployeeManagement.Model.Entity;
using EmployeeManagement.Model.Entity.Context;
using EmployeeManagement.View;

namespace EmployeeManagement.ViewModel;

public class DashboardViewModel : BaseViewModel
{
    private ObservableCollection<Employee>? _employees;
    private Employee? _selectedEmployee;

    public Admin User { get; }

    public ObservableCollection<Employee>? Employees
    {
        get => _employees;
        private set => SetProperty(ref _employees, value);
    }

    public Employee? SelectedEmployee
    {
        get => _selectedEmployee;
        set
        {
            SetProperty(ref _selectedEmployee, value);
            CurrentEmployee.Employee = SelectedEmployee;
            WindowManager.Open<EmployeeWorkingShiftWindow, DashboardWindow>();
        }
    }

    public ICommand SignOutCommand { get; private set; }
    public ICommand DeleteEmployeeCommand { get; private set; }
    public ICommand OpenNewEmployeeWindowCommand { get; private set; }
    public ICommand RefreshEmployeesCommand { get; private set; }

    public DashboardViewModel()
    {
        User = CurrentUser.User;

        SignOutCommand = new DelegateCommand(SignOut);
        DeleteEmployeeCommand = new DelegateCommand(DeleteEmployee);
        OpenNewEmployeeWindowCommand = new DelegateCommand(OpenNewEmployeeWindow);
        RefreshEmployeesCommand = new DelegateCommand(RefreshEmployees);

        Task.Run(async () => { await RefreshEmployeesWithSpecialization(); });
    }

    private async void RefreshEmployees(object obj)
    {
        await RefreshEmployeesWithSpecialization();
    }

    private static void OpenNewEmployeeWindow(object obj) =>
        WindowManager.Open<NewEmployeeWindow, DashboardWindow>();

    private async void DeleteEmployee(object id)
    {
        if (MessageBox.Show("Вы уверены что хотите удалить сотрудника?",
                "Удаление", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;

        await using var context = new EmployeeDbContext();
        using var employeeService = new CrudDbService<int, Employee>(context);

        await employeeService.DeleteAsync((int)id);

        Employees = new ObservableCollection<Employee>(await employeeService.GetAllAsync());
    }

    private static void SignOut(object obj)
    {
        CurrentUser.Reset();
        WindowManager.Open<LoginWindow, DashboardWindow>();
    }

    private async Task RefreshEmployeesWithSpecialization()
    {
        await using var context = new EmployeeDbContext();
        using var employeeService = new CrudDbService<int, Employee>(context);

        Employees = new ObservableCollection<Employee>(
            await employeeService.GetAllAsync("Specialization"));
    }
}
