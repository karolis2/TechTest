using System.Linq;
using UserManagement.Services.Interfaces;
using UserManagement.Web.Models.AuditLogs;

namespace UserManagement.Web.Controllers
{
    public class AuditsController : Controller
    {
        private readonly IAuditLogsService _auditLogsService;
        public AuditsController(IAuditLogsService auditLogsService) => _auditLogsService = auditLogsService;

        public ViewResult Index()
        {
            var logs = _auditLogsService.GetAll();

            var logItemVm = logs.Select(x => new UserLogItemViewModel
            {
                AffectedColumns = x.AffectedColumns,
                DateTime = x.DateTime,
                Id = x.Id,
                ModifiedUserId = x.ModifiedUserId,
                NewValues = x.NewValues,
                OldValues = x.OldValues,
                PrimaryKey = x.PrimaryKey,
                TableName = x.TableName,
                Type = x.Type
            });

            var model = new UserLogListViewModel
            {
                Items = logItemVm.ToList()
            };

            return View(model);
        }

        public ViewResult Details(int id)
        {
            var log = _auditLogsService.GetSingleLogBy(id);

            var logItemVm = new UserLogItemViewModel
            {
                AffectedColumns = log.AffectedColumns,
                DateTime = log.DateTime,
                Id = log.Id,
                ModifiedUserId = log.ModifiedUserId,
                NewValues = log.NewValues,
                OldValues = log.OldValues,
                PrimaryKey = log.PrimaryKey,
                TableName = log.TableName,
                Type = log.Type
            };

            return View(logItemVm);
        }
    }
}
