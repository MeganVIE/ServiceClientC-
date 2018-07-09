using System.Collections.ObjectModel;

namespace Client_HW_8
{
    public interface IViewDepartment : IViewMain
    {
        string DepTitle { get; set; }
        //ObservableCollection<Department> Departments { get; set; }
    }
}
