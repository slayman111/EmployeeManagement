namespace EmployeeManagement.DbService.Exception;

internal class UserNotFoundException() : System.Exception("Неверный логин или пароль");
