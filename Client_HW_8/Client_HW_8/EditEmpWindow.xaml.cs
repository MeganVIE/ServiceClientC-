using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Client_HW_8
{
    /// <summary>
    /// Логика взаимодействия для EditEmpWindow.xaml
    /// </summary>
    public partial class EditEmpWindow : Window, IViewEmployeer
    {
        PresenterEditEmp p;

        public EditEmpWindow(int iDep, int iEmp)
        {
            InitializeComponent();

            p = new PresenterEditEmp(this);

            this.Loaded += delegate
            {
                p.LoadEmployee(iDep, iEmp);
                if (iDep > -1)
                {
                    cbDep.SelectedIndex = iDep;
                }
            };

            btnEditEmp.Click += delegate
            {
                if (EqualsNull(FirstName)
                    || EqualsNull(LastName)
                    || Age <= 0
                    || Salary <= 0
                    || EqualsNull(Position)
                    || cbDep.SelectedIndex < 0)
                {
                    MessageBox.Show("Введены не все данные!");
                }
                else
                {
                    if (p.Equals(cbDep.SelectedIndex, iDep))
                    {
                        MessageBox.Show("Такой сотрудник уже существует!");
                    }
                    else
                    {
                        var res = MessageBox.Show("Добавить нового сотрудника?", "", MessageBoxButton.YesNo);
                        if (res == MessageBoxResult.Yes)
                        {
                            p.AddEmployee(cbDep.SelectedIndex);
                        }
                        else
                        {
                            p.EditEmployee(cbDep.SelectedIndex, iDep, iEmp);
                        }
                        this.Close();
                    }
                }

            };
        }

        public string FirstName
        {
            get => tbFName.Text;
            set => tbFName.Text = value;
        }
        public string LastName
        {
            get => tbLName.Text;
            set => tbLName.Text = value;
        }
        public int Age
        {
            get => Int32.Parse(tbAge.Text);
            set => tbAge.Text = value.ToString();
        }
        public int Salary
        {
            get => Int32.Parse(tbSalary.Text);
            set => tbSalary.Text = value.ToString();
        }
        public string Position
        {
            get => tbPosition.Text;
            set => tbPosition.Text = value;
        }
        public ObservableCollection<Department> Departments
        {
            get => (ObservableCollection<Department>)cbDep.ItemsSource;
            set => cbDep.ItemsSource = value;
        }

        /// <summary>
        /// Проверка строки на соответствие
        /// </summary>
        bool EqualsNull(string str)
        {
            if (str == null)
            {
                return true;
            }
            string s = new string(' ', str.Length);
            if (str == s)
            {
                return true;
            }
            return false;
        }

    }
}
