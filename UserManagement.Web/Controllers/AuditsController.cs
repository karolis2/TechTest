using System.Linq;
using UserManagement.Services.Interfaces;
using UserManagement.Web.Models.AuditLogs;

namespace UserManagement.Web.Controllers
{
    public class AuditsController : Controller
    {
        private readonly IAuditLogsService _auditLogsService;
        public AuditsController(IAuditLogsService auditLogsService) => _auditLogsService = auditLogsService;

        // GET: Audits
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
    }
}
