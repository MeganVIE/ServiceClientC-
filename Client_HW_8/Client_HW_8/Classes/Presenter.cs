using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Client_HW_8 //Веденеева Ирина
{
    class Presenter 
    {
        private IViewMain viewMain;


        public Presenter(IViewMain viewMain)
        {
            this.viewMain = viewMain;
            Model.Start();
        }

        /// <summary>
        /// Загрузка данных из сервиса
        /// </summary>
        public void LoadInfo()
        {
            Model.LoadData();
            viewMain.Departments = Model.Departments;
        }

        /// <summary>
        /// Сохранение данных в файл
        /// </summary>
        public void SaveData()
        {
            //Model.SaveDataToFile();
        }

    }
}
