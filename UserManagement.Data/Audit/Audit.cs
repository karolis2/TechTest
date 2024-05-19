using System;

namespace UserManagement.Data.Audit;
public class Audit
{
    public int Id { get; set; }

    //TODO: userID will represent session user ID
    public string? UserId { get; set; }
    public string? Type { get; set; }
    public string? TableName { get; set; }
    public DateTime DateTime { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? AffectedColumns { get; set; }
    public string? PrimaryKey { get; set; }
    public long ModifiedUserId { get; set; }
}
