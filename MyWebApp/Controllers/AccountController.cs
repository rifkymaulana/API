using API.DTOs.Accounts;
using Microsoft.AspNetCore.Mvc;
using MyWebApp.Contracts;
using API.Utilities;

namespace Client.Controllers;

public class AccountController : Controller
{
    private readonly IAccountRepository repository;

    public AccountController(IAccountRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignIn(LoginAccountDto loginVM)
    {
        var result = await repository.Login(loginVM);
        if (result is null)
        {
            return RedirectToAction("Error", "Home");
        }
        else if (result.Code == 400)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return RedirectToAction("Index", "Home");
            // return View();
        }
        else if (result.Code == 200)
        {
            HttpContext.Session.SetString("JWToken", result.Data);
            return RedirectToAction("Index", "Home");
        }
        // return View();
        return RedirectToAction("Index", "Home");
    }
}
