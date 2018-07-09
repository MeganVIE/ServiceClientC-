using System;
using System.ComponentModel;

namespace Client_HW_8
{
    /// <summary>
    /// Класс "Работник", содержащий данные об имени,
    /// возрасте, зарплате и дожности
    /// </summary>
    public class Employee : INotifyPropertyChanged, IEquatable<Employee>
    {
        private string firstName;
        private string lastName;
        private int age;
        private int salary;
        private string position;
        private int id;

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.FirstName)));
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.LastName)));
            }
        }
        public int Age
        {
            get => age;
            set
            {
                age = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Age)));
            }
        }
        public int Salary
        {
            get => salary;
            set
            {
                salary = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Salary)));
            }
        }
        public string Position
        {
            get => position;
            set
            {
                position = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Position)));
            }
        }
        public int ID { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Employee() { }

        public Employee(string firstName, string lastName, int age, int salary, string position)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.Salary = salary;
            this.Position = position;
        }

        /// <summary>
        /// Проверка на равность двух работников
        /// </summary>
        public bool Equals(Employee other)
        {
            return this.firstName == other.FirstName
                   && this.lastName == other.LastName
                   && this.age == other.Age
                   && this.salary == other.Salary
                   && this.position == other.Position;
        }

        //public override string ToString()=>$"{firstName} {lastName} {age} {salary} {position}";
        
    }
}
