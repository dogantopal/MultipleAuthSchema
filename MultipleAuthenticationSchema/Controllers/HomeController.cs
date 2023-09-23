using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MultipleAuthenticationSchema.Models;
using MultipleAuthenticationSchema.Services;

namespace MultipleAuthenticationSchema.Controllers;

public class HomeController : Controller
{
    private readonly AuthService _tokenService;
    private readonly CookieHelperService _cookieHelperService;

    public HomeController(
        AuthService tokenService,
        CookieHelperService cookieHelperService)
    {
        _tokenService = tokenService;
        _cookieHelperService = cookieHelperService;
    }

    public IActionResult Index()
    {
        var token = _tokenService.CreateToken();
        var cookie = _cookieHelperService.GetCookie();

        ViewBag.NoAuth = TempData["NoAuth"] ?? false;

        return View(new Account
        {
            Token = token,
            Cookie = cookie
        });
    }

    public async Task<IActionResult> Login()
    {
        var claims = _tokenService.GetClaims();
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(principal);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult LoginRedirect()
    {
        TempData["NoAuth"] = true;
        return RedirectToAction(nameof(Index));
    }
}