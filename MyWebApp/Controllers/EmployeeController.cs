using Microsoft.AspNetCore.Mvc;

namespace MyWebApp.Controllers;

public class EmployeeController : Controller
{
    // get all employees from API
    public IActionResult Index()
    {
        return View();
    }
}
