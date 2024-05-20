using System;
using System.ComponentModel.DataAnnotations;
using UserManagement.Web.Models.AuditLogs;

namespace UserManagement.Web.Models.Users;

public class UserListViewModel
{
    public List<UserListItemViewModel> Items { get; set; } = new();
}

public class UserListItemViewModel
{
    public long Id { get; set; }

    [Required]
    public string? Forename { get; set; }
    [Required]
    public string? Surname { get; set; }

    [Required]
    [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    public UserLogListViewModel LogItems { get; set; } = new();
}
