using System;
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
}
