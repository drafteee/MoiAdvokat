using FastReport.Web;
using System.Threading.Tasks;

namespace LawyerService.BL.Interfaces.Reports
{
    public interface IReportManager
    {
        Task<WebReport> CreateReportAdminAllAsync(string report, int year);
    }
}
