using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Client_HW_8
{
    static class Model
    {
        static HttpClient client = new HttpClient();

        static ObservableCollection<Department> deps;
        public static ObservableCollection<Department> Departments => deps;

        static Model()
        {
            deps = new ObservableCollection<Department>();
        }
        
        /// <summary>
        /// Загрузка данных в базы при старте
        /// </summary>
        public static void Start()
        {
            client.BaseAddress = new Uri(@"http://localhost:63346/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            LoadData();
        }

        /// <summary>
        /// Загрузка данных с сервиса
        /// </summary>
        public static void LoadData()
        {
            Departments.Clear();
            string response = client.GetStringAsync(client.BaseAddress + "getDepsCount").Result;
            int count = Int32.Parse(response);
            for (int i = 1; i <= count; i++)
            {
                response = client.GetStringAsync(client.BaseAddress + $@"getEmployees/{i}").Result;
                var depData = JObject.Parse(response);

                Departments.Add(new Department(depData["Title"].ToString()));

                var emps = JObject.Parse(response)["Employees"].ToArray();

                foreach (var emp in emps)
                {
                    try
                    {
                        Departments[Departments.Count - 1].Id = Int32.Parse(emp["DepID"].ToString());

                        string fname = emp["FirstName"].ToString();
                        string lname = emp["LastName"].ToString();
                        string age = emp["Age"].ToString();
                        string salary = emp["Salary"].ToString();
                        string pos = emp["Position"].ToString();
                        string id = emp["ID"].ToString();

                        Departments[Departments.Count - 1]
                                  .Employees.Add(new Employee(fname
                                                            , lname
                                                            , Int32.Parse(age)
                                                            , Int32.Parse(salary)
                                                            , pos)
                                  { ID = Int32.Parse(id) });
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Добавление нового отдела
        /// </summary>
        public static void AddDepartment()
        {
            var json = JsonConvert.SerializeObject(deps[deps.Count - 1]);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            client.PostAsync(client.BaseAddress + "addDepartment", stringContent);
        }

        /// <summary>
        /// Добавление нового сотрудника 
        /// </summary>
        public static void AddEmployee(int i)
        {
            var json = JsonConvert.SerializeObject(deps[i].Employees[deps[i].Employees.Count - 1]);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            client.PostAsync(client.BaseAddress + "addEmployee", stringContent);
        }

        /// <summary>
        /// Изменение названия отдела
        /// </summary>
        /// <param name="i">Индекс отдела</param>
        public static void EditDepartment(int i)
        {
            var json = JsonConvert.SerializeObject(deps[i]);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            client.PostAsync(client.BaseAddress + "editDepartment", stringContent);
        }

        /// <summary>
        /// Изменение сотрудника
        /// </summary>
        /// <param name="iDep">Индекс отдела</param>
        /// <param name="iEmp">Индекс сотрудника</param>
        public static void EditEmployee(int iDep, int iEmp)
        {
            var json = JsonConvert.SerializeObject(deps[iDep].Employees[iEmp]);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            client.PostAsync(client.BaseAddress + "editEmployee", stringContent);
        }

    }
}
