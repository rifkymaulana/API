using Microsoft.AspNetCore.Mvc;

namespace MyWebApp.Controllers;

public class EmployeeController : Controller
{
    // get all employees from API
    public IActionResult Index()
    {
        return View();
    }

    // get employee by guid from API
    public IActionResult Details()
    {
        return View();
    }

    // create employee
    public IActionResult Create()
    {
        return View();
    }

    // edit employee
    public IActionResult Edit()
    {
        return View();
    }

    // delete employee
    public IActionResult Delete()
    {
        return View();
    }
}