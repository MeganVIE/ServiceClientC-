using System;
using System.ComponentModel;

namespace Service_HW_8.Models
{
    /// <summary>
    /// Класс "Работник", содержащий данные об имени,
    /// возрасте, зарплате и дожности
    /// </summary>
    public class Employee : IEquatable<Employee>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public string Position { get; set; }
        public int ID { get; set; }
        public int DepID { get; set; }

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
            return this.FirstName == other.FirstName
                   && this.LastName == other.LastName
                   && this.Age == other.Age
                   && this.Salary == other.Salary
                   && this.Position == other.Position;
        }

        //public override string ToString()=>$"{firstName} {lastName} {age} {salary} {position}";
        
    }
}
