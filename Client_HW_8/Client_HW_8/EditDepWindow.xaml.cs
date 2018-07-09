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
    /// Логика взаимодействия для EditDepWindow.xaml
    /// </summary>
    public partial class EditDepWindow : Window, IViewDepartment
    {
        PresenterEditDep p;

        public ObservableCollection<Department> Departments { get; set; }
        public string DepTitle { get; set; }

        public EditDepWindow(int iDep)
        {
            InitializeComponent();

            p = new PresenterEditDep(this);
            p.Departments();

            this.Loaded += delegate
            {
                if (iDep > -1)
                    tbDepTitle.Text = Departments[iDep].Title;
            };

            btnSaveDep.Click += delegate
            {
                DepTitle = tbDepTitle.Text;
                if (EqualsNull(DepTitle))
                {
                    MessageBox.Show("Введены не все данные!");
                }
                else
                {
                    var res = MessageBox.Show("Добавить новый департамент?", "", MessageBoxButton.YesNo);
                    if (res == MessageBoxResult.Yes)
                    {
                        p.AddDepartment();
                    }
                    else
                    {
                        p.EditDepartment(iDep);
                    }
                    this.Close();
                }

            };

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
}
