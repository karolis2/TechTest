using System;
using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Interfaces;
using UserManagement.WebMS.Controllers;

namespace UserManagement.Web.Tests;

public class TestsBase
{
    protected User[] SetupUsers(string forename = "Johnny", string surname = "User", string email = "juser@example.com", bool isActive = true)
    {
        var users = new[]
        {
            new User
            {
                Forename = forename,
                Surname = surname,
                Email = email,
                IsActive = isActive,
                DateOfBirth = new DateTime(2012, 12, 25, 10, 30, 50, DateTimeKind.Utc)
            }
        };

        _userService
            .Setup(s => s.GetAll())
            .Returns(users);

        return users;
    }

    protected User[] SetupMultipleUsers()
    {
        var users = UsersData();

        _userService
            .Setup(s => s.FilterByActive(true))
            .Returns(users.Where(x => x.IsActive));

        _userService
            .Setup(s => s.FilterByActive(false))
            .Returns(users.Where(x => !x.IsActive));

        return users;
    }

    protected User[] UsersData()
    {
        return
        [
            new User { Id = 1, Forename = "Peter", Surname = "Loew", Email = "ploew@example.com", IsActive = true, DateOfBirth = new DateTime(2012, 12, 25, 10, 30, 50, DateTimeKind.Utc) },
            new User { Id = 2, Forename = "Benjamin Franklin", Surname = "Gates", Email = "bfgates@example.com", IsActive = true, DateOfBirth = new DateTime(2012, 12, 25, 10, 30, 50, DateTimeKind.Utc) },
            new User { Id = 3, Forename = "Castor", Surname = "Troy", Email = "ctroy@example.com", IsActive = false, DateOfBirth = new DateTime(2012, 12, 25, 10, 30, 50, DateTimeKind.Utc) },
            new User { Id = 4, Forename = "Memphis", Surname = "Raines", Email = "mraines@example.com", IsActive = true, DateOfBirth = new DateTime(2012, 12, 25, 10, 30, 50, DateTimeKind.Utc) },
            new User { Id = 5, Forename = "Stanley", Surname = "Goodspeed", Email = "sgodspeed@example.com", IsActive = true, DateOfBirth = new DateTime(2012, 12, 25, 10, 30, 50, DateTimeKind.Utc) },
            new User { Id = 6, Forename = "H.I.", Surname = "McDunnough", Email = "himcdunnough@example.com", IsActive = true, DateOfBirth = new DateTime(2012, 12, 25, 10, 30, 50, DateTimeKind.Utc) },
            new User { Id = 7, Forename = "Cameron", Surname = "Poe", Email = "cpoe@example.com", IsActive = false, DateOfBirth = new DateTime(2012, 12, 25, 10, 30, 50, DateTimeKind.Utc) },
            new User { Id = 8, Forename = "Edward", Surname = "Malus", Email = "emalus@example.com", IsActive = false, DateOfBirth = new DateTime(2012, 12, 25, 10, 30, 50, DateTimeKind.Utc) },
            new User { Id = 9, Forename = "Damon", Surname = "Macready", Email = "dmacready@example.com", IsActive = false, DateOfBirth = new DateTime(2012, 12, 25, 10, 30, 50, DateTimeKind.Utc) },
            new User { Id = 10, Forename = "Johnny", Surname = "Blaze", Email = "jblaze@example.com", IsActive = true, DateOfBirth = new DateTime(2012, 12, 25, 10, 30, 50, DateTimeKind.Utc) },
            new User { Id = 11, Forename = "Robin", Surname = "Feld", Email = "rfeld@example.com", IsActive = true, DateOfBirth = new DateTime(2012, 12, 25, 10, 30, 50, DateTimeKind.Utc) }
        ];
    }

    protected UsersController CreateController() => new(_userService.Object);

    private readonly Mock<IUserService> _userService = new();
}
