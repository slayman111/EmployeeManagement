using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EmployeeManagement.Command;
using EmployeeManagement.Core;
using EmployeeManagement.DbService;
using EmployeeManagement.Exception;
using EmployeeManagement.Model.Entity;
using EmployeeManagement.Model.Entity.Context;
using EmployeeManagement.View;

namespace EmployeeManagement.ViewModel;

public class NewEmployeeViewModel : BaseViewModel
{
    private string? _fullName;
    private ObservableCollection<Specialization>? _specializations;
    private Specialization? _selectedSpecialization;
    private string? _insuranceNumber;
    private string? _medBookNumber;
    private string? _employmentBookNumber;
    private decimal? _salary;

    public string? FullName { get => _fullName; set => SetProperty(ref _fullName, value); }

    public ObservableCollection<Specialization>? Specializations
    {
        get => _specializations;
        set => SetProperty(ref _specializations, value);
    }

    public Specialization? SelectedSpecialization
    {
        get => _selectedSpecialization;
        set => SetProperty(ref _selectedSpecialization, value);
    }

    public string? InsuranceNumber { get => _insuranceNumber; set => SetProperty(ref _insuranceNumber, value); }

    public string? MedBookNumber { get => _medBookNumber; set => SetProperty(ref _medBookNumber, value); }

    public string? EmploymentBookNumber
    {
        get => _employmentBookNumber;
        set => SetProperty(ref _employmentBookNumber, value);
    }

    public decimal? Salary { get => _salary; set => SetProperty(ref _salary, value); }

    public ICommand OpenDashboardWindowCommand { get; private set; }
    public ICommand AddEmployeeCommand { get; private set; }

    public NewEmployeeViewModel()
    {
        OpenDashboardWindowCommand = new DelegateCommand(OpenDashboardWindow);
        AddEmployeeCommand = new DelegateCommand(AddEmployee);

        Task.Run(async () =>
        {
            await using var context = new EmployeeDbContext();
            using var specializationService = new CrudDbService<int, Specialization>(context);

            Specializations = new ObservableCollection<Specialization>(await specializationService.GetAllAsync());
        });
    }

    private async void AddEmployee(object obj)
    {
        await using var context = new EmployeeDbContext();
        using var employeeService = new CrudDbService<int, Employee>(context);

        var names = FullName is not null ? FullName.Split(' ') : throw new EmptyFullNameException();
        await employeeService.CreateAsync(new Employee
        {
            FirstName = names[0],
            LastName = names[1],
            Patronymic = names.Length == 3 ? names[2] : null,
            SpecializationId = SelectedSpecialization?.Id ?? throw new EmptySpecializationException(),
            InsuranceNumber = InsuranceNumber,
            MedBookNumber = MedBookNumber,
            EmploymentBookNumber = EmploymentBookNumber,
            Salary = Salary ?? throw new EmptySalaryException(),
            CreatedById = CurrentUser.User.Id
        });

        MessageBoxService.Info("Сотрудник добавлен");
        ClearFields();
    }

    private void ClearFields()
    {
        FullName = null;
        SelectedSpecialization = null;
        InsuranceNumber = null;
        MedBookNumber = null;
        EmploymentBookNumber = null;
        EmploymentBookNumber = null;
        Salary = null;
    }

    private static void OpenDashboardWindow(object obj) => WindowManager.Open<DashboardWindow, NewEmployeeWindow>();
}
