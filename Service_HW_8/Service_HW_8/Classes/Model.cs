using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using Service_HW_8.Models;

namespace Service_HW_8
{
    static class Model
    {
        static ObservableCollection<Department> deps;
        static string path = @"C:\Users\Megan\source\repos\Service_HW_8\departments.txt";
        static string connectionString =@"  Data Source=(localdb)\MSSQLLocalDB;
                                            Initial Catalog=HWdb;
                                            Integrated Security=True;";

        public static ObservableCollection<Department> Departments => deps;

        static Model()
        {
            deps = new ObservableCollection<Department>();
        }

        /// <summary>
        /// Загрузка данных из файла
        /// </summary>
        internal static void LoadDataFromFile()
        {
            deps.Clear();
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (line == "Department:")
                        {
                            string title = sr.ReadLine();
                            deps.Add(new Department(title));
                        }
                        else
                        {
                            if (line == "Employee:")
                            {
                                string fname = sr.ReadLine();
                                string lname = sr.ReadLine();
                                int age = Int32.Parse(sr.ReadLine());
                                int salary = Int32.Parse(sr.ReadLine());
                                string position = sr.ReadLine();
                                deps[deps.Count - 1]
                                    .Employees.Add(new Employee(fname, lname, age, salary, position));
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                using (StreamWriter sw = new StreamWriter(@"C:\Users\Megan\source\repos\Service_HW_8\log.txt",false))
                {
                    sw.WriteLine(e.Message+ " LoadDataFromFile");
                }
            }
        }

        /// <summary>
        /// Запись данных в файл
        /// </summary>
        internal static void SaveDataToFile()
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (Department d in deps)
                {
                    sw.WriteLine("Department:");
                    sw.WriteLine(d.Title);
                    foreach (Employee e in d.Employees)
                    {
                        sw.WriteLine("Employee:");
                        sw.WriteLine(e.FirstName);
                        sw.WriteLine(e.LastName);
                        sw.WriteLine(e.Age);
                        sw.WriteLine(e.Salary);
                        sw.WriteLine(e.Position);
                    }
                }
            }
        }

        /// <summary>
        /// Запись данных в базы данных
        /// </summary>
        internal static void SaveDataToDB()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var sql = @"TRUNCATE TABLE EmployeesTable";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                    sql = @"TRUNCATE TABLE DepartmentsTable";
                    command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
                SaveDep();
                DepId();
                SaveEmp();
                EmpId();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "_SaveDataToDB");
            }
        }

        /// <summary>
        /// Запись в БД сотрудников
        /// </summary>
        private static void SaveEmp()
        {
            try
            {
                for (int i = 0; i < deps.Count; i++)
                {
                    for (int j = 0; j < deps[i].Employees.Count; j++)
                    {
                        var sqlEmp = $@"INSERT INTO EmployeesTable (FirstName, LastName, Age, Salary, Position, DepartmentId) 
                                        VALUES (N'{deps[i].Employees[j].FirstName}'
                                               , '{deps[i].Employees[j].LastName}'
                                               , '{deps[i].Employees[j].Age}'
                                               , '{deps[i].Employees[j].Salary}'
                                               , '{deps[i].Employees[j].Position}'
                                               , '{deps[i].Id}')";

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            SqlCommand command = new SqlCommand(sqlEmp, connection);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "_SaveEmp");
            }
        }

        /// <summary>
        /// Получение id сотрудников
        /// </summary>
        private static void EmpId()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    for (int i = 0; i < deps.Count; i++)
                    {
                        var sql = $@"SELECT * FROM EmployeesTable WHERE DepartmentId={deps[i].Id}";
                        SqlCommand command = new SqlCommand(sql, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        int j = 0;
                        while (reader.Read())
                        {
                            deps[i].Employees[j].ID = reader.GetInt32(0);
                            deps[i].Employees[j].DepID = deps[i].Id;
                            j++;
                        }
                        reader.Close();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " _EmpId");
            }
        }

        /// <summary>
        /// Запись в БД отделов
        /// </summary>
        private static void SaveDep()
        {
            try
            {
                for (int i = 0; i < deps.Count; i++)
                {
                    var sql = $@"INSERT INTO DepartmentsTable (Title) 
                                 VALUES (N'{deps[i].Title}')";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sql, connection);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "_SaveDep");
            }
        }

        /// <summary>
        /// Получение id отделов
        /// </summary>
        private static void DepId()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var sql = @" SELECT ID FROM DepartmentsTable";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    int i = 0;
                    while (reader.Read())
                    {
                        deps[i].Id = reader.GetInt32(0);
                        i++;
                    }
                    reader.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "_DepId");
            }
        }

        /// <summary>
        /// Загрузка данных из баз данных
        /// </summary>
        internal static void LoadDataFromDB()
        {
            deps.Clear();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var sql = @" SELECT * FROM DepartmentsTable";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        deps.Add(new Department(reader.GetString(1)) { Id = reader.GetInt32(0) });
                    }
                    reader.Close();
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message + " _LoadDataFromDB");
            }
            LoadEmps();
        }

        /// <summary>
        /// Считываение сотрудников из БД
        /// </summary>
        private static void LoadEmps()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    for (int i = 0; i < deps.Count; i++)
                    {
                        deps[i].Employees.Clear();

                        var sql = $@"SELECT * FROM EmployeesTable WHERE DepartmentId='{deps[i].Id}'";
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlCommand command = new SqlCommand(sql, connection);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            deps[i].Employees.Add(new Employee(reader.GetString(1)
                                                             , reader.GetString(2)
                                                             , reader.GetInt32(3)
                                                             , reader.GetInt32(4)
                                                             , reader.GetString(5))
                            { ID = reader.GetInt32(0), DepID = deps[i].Id });
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " _LoadEmps");
            }
        }

        /// <summary>
        /// Загрузка данных в базы при старте
        /// </summary>
        internal static void Start()
        {
            LoadDataFromFile();
            SaveDataToDB();
        }

        /// <summary>
        /// Добавление нового отдела в БД
        /// </summary>
        internal static void AddDepartmentToDB()
        {
            try
            {
                var sql = $@"INSERT INTO DepartmentsTable (Title) 
                             VALUES (N'{deps[deps.Count - 1].Title}')";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " _AddDepartmentToDB");
            }
            DepId();
        }

        /// <summary>
        /// Добавление нового сотрудника в БД
        /// </summary>
        internal static void AddEmployeeToDB(int i)
        {
            try
            {
                int j = deps[i].Employees.Count - 1;
                var sql = $@"INSERT INTO EmployeesTable (FirstName, LastName, Age, Salary, Position, DepartmentId) 
                             VALUES (N'{deps[i].Employees[j].FirstName}'
                                    , '{deps[i].Employees[j].LastName}'
                                    , '{deps[i].Employees[j].Age}'
                                    , '{deps[i].Employees[j].Salary}'
                                    , '{deps[i].Employees[j].Position}'
                                    , '{deps[i].Id}')";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " _AddEmployeeToDB");
            }
            //EmpId();
        }

        /// <summary>
        /// Изменение названия отдела в БД
        /// </summary>
        /// <param name="i">Индекс отдела</param>
        internal static bool EditDepartment(Department dep)
        {
            try
            {
                var sql = $@"UPDATE DepartmentsTable SET Title ='{dep.Title}' WHERE Id = '{dep.Id}'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
                LoadDataFromDB();
                SaveDataToFile();
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Изменение сотрудника в БД
        /// </summary>
        /// <param name="iDep">Индекс отдела</param>
        /// <param name="iEmp">Индекс сотрудника</param>
        internal static bool EditEmployee(Employee emp)
        {
            try
            {
                string sql= $@"UPDATE EmployeesTable SET FirstName ={emp.FirstName}
                                                       , LastName ={emp.LastName}
                                                       , Age ={emp.Age}
                                                       , Salary ={emp.Salary}
                                                       , Position ={emp.Position}
                               WHERE Id = {emp.ID}";
                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
                LoadDataFromDB();
                SaveDataToFile();
            }
            catch
            {
                return false;
            }
            LoadEmps();
            return true;
        }

        /// <summary>
        /// Получение списка сотрудников по id отдела
        /// </summary>
        internal static Department GetEmployees(int id)
        {
            
            Department dep = new Department("");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var sql = $@"SELECT * FROM EmployeesTable WHERE DepartmentId={id}";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        dep.Employees.Add(new Employee(reader.GetString(1)
                                            , reader.GetString(2)
                                            , reader.GetInt32(3)
                                            , reader.GetInt32(4)
                                            , reader.GetString(5))
                        { ID = reader.GetInt32(0), DepID = id });
                    }
                    reader.Close();

                    sql = $@"SELECT * FROM DepartmentsTable WHERE Id={id}";
                    command = new SqlCommand(sql, connection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        dep.Title = reader.GetString(1);
                    }
                    dep.Id = id;
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " _GetEmployees");
            }
            return dep;
        }

        /// <summary>
        /// Получение данных о сотруднике по его id
        /// </summary>
        internal static Employee GetEmployeeId(int id)
        {
            Employee emp = new Employee();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var sql = $@"SELECT * FROM EmployeesTable WHERE Id={id}";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        emp = new Employee(reader.GetString(1)
                                            , reader.GetString(2)
                                            , reader.GetInt32(3)
                                            , reader.GetInt32(4)
                                            , reader.GetString(5))
                                          { ID = reader.GetInt32(0), DepID=reader.GetInt32(6) };
                    }
                    reader.Close();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " _GetEmployeeId");
            }
            return emp;
        }

        /// <summary>
        /// Добавление нового сотрудника в БД
        /// </summary>
        internal static bool AddEmployee(Employee emp)
        {
            try
            {
                var sql = $@"INSERT INTO EmployeesTable (FirstName, LastName, Age, Salary, Position, DepartmentId) 
                             VALUES (N'{emp.FirstName}'
                                    , '{emp.LastName}'
                                    , '{emp.Age}'
                                    , '{emp.Salary}'
                                    , '{emp.Position}'
                                    , '{emp.DepID}')";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                return false;
            }
            LoadDataFromDB();
            SaveDataToFile();
            return true;
        }

        /// <summary>
        /// Добавление нового сотрудника в БД
        /// </summary>
        internal static bool AddDepartment(Department dep)
        {
            try
            {
                var sql = $@"INSERT INTO DepartmentsTable (Title) 
                             VALUES (N'{dep.Title}')";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
                DepId();

                for (int i = 0; i < dep.Employees.Count; i++)
                {
                    sql = $@"INSERT INTO EmployeesTable (FirstName, LastName, Age, Salary, Position, DepartmentId) 
                             VALUES (N'{dep.Employees[i].FirstName}'
                                    , '{dep.Employees[i].LastName}'
                                    , '{dep.Employees[i].Age}'
                                    , '{dep.Employees[i].Salary}'
                                    , '{dep.Employees[i].Position}'
                                    , '{deps[deps.Count-1].Id}')";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(sql, connection);
                        command.ExecuteNonQuery();
                    }
                }
                LoadDataFromDB();
                SaveDataToFile();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
