using AppUtility.DataLayer.Repositories;
using AppUtility.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUtility.Pages.Crud
{
    public class DeleteModel : PageModel
    {
        private readonly IEmployeeRepository context;
        public DeleteModel(IEmployeeRepository context)
        {
            this.context = context;
        }
        [BindProperty]
        public Employee Employee { get; set; }
        public IActionResult OnGet(int id)
        {
            Employee = context.GetEmployee(id);
           
            return Page();
        }
        public IActionResult OnPost()
        {
            Employee = context.Delete(Employee.Id);
            return RedirectToPage("/Index");
        }
    }
}
