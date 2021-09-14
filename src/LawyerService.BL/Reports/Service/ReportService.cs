using LawyerService.BL.Enums.Country;
using LawyerService.BL.Enums.TransactionStatus;
using LawyerService.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerService.BL.Reports.Service
{
    public static class ReportService
    {

        public static async Task<List<ReportData>> GetDataReportAllTableAsync(LawyerDbContext _context, int year)
        {
            var query1 = _context.Users
                .Include(u => u.Address)
                    .ThenInclude(u => u.AdministrativeTerritory)
                .Where(u => u.CreateDate.Year == year)
                .GroupBy(u => new { u.Address.AdministrativeTerritory.CountryId, u.CreateDate.Month })
                .Select(u => new ReportData()
                {
                    Month = u.Key.Month,
                    Quarter = ((u.Key.Month - 1) / 3) + 1,
                    V01 = u.Sum(user => u.Key.CountryId == (int)Countries.Belarus ? 1 : 0),
                    V02 = u.Sum(user => u.Key.CountryId == (int)Countries.Russia ? 1 : 0),
                    V03 = u.Sum(user => u.Key.CountryId == (int)Countries.Kazakhstan ? 1 : 0),
                    V04 = default,
                    V05 = default,
                    V06 = default,
                    V07 = default,
                    V08 = default,
                    V09 = default,
                    V10 = default,
                    V11 = default,
                    V12 = default,
                });
            var query2 = _context.Orders
                .Include(o => o.User)
                    .ThenInclude(u => u.Address)
                        .ThenInclude(u => u.AdministrativeTerritory)
                .Where(o => o.StartDate.Year == year)
                .GroupBy(o => new { o.User.Address.AdministrativeTerritory.CountryId, o.StartDate.Month })
                .Select(u => new ReportData()
                {
                    Month = u.Key.Month,
                    Quarter = ((u.Key.Month - 1) / 3) + 1,
                    V01 = default,
                    V02 = default,
                    V03 = default,
                    V04 = u.Sum(user => u.Key.CountryId == (int)Countries.Belarus ? 1 : 0),
                    V05 = u.Sum(user => u.Key.CountryId == (int)Countries.Russia ? 1 : 0),
                    V06 = u.Sum(user => u.Key.CountryId == (int)Countries.Kazakhstan ? 1 : 0),
                    V07 = default,
                    V08 = default,
                    V09 = default,
                    V10 = default,
                    V11 = default,
                    V12 = default,
                });
            var query3 = _context.HistoryTransactions
                .Include(ht => ht.Status)
                .Include(ht => ht.User)
                    .ThenInclude(u => u.Address)
                        .ThenInclude(u => u.AdministrativeTerritory)
                .Where(ht => ht.Date.Year == year && ht.IsInService && ht.Status.Code == ((int)TransactionStatusEnum.IsSuccessful).ToString())
                .GroupBy(ht => new { ht.User.Address.AdministrativeTerritory.CountryId, ht.Date.Month })
                .Select(ht => new ReportData()
                {
                    Month = ht.Key.Month,
                    Quarter = ((ht.Key.Month - 1) / 3) + 1,
                    V01 = default,
                    V02 = default,
                    V03 = default,
                    V04 = default,
                    V05 = default,
                    V06 = default,
                    V07 = ht.Sum(tr => ht.Key.CountryId == (int)Countries.Belarus ? tr.Amount : 0),
                    V08 = ht.Sum(tr => ht.Key.CountryId == (int)Countries.Russia ? tr.Amount : 0),
                    V09 = ht.Sum(tr => ht.Key.CountryId == (int)Countries.Kazakhstan ? tr.Amount : 0),
                    V10 = default,
                    V11 = default,
                    V12 = default,
                });
            var query4 = _context.HistoryUserTransactions
                .Include(ht => ht.Reason)
                .Include(ht => ht.User)
                    .ThenInclude(u => u.Address)
                        .ThenInclude(u => u.AdministrativeTerritory)
                .Where(ht => ht.Date.Year == year && ht.Reason.Code == ((int)TransactionReasonEnum.PayOrder).ToString())
                .GroupBy(ht => new { ht.User.Address.AdministrativeTerritory.CountryId, ht.Date.Month })
                .Select(ht => new ReportData()
                {
                    Month = ht.Key.Month,
                    Quarter = ((ht.Key.Month - 1) / 3) + 1,
                    V01 = default,
                    V02 = default,
                    V03 = default,
                    V04 = default,
                    V05 = default,
                    V06 = default,
                    V07 = default,
                    V08 = default,
                    V09 = default,
                    V10 = ht.Sum(tr => ht.Key.CountryId == (int)Countries.Belarus ? tr.Amount : 0),
                    V11 = ht.Sum(tr => ht.Key.CountryId == (int)Countries.Russia ? tr.Amount : 0),
                    V12 = ht.Sum(tr => ht.Key.CountryId == (int)Countries.Kazakhstan ? tr.Amount : 0),
                });
            var result = query1.Concat(query2).Concat(query3).Concat(query4)
                .GroupBy(u => new { u.Quarter, u.Month })
                .Select(u => new ReportData()
                {
                    Quarter = u.Key.Quarter,
                    Month = u.Key.Month,
                    V01 = u.Sum(ur => ur.V01),
                    V02 = u.Sum(ur => ur.V02),
                    V03 = u.Sum(ur => ur.V03),
                    V04 = u.Sum(ur => ur.V04),
                    V05 = u.Sum(ur => ur.V05),
                    V06 = u.Sum(ur => ur.V06),
                    V07 = u.Sum(ur => ur.V07),
                    V08 = u.Sum(ur => ur.V08),
                    V09 = u.Sum(ur => ur.V09),
                    V10 = u.Sum(ur => ur.V10),
                    V11 = u.Sum(ur => ur.V11),
                    V12 = u.Sum(ur => ur.V12),
                }).ToList();
            return result;
        }
        

        public static List<QuarterMonth> GetQuarterMonths(Dictionary<long, string> quarters)
        {
            var monthsNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.Where(m => !string.IsNullOrEmpty(m)).ToList();
            List<QuarterMonth> months = new();
            for (int month = 0; month < 12; month++)
            {
                months.Add(new QuarterMonth
                {
                    Quarter = (month / 3) + 1,
                    QuarterName = $"{quarters[(month / 3) + 1]}",
                    Month = month + 1,
                    MonthName = monthsNames[month]
                });
            }
            return months;
        }

        public class QuarterMonth
        {
            /// <summary>
            /// Номер квартала
            /// </summary>
            public long Quarter { get; set; }

            /// <summary>
            /// Название квартала
            /// </summary>
            public string QuarterName { get; set; }

            /// <summary>
            /// Номер месяца
            /// </summary>
            public long Month { get; set; }

            /// <summary>
            /// Название месяца
            /// </summary>
            public string MonthName { get; set; }
        }

        public class ReportData
        {
            /// <summary>
            /// Номер квартала
            /// </summary>
            public long Quarter { get; set; }

            /// <summary>
            /// Номер месяца
            /// </summary>
            public long Month { get; set; }

            public int V01 { get; set; }
            public int V02 { get; set; }
            public int V03 { get; set; }
            public int V04 { get; set; }
            public int V05 { get; set; }
            public int V06 { get; set; }
            public decimal V07 { get; set; }
            public decimal V08 { get; set; }
            public decimal V09 { get; set; }
            public decimal V10 { get; set; }
            public decimal V11 { get; set; }
            public decimal V12 { get; set; }
            public int V13 { get; set; }
            public int V14 { get; set; }
            public int V15 { get; set; }
            public int V16 { get; set; }
            public int V17 { get; set; }
            public int V18 { get; set; }
            public int V19 { get; set; }
            public int V20 { get; set; }

        }
    }
}
