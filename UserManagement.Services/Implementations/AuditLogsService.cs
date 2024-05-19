using System.Collections.Generic;
using System.Linq;
using UserManagement.Data;
using UserManagement.Data.Audit;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class AuditLogsService : IAuditLogsService
{
    private readonly IDataContext _dataAccess;
    public AuditLogsService(IDataContext dataAccess) => _dataAccess = dataAccess;

    //TOOO: Test
    public IEnumerable<Audit> GetAll() => _dataAccess.GetAll<Audit>();

    //TOOO: Test
    public IEnumerable<Audit> GetUserLogs(long id)
    {
        var logs = _dataAccess.GetAll<Audit>();

        var userLogs = logs.Where(log => log.ModifiedUserId == id);

        return userLogs;
    }

    //TOOO: Test
    public Audit GetSingleLogBy(int id)
    {
        var logs = _dataAccess.GetAll<Audit>();

        var singleLogById = logs.Single(log => log.Id == id);

        return singleLogById;
    }
}
