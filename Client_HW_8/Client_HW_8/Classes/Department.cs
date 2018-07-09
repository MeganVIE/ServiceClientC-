using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Client_HW_8
{
    /// <summary>
    /// Класс "Департамент", содержащий название
    /// и список сотрудников
    /// </summary>
    public class Department: INotifyPropertyChanged
    {
        private string title;
        private ObservableCollection<Employee> employees;
        private int id;

        public string Title {
            get => title;
            set
            {
                title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Title)));
            }
        }
        public ObservableCollection<Employee> Employees => employees;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Id)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Department(string title)
        {
            this.Title = title;
            employees = new ObservableCollection<Employee>();
        }

        public override string ToString()=>$"{title}";


        public Employee this[int index]
        {
            get => !IsNullOrEmpty() ? employees[index] : null;
        }
        private bool IsNullOrEmpty()
        {
            bool flag = true;

            if (employees != null)
            {
                if (employees.Count > 0)
                {
                    flag = false;
                }
            }
            return flag;
        }
        public IEnumerator GetEnumerator()
        {
            foreach (Employee e in employees)
            {
                yield return (Employee)e;
            }
        }
    }
}
