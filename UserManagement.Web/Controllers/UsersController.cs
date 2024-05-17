using System;
using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet("users/")]
    public ViewResult List()
    {
        var items = _userService.GetAll().Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            IsActive = p.IsActive,
            DateOfBirth = p.DateOfBirth
        });

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    [HttpPost("users/")]
    public ViewResult List(bool isActive)
    {
        var items = _userService
            .FilterByActive(isActive)
            .Select(p => new UserListItemViewModel
            {
                Id = p.Id,
                Forename = p.Forename,
                Surname = p.Surname,
                Email = p.Email,
                IsActive = p.IsActive,
                DateOfBirth = p.DateOfBirth
            });

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    [HttpGet("users/create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("users/create")]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Forename,Surname,Email,IsActive,DateOfBirth")] UserListItemViewModel userViewModel)
    {
        if (ModelState.IsValid)
        {
            if (userViewModel.DateOfBirth >= DateTime.Today)
            {
                ModelState.AddModelError(string.Empty, "Date of birthday can't be higher than today.");
                return View();
            }

            //todo: not sure if good idea to suppress null
            var user = new User
            {
                Forename = userViewModel.Forename!,
                Surname = userViewModel.Surname!,
                Email = userViewModel.Email!,
                IsActive = userViewModel.IsActive,
                DateOfBirth = userViewModel.DateOfBirth
            };

            _userService.CreateUser(user);

            return RedirectToAction(nameof(List));
        }

        return View();
    }
}
