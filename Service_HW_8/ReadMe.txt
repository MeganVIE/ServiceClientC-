1. Crate Database named HWdb

2. Create new table Employees:
CREATE TABLE [dbo].[EmployeesTable]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [Age] INT NOT NULL, 
    [Salary] INT NOT NULL, 
    [Position] VARCHAR(50) NOT NULL, 
    [DepartmentId] INT NOT NULL,
	CONSTRAINT[PK_dbo.EmployeesTable] PRIMARY KEY
	CLUSTERED([Id] ASC)
)

3. Create new table Departments:
CREATE TABLE [dbo].[DepartmentsTable]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [Title] VARCHAR(60) NOT NULL,
	CONSTRAINT[PK_dbo.DepartmentsTable] PRIMARY KEY
	CLUSTERED([Id] ASC)
)


To fill the tables on first start you can answer "Yes", or use next:

4. Insert new employees into table EmployeesTable (method SaveDataToDB from class Model):
var sqlEmp = $@"INSERT INTO EmployeesTable (FirstName, LastName, Age, Salary, Position, DepartmentId) 
                VALUES (N'{deps[i].Employees[j].FirstName}'
                       , '{deps[i].Employees[j].LastName}'
                       , '{deps[i].Employees[j].Age}'
                       , '{deps[i].Employees[j].Salary}'
                       , '{deps[i].Employees[j].Position}'
                       , '{i + 1}')";

5. Insert new departments into table DepartmentsTable (method SaveDataToDB from class Model):
var sqlDep = $@"INSERT INTO DepartmentsTable (Title) 
                VALUES (N'{deps[i].Title}')";