using System.Collections.ObjectModel;
using System.Data;

namespace Client_HW_8
{
    public interface IViewMain
    {
        ObservableCollection<Department> Departments { get; set; }
    }
}
