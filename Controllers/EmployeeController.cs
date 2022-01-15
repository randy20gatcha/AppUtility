using AppUtility.DataLayer;
using AppUtility.DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace AppUtility.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository context;
        private readonly DataBaseDbContext dataBase;
        public EmployeeController(IEmployeeRepository context, DataBaseDbContext dataBase)
        {
            this.context = context;
            this.dataBase = dataBase;
        }
        
        [HttpPost]
        public IActionResult GetEmployees()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() +
                                              "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var employeeData = (from tempemployee in dataBase.Employees select tempemployee);
                if (!string.IsNullOrEmpty(searchValue))
                {
                    employeeData = employeeData.Where(m => m.FirstName.Contains(searchValue)
                                        || m.LastName.Contains(searchValue)
                                        || m.EmailAddress.Contains(searchValue));
                }
                if (string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection))
                {
                    employeeData = employeeData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                recordsTotal = employeeData.Count();
                var data = employeeData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new
                {
                    draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal,
                    data
                };
                return Ok(jsonData);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
