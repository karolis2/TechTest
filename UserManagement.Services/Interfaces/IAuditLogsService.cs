using System.Collections.Generic;
using UserManagement.Data.Audit;

namespace UserManagement.Services.Interfaces;

public interface IAuditLogsService
{
    public IEnumerable<Audit> GetAll();
    public IEnumerable<Audit> GetUserLogs(long id);
}
