using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client_HW_8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewMain, INotifyPropertyChanged
    {
        Presenter p;
        public event PropertyChangedEventHandler PropertyChanged;

        ObservableCollection<Employee> employees;
        ObservableCollection<Department> departments;

        public ObservableCollection<Employee> Employees
        {
            get => employees;
            set
            {
                employees = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Employees)));
            }
        }
        public ObservableCollection<Department> Departments
        {
            get => departments;
            set
            {
                departments = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Departments)));
            }
        }

        public MainWindow()
        {

            InitializeComponent();

            p = new Presenter(this);
            
            this.DataContext = this;

            btnDepEdit.Click += (s, e) => OpenEditDep();
            btnNewDep.Click += (s, e) => OpenEditDep();
            btnNewEmp.Click += (s, e) => OpenEditEmp();
            lvEmps.MouseDoubleClick += (s, e) => OpenEditEmp();

            //btnDepDelete.Click += (s, e) => p.DepDelete(cbDeps.SelectedIndex);
            //btnEmpDelete.Click += (s, e) =>
            //{
            //    p.EmpDelete(cbDeps.SelectedIndex, EmpsDataGrid.SelectedIndex);
            //    dataTable.Clear();
            //};

            cbDeps.SelectionChanged += (s, e) =>
            {
                if (cbDeps.SelectedIndex > -1)
                    Employees = Departments[cbDeps.SelectedIndex].Employees;
            };

            this.Loaded += (s, e) => p.LoadInfo();
        }

        /// <summary>
        /// Окно редактирования/добавления работника
        /// </summary>
        public void OpenEditEmp()
        {
            int iDep = cbDeps.SelectedIndex;
            EditEmpWindow editEmp = new EditEmpWindow(iDep, lvEmps.SelectedIndex);
            editEmp.ShowDialog();
            cbDeps.SelectedIndex = iDep;
        }

        /// <summary>
        /// Окно редактирование/добавления отдела
        /// </summary>
        public void OpenEditDep()
        {
            int iDep = cbDeps.SelectedIndex;
            EditDepWindow editDep = new EditDepWindow(iDep);
            editDep.ShowDialog();
        }


        private void btnEmpEdit_Click(object sender, RoutedEventArgs e)
        {
            if (cbDeps.SelectedIndex < 0 || lvEmps.SelectedIndex < 0)
            {
                MessageBox.Show("Вы не выбрали сотрудника для редактирования!");
            }
            else
            {
                OpenEditEmp();
            }
        }

    }
}
