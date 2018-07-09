using Service_HW_8.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

/*Изменить WPF-приложение для ведения списка сотрудников компании (из урока №7), используя веб-сервисы. 
 * Разделить приложение на две части. Первая часть – клиентское приложение, отображающее данные. Вторая 
 * часть – веб-сервис и код, связанный с извлечением данных из БД. Приложение реализует только просмотр 
 * данных. При разработке приложения допустимо использовать любой из рассмотренных типов веб-сервисов.
1. Создать таблицы Employee и Department в БД MSSQL Server и заполнить списки сущностей начальными данными.
2. Для списка сотрудников и списка департаментов предусмотреть визуализацию (отображение).
3. Разработать формы для отображения отдельных элементов списков сотрудников и департаментов.*/

namespace Service_HW_8.Controllers
{
    public class EmployeeController : ApiController
    {
        Presenter pr = new Presenter();

        [Route("getEmployees/{id}")]
        public Department Get1(int id)
        {
            return pr.GetList(id);
        }

        [Route("getEmployee/{id}")]
        public Employee Get2(int id)
        {
            return pr.GetEmployeeId(id);
        }

        [Route("getDepsCount")]
        public int Get()
        {
            return pr.Departments.Count;
        }

        [Route("addEmployee")]
        public HttpResponseMessage Post([FromBody]Employee value)
        {
            if (pr.AddEmployee(value))
                return Request.CreateResponse(HttpStatusCode.Created);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("addDepartment")]
        public HttpResponseMessage Post([FromBody]Department value)
        {
            if (pr.AddDepartment(value))
                return Request.CreateResponse(HttpStatusCode.Created);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("editDepartment")]
        public HttpResponseMessage Post2([FromBody]Department value)
        {
            if (pr.EditDepartment(value))
                return Request.CreateResponse(HttpStatusCode.Created);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("editEmployee")]
        public HttpResponseMessage Post2([FromBody]Employee value)
        {
            if (pr.EditEmployee(value))
                return Request.CreateResponse(HttpStatusCode.Created);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
