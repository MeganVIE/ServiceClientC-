using Service_HW_8.Models;
using System.Collections.ObjectModel;
using System.Data;

namespace Service_HW_8
{
    public interface IViewMain
    {
        ObservableCollection<Department> Departments { get; set; }
    }
}
