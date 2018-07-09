using System.Collections.ObjectModel;

namespace Client_HW_8
{
    public interface IViewEmployeer : IViewMain
    {
         string FirstName { get; set; }
         string LastName { get; set; }
         int Age { get; set; }
         int Salary { get; set; }
         string Position { get; set; }
         //ObservableCollection<Department> Departments { get; set; }
    }
}
