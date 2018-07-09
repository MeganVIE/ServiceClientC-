using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_HW_8
{
    class PresenterEditEmp
    {
        private IViewEmployeer viewEmployeer;

        public PresenterEditEmp(IViewEmployeer viewEmployeer)
        {
            this.viewEmployeer = viewEmployeer;
        }

        /// <summary>
        /// Pагрузка данных по работнику
        /// </summary>
        /// <param name="iDep">Индекс отдела</param>
        /// <param name="iEmp">Индекс сотрудника в отделе</param>
        public void LoadEmployee(int iDep, int iEmp)
        {
            viewEmployeer.Departments = Model.Departments;
            if (iDep > -1 && iEmp > -1)
            {
                Employee emp = Model.Departments[iDep].Employees[iEmp];
                viewEmployeer.FirstName = emp.FirstName;
                viewEmployeer.LastName = emp.LastName;
                viewEmployeer.Age = emp.Age;
                viewEmployeer.Salary = emp.Salary;
                viewEmployeer.Position = emp.Position;
            }
        }

        /// <summary>
        /// Проверка на совпадение по сотрудникам
        /// </summary>
        /// <param name="index">Выбранных отдел</param>
        /// <param name="iDep">Исходный отдел</param>
        public bool Equals(int index, int iDep)
        {
            if (index != iDep)
            {
                return false;
            }

            Employee emp = new Employee(viewEmployeer.FirstName,
                                        viewEmployeer.LastName,
                                        viewEmployeer.Age,
                                        viewEmployeer.Salary,
                                        viewEmployeer.Position);
            bool flag = false;

            foreach (Department d in Model.Departments)
            {
                foreach (Employee e in d.Employees)
                {
                    if (emp.Equals(e))
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }


        /// <summary>
        /// Добавление работника в отдел
        /// </summary>
        /// <param name="index">Индекс отдела</param>
        public void AddEmployee(int index)
        {
            Model.Departments[index]
                 .Employees
                 .Add(new Employee(viewEmployeer.FirstName,
                                   viewEmployeer.LastName,
                                   viewEmployeer.Age,
                                   viewEmployeer.Salary,
                                   viewEmployeer.Position));
            Model.AddEmployee(index);
        }

        /// <summary>
        /// Редактирование работника
        /// </summary>
        /// <param name="index">Выбранный отдел</param>
        /// <param name="iDep">Исходный отдел</param>
        /// <param name="iEmp">Индекс сотрудника в исходном отделе</param>
        public void EditEmployee(int index, int iDep, int iEmp)
        {
            Employee emp = new Employee(viewEmployeer.FirstName,
                                        viewEmployeer.LastName,
                                        viewEmployeer.Age,
                                        viewEmployeer.Salary,
                                        viewEmployeer.Position)
                                        { ID = Model.Departments[iDep].Employees[iEmp].ID };
            if (index != iDep)
            {
                Model.Departments[iDep].Employees.RemoveAt(iEmp);
                Model.Departments[index].Employees.Add(emp);
                Model.EditEmployee(index, iEmp);
            }
            else
            {
                Model.Departments[iDep].Employees[iEmp] = emp;
                Model.EditEmployee(iDep, iEmp);
            }

        }
    }
}
