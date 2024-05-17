using System;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Web.Models.Users;

namespace UserManagement.Web.Tests;

public class UserControllerTests : TestsBase
{
    [Fact]
    public void List_WhenServiceReturnsUsers_ModelMustContainUsers()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var controller = CreateController();
        var users = SetupUsers();

        // Act: Invokes the method under test with the arranged parameters.
        var result = controller.List();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Model
            .Should().BeOfType<UserListViewModel>()
            .Which.Items.Should().BeEquivalentTo(users);
    }

    [Fact]
    public void Should_ReturnActiveUsersOnly_When_IsActiveServiceUsed()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var controller = CreateController();
        SetupMultipleUsers();

        // Act: Invokes the method under test with the arranged parameters.
        var result = controller.List(true);

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Model
            .Should().BeOfType<UserListViewModel>()
            .Which.Items.Should().OnlyContain(x => x.IsActive);
    }

    [Fact]
    public void Should_ReturnNonActiveUsersOnly_When_NonActiveServiceUsed()
    {
        var controller = CreateController();
        SetupMultipleUsers();

        var result = controller.List(false);

        result.Model
            .Should().BeOfType<UserListViewModel>()
            .Which.Items.Should().OnlyContain(x => !x.IsActive);
    }

    [Fact]
    public void Should_NotHaveDateOfBirthAsNullOrEmpty_When_GettingAll()
    {
        var controller = CreateController();
        SetupUsers();

        var result = controller.List();

        result.Model
            .Should().BeOfType<UserListViewModel>()
            .Which.Items.Should().OnlyContain(x => x.DateOfBirth == new DateTime(2012, 12, 25, 10, 30, 50, DateTimeKind.Utc));
    }

    [Fact]
    public void Should_NotHaveDateOfBirthAsNullOrEmpty_When_CallingFilteredList()
    {
        var controller = CreateController();
        SetupMultipleUsers();

        var result = controller.List(false);

        result.Model
            .Should().BeOfType<UserListViewModel>()
            .Which.Items.Should().OnlyContain(x => x.DateOfBirth == new DateTime(2012, 12, 25, 10, 30, 50, DateTimeKind.Utc));
    }

    [Fact]
    public void Should_AddUserToTheDd_When_CreatingNewUser()
    {
        var controller = CreateController();
        SetupMultipleUsers();

        var newUser = new UserListItemViewModel()
        {
            Id = 1,
            Forename = "John",
            Surname = "Johnson",
            Email = "jj@example.com",
            IsActive = true,
            DateOfBirth = new DateTime(2012, 11, 25, 10, 30, 50, DateTimeKind.Utc)
        };

        controller.Create(newUser);

        var result = controller.List();

        result.Model
            .Should().BeOfType<UserListViewModel>()
            .Which.Items.Should().Contain(x => x.DateOfBirth == newUser.DateOfBirth);
    }

    [Fact]
    public void Should_GetUserFromTheDb_When_WhenIdProvided()
    {
        var controller = CreateController();
        SetupMultipleUsers();

        var result = controller.Edit(1);

        result.Should().BeOfType<ViewResult>().Which.Model.Should().BeOfType<UserListItemViewModel>().Which.Id.Should().Be(1);
    }

    [Fact]
    public void Should_ChangeUserDetails_When_CallingEditService()
    {
        var controller = CreateController();
        SetupMultipleUsers();

        var newUser = new UserListItemViewModel
        {
            Id = 1,
            Forename = "John",
            Surname = "Johnson",
            Email = "jj@example.com",
            IsActive = true,
            DateOfBirth = new DateTime(2012, 11, 25, 10, 30, 50, DateTimeKind.Utc)
        };

        controller.Edit(newUser);

        var result = controller.Edit(1);

        result.Should().BeOfType<ViewResult>().Which.Model.Should().BeOfType<UserListItemViewModel>().Which.Forename.Should().Be(newUser.Forename);
        result.Should().BeOfType<ViewResult>().Which.Model.Should().BeOfType<UserListItemViewModel>().Which.Surname.Should().Be(newUser.Surname);
        result.Should().BeOfType<ViewResult>().Which.Model.Should().BeOfType<UserListItemViewModel>().Which.Email.Should().Be(newUser.Email);
        result.Should().BeOfType<ViewResult>().Which.Model.Should().BeOfType<UserListItemViewModel>().Which.IsActive.Should().Be(newUser.IsActive);
        result.Should().BeOfType<ViewResult>().Which.Model.Should().BeOfType<UserListItemViewModel>().Which.DateOfBirth.Should().Be(newUser.DateOfBirth);
    }
}
