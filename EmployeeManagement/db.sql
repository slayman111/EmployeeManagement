create table Admin
(
    Id       int identity
        constraint Admin_pk
            primary key,
    Login    nvarchar(50) not null,
    Password nvarchar(50) not null
)
go

create table Specialization
(
    Id   int identity
        constraint Specialization_pk
            primary key,
    Name nvarchar(50) not null
)
go

create table Employee
(
    Id                   int identity
        constraint Employee_pk
            primary key,
    FirstName            nvarchar(50) not null,
    LastName             nvarchar(50) not null,
    Patronymic           nvarchar(50),
    SpecializationId     int          not null
        constraint Employee_Specialization_Id_fk
            references Specialization,
    InsuranceNumber      nvarchar(50),
    MedBookNumber        nvarchar(50),
    EmploymentBookNumber nvarchar(50),
    Salary               money        not null,
    CreatedById          int          not null
        constraint Employee_Admin_Id_fk
            references Admin
)
go

create index IX_Employee_CreatedById
    on Employee (CreatedById)
go

create index IX_Employee_SpecializationId
    on Employee (SpecializationId)
go

create table WorkingShift
(
    Id         int identity
        constraint WorkingShift_pk
            primary key,
    Date       datetime not null,
    Hours      smallint not null,
    Note       nvarchar(50),
    EmployeeId int      not null
        constraint WorkingShift_Employee_Id_fk
            references Employee
            on delete cascade
)
go

create index IX_WorkingShift_EmployeeId
    on WorkingShift (EmployeeId)
go
