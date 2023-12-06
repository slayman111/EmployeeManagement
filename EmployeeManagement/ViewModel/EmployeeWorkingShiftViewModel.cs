using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EmployeeManagement.Command;
using EmployeeManagement.Core;
using EmployeeManagement.DbService;
using EmployeeManagement.Model.Entity;
using EmployeeManagement.Model.Entity.Context;
using EmployeeManagement.View;

namespace EmployeeManagement.ViewModel;

public class EmployeeWorkingShiftViewModel : BaseViewModel
{
    private ObservableCollection<WorkingShift>? _workingShifts;
    private DateTime _selectedData;
    private short _hours;
    private string _note;

    public Employee SelectedEmployee { get; }

    public ObservableCollection<WorkingShift>? WorkingShifts
    {
        get => _workingShifts;
        private set => SetProperty(ref _workingShifts, value);
    }

    public DateTime SelectedDate { get => _selectedData; set => SetProperty(ref _selectedData, value); }
    public short Hours { get => _hours; set => SetProperty(ref _hours, value); }
    public string Note { get => _note; set => SetProperty(ref _note, value); }

    public ICommand OpenDashboardWindowCommand { get; private set; }
    public ICommand DeleteWorkingShiftCommand { get; private set; }
    public ICommand AddWorkingShiftCommand { get; private set; }

    public EmployeeWorkingShiftViewModel()
    {
        SelectedEmployee = CurrentEmployee.Employee!;
        SelectedDate = DateTime.Today;

        OpenDashboardWindowCommand = new DelegateCommand(OpenDashboardWindow);
        DeleteWorkingShiftCommand = new DelegateCommand(DeleteWorkingShift);
        AddWorkingShiftCommand = new DelegateCommand(AddWorkingShift);

        Task.Run(async () => { await RefreshWorkingShifts(); });
    }

    private async void AddWorkingShift(object obj)
    {
        await using var context = new EmployeeDbContext();
        using var workingShiftService = new CrudDbService<int, WorkingShift>(context);

        await workingShiftService.CreateAsync(new WorkingShift
        {
            Date = SelectedDate,
            Hours = Hours,
            Note = Note,
            EmployeeId = SelectedEmployee.Id
        });

        await RefreshWorkingShifts();
    }

    private async void DeleteWorkingShift(object id)
    {
        await using var context = new EmployeeDbContext();
        using var workingShiftService = new CrudDbService<int, WorkingShift>(context);

        await workingShiftService.DeleteAsync((int)id);

        await RefreshWorkingShifts();
    }

    private static void OpenDashboardWindow(object obj)
    {
        CurrentEmployee.Reset();
        WindowManager.Open<DashboardWindow, EmployeeWorkingShiftWindow>();
    }

    private async Task RefreshWorkingShifts()
    {
        await using var context = new EmployeeDbContext();
        using var workingShiftService = new CrudDbService<int, WorkingShift>(context);

        WorkingShifts = new ObservableCollection<WorkingShift>(
            (await workingShiftService.GetAllAsync())
            .Where(w => w.EmployeeId == SelectedEmployee.Id));
    }
}
