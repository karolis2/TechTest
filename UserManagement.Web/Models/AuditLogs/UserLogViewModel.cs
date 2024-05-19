using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Web.Models.AuditLogs;

public class UserLogListViewModel
{
    public List<UserLogViewModel> Items { get; set; } = new();
}

public class UserLogViewModel
{
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


}
