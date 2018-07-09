using Service_HW_8.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Service_HW_8 //Веденеева Ирина
{
    class Presenter : IViewMain
    {
        public ObservableCollection<Department> Departments { get; set; }

        public Presenter()
        {
            Departments = new ObservableCollection<Department>();
            Model.Start();
            LoadInfo();
        }

        /// <summary>
        /// Получение списка сотрудников отдела
        /// </summary>
        /// <param name="id">ID отдела</param>
        internal Department GetList(int id)
        {
            return Model.GetEmployees(id);
        }

        /// <summary>
        /// Получение данных по сотруднику
        /// </summary>
        /// <param name="id">ID сотрудника</param>
        /// <returns></returns>
        internal Employee GetEmployeeId(int id)
        {
            return Model.GetEmployeeId(id);
        }

        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <param name="value">Новый сотрудник</param>
        internal bool AddEmployee(Employee value)
        {
            return Model.AddEmployee(value);
        }

        /// <summary>
        /// Добавление отдела
        /// </summary>
        /// <param name="value">Новый отдел</param>
        internal bool AddDepartment(Department value)
        {
            return Model.AddDepartment(value);
        }

        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <param name="value">Новый сотрудник</param>
        internal bool EditEmployee(Employee value)
        {
            return Model.EditEmployee(value);
        }

        /// <summary>
        /// Добавление отдела
        /// </summary>
        /// <param name="value">Новый отдел</param>
        internal bool EditDepartment(Department value)
        {
            return Model.EditDepartment(value);
        }

        /// <summary>
        /// Загрузка данных из БД
        /// </summary>
        public void LoadInfo()
        {
            Model.LoadDataFromDB();
            DepsToView();
        }

        /// <summary>
        /// Перенос данных из модели
        /// </summary>
        private void DepsToView()
        {
            Departments = Model.Departments;
        }

        /// <summary>
        /// Сохранение данных в файл
        /// </summary>
        public void SaveData()
        {
            Model.SaveDataToFile();
        }

    }
}
