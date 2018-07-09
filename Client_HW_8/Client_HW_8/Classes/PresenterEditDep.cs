using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_HW_8
{
    class PresenterEditDep
    {
        private IViewDepartment viewDepartment;

        public PresenterEditDep(IViewDepartment viewDepartment)
        {
            this.viewDepartment = viewDepartment;
        }

        /// <summary>
        /// Обновление данных по отделам
        /// </summary>
        public void Departments()
        {
            viewDepartment.Departments = Model.Departments;
        }

        /// <summary>
        /// Добавление нового отдела
        /// </summary>
        public void AddDepartment()
        {
            Model.Departments.Add(new Department(viewDepartment.DepTitle) { Id = Model.Departments.Count + 1 });
            Model.AddDepartment();
        }

        /// <summary>
        /// Изменение наименование отдела
        /// </summary>
        /// <param name="i">Индекс отдела</param>
        public void EditDepartment(int i)
        {
            Model.Departments[i].Title = viewDepartment.DepTitle;
            Model.EditDepartment(i);
        }
    }
}
