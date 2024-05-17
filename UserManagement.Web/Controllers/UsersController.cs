using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            //TODO: test
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

    [HttpPut("edit")]
    public IActionResult Edit(int id)
    {
        //TODO: validate
        var user = _userService.GetUser(id);

        var itemViewModel = new UserListItemViewModel
        {
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            IsActive = user.IsActive,
            DateOfBirth = user.DateOfBirth
        };

        return View(itemViewModel);
    }

    [HttpPut("edit")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit([Bind("Id,Forename,Surname,Email,IsActive,DateOfBirth")] UserListItemViewModel userViewModel)
    {
        var user = new User
        {
            Id = userViewModel.Id,
            Forename = userViewModel.Forename!,
            Surname = userViewModel.Surname!,
            Email = userViewModel.Email!,
            IsActive = userViewModel.IsActive,
            DateOfBirth = userViewModel.DateOfBirth
        };

        if (ModelState.IsValid)
        {
            //TODO: DRY
            if (userViewModel.DateOfBirth >= DateTime.Today)
            {
                ModelState.AddModelError(string.Empty, "Date of birthday can't be higher than today.");
                return View();
            }
            try
            {
                _userService.UpdateUser(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                var userExists = _userService.CheckIfUserExists(user.Id);

                if (!userExists)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(List));
        }
        return View(userViewModel);
    }

    [HttpGet("users/view")]
    public IActionResult UserDetails(int id)
    {
        //TODO: Check if not found.

        var user = _userService.GetUser(id);

        var itemViewModel = new UserListItemViewModel
        {
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            IsActive = user.IsActive,
            DateOfBirth = user.DateOfBirth
        };

        return View(itemViewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _userService.DeleteUser(id);
        return RedirectToAction(nameof(List));
    }

    public IActionResult Delete(int id)
    {
        var user = _userService.GetUser(id);

        var itemViewModel = new UserListItemViewModel
        {
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            IsActive = user.IsActive,
            DateOfBirth = user.DateOfBirth
        };

        return View(itemViewModel);
    }
}
