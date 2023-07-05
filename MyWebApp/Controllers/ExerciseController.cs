using Microsoft.AspNetCore.Mvc;

namespace MyWebApp.Controllers;

public class ExerciseController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
