using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Service_HW_8.Models
{
    /// <summary>
    /// Класс "Департамент", содержащий название
    /// и список сотрудников
    /// </summary>
    public class Department
    {
        public string Title { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }
        public int Id { get; set; }

        public Department(string title)
        {
            this.Title = title;
            Employees = new ObservableCollection<Employee>();
        }

        public override string ToString()=>$"{Title}";


        public Employee this[int index]
        {
            get => !IsNullOrEmpty() ? Employees[index] : null;
        }
        private bool IsNullOrEmpty()
        {
            bool flag = true;

            if (Employees != null)
            {
                if (Employees.Count > 0)
                {
                    flag = false;
                }
            }
            return flag;
        }
        public IEnumerator GetEnumerator()
        {
            foreach (Employee e in Employees)
            {
                yield return (Employee)e;
            }
        }
    }
}
