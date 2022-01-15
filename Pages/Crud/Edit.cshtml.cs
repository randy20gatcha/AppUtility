using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppUtility.DataLayer.Repositories;
using AppUtility.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUtility.Pages.Crud
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository context;
        public EditModel(IEmployeeRepository context)
        {
            this.context = context;
        }
        [BindProperty]
        public Employee Employee { get; set; }
        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Employee = context.GetEmployee(id.Value);
            }
            else
            {
                Employee = new Employee();
            }

            if (Employee == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }
        //Used for both Creating and Editing row data
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Employee.Id > 0)
                {
                    Employee = context.Update(Employee);

                }
                else
                {
                    Employee = context.Add(Employee);

                }
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
