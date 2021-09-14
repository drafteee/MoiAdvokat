using FastReport;
using FastReport.Data;
using FastReport.Web;
using LawyerService.BL.Helpers;
using LawyerService.BL.Interfaces;
using LawyerService.BL.Interfaces.Reports;
using LawyerService.BL.Reports.Datasets.Admin;
using LawyerService.DataAccess;
using LawyerService.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Threading.Tasks;

namespace LawyerService.BL.Reports
{
    public class ReportManager: IReportManager
    {
        private readonly LawyerDbContext _context;
        private readonly ILocalizationManager _localizationManager;
        private readonly UserManager<User> _userManager;
        private readonly JwtGenerator _jwtGenerator;

        public ReportManager(LawyerDbContext context, ILocalizationManager localizationManager, UserManager<User> userManager, JwtGenerator jwtGenerator)
        {
            _context = context;
            _localizationManager = localizationManager;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<WebReport> CreateReportAdminAllAsync(string report, int year)
        {
            var webReport = new WebReport();
            var dataSet = new DataSet();
            switch (report)
            {
                case "1":
                    webReport.Report.Load("Frx/Admin/Report_All.frx");
                    dataSet = await DataSetAdmin.GetDataSetAllAsync(_context, year);
                    webReport.Report.SetParameterValue("YearParameter", year.ToString());
                    webReport.Report.RegisterData(dataSet, "DnReport");
                    foreach (DataSourceBase source in webReport.Report.Dictionary.DataSources)
                        source.Enabled = true;
                    for (var i = 1; i <= 1; i++)
                    {
                        var valueDataSource = webReport.Report.GetDataSource("MainTable");
                        if (webReport.Report.FindObject("Data" + i) is DataBand dataBand) dataBand.DataSource = valueDataSource;
                    }
                    break;
            }
            return webReport;
        }
    }
}
