using LawyerService.BL.Reports.Service;
using LawyerService.DataAccess;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LawyerService.BL.Reports.Datasets.Admin
{
    public class DataSetAdmin
    {
        public static async Task<DataSet> GetDataSetAllAsync(LawyerDbContext dbContext, int year)
        {
            var dataSet = new DataSet();

            var mainTable = new DataTable("MainTable");
            mainTable.Columns.Add("V01", typeof(int));      
            mainTable.Columns.Add("V02", typeof(int));      
            mainTable.Columns.Add("V03", typeof(int));      
            mainTable.Columns.Add("V04", typeof(int));      
            mainTable.Columns.Add("V05", typeof(int));       
            mainTable.Columns.Add("V06", typeof(int));      
            mainTable.Columns.Add("V07", typeof(int));     
            mainTable.Columns.Add("V08", typeof(int));    
            mainTable.Columns.Add("V09", typeof(int));     
            mainTable.Columns.Add("V10", typeof(int));        
            mainTable.Columns.Add("V11", typeof(int));        
            mainTable.Columns.Add("V12", typeof(int));     


            mainTable.Columns.Add("QuarterId", typeof(long));       // квартал
            mainTable.Columns.Add("MonthId", typeof(long));         // месяц

            var refTableQuarter = new DataTable("RefTableQuarter"); // кварталы
            refTableQuarter.Columns.Add("QuarterId", typeof(long));
            refTableQuarter.Columns.Add("Name", typeof(string));

            var refTableMonth = new DataTable("RefTableMonth");     // месяцы
            refTableMonth.Columns.Add("QuarterId", typeof(long));
            refTableMonth.Columns.Add("MonthId", typeof(long));
            refTableMonth.Columns.Add("Name", typeof(string));

            dataSet.Tables.Add(mainTable);
            dataSet.Tables.Add(refTableQuarter);
            dataSet.Tables.Add(refTableMonth);

            dataSet.Relations.Add("RelQuarterMainTable", dataSet.Tables["RefTableQuarter"].Columns["QuarterId"],
                                  dataSet.Tables["MainTable"].Columns["QuarterId"]);
            dataSet.Relations.Add("RelMonthMainTable",
                                  new DataColumn[] { dataSet.Tables["RefTableMonth"].Columns["QuarterId"], dataSet.Tables["RefTableMonth"].Columns["MonthId"] },
                                  new DataColumn[] { dataSet.Tables["MainTable"].Columns["QuarterId"], dataSet.Tables["MainTable"].Columns["MonthId"] });

            var months = ReportService.GetQuarterMonths(new Dictionary<long, string>() { { 1, "Итого за I квартал" }, { 2, "Итого за II квартал" }, { 3, "Итого за III квартал" }, { 4, "Итого за IV квартал" }, });

            var quartersTables = months.Select(r => new { r.Quarter, r.QuarterName }).OrderBy(r => r.Quarter).Distinct().ToList();

            DataRow row;
            foreach (var quarter in quartersTables)
            {
                row = refTableQuarter.NewRow();
                row["QuarterId"] = quarter.Quarter;
                row["Name"] = quarter.QuarterName;
                refTableQuarter.Rows.Add(row);
            }

            var monthTables = months.OrderBy(r => r.Quarter).ThenBy(r => r.Month).Distinct().ToList();

            foreach (var month in monthTables)
            {
                row = refTableMonth.NewRow();
                row["QuarterId"] = month.Quarter;
                row["MonthId"] = month.Month;
                row["Name"] = month.MonthName;
                refTableMonth.Rows.Add(row);
            }

            //var results = new List<ReportData>() 
            //{ 
            //    new ReportData() { Quarter = 1, Month = 1, V01 = 1, V02 = 2, V03 = 3, V04 = 4, V05 = 5, V06 = 6, V07 = 7, V08 = 8, V09 = 9, V10 = 10, V11 = 11, V12 = 12, },
            //    new ReportData() { Quarter = 1, Month = 2, V01 = 1, V02 = 2, V03 = 3, V04 = 4, V05 = 5, V06 = 6, V07 = 7, V08 = 8, V09 = 9, V10 = 10, V11 = 11, V12 = 12, },
            //    new ReportData() { Quarter = 2, Month = 5, V01 = 1, V02 = 2, V03 = 3, V04 = 4, V05 = 5, V06 = 6, V07 = 7, V08 = 8, V09 = 9, V10 = 10, V11 = 11, V12 = 12, },
            //    new ReportData() { Quarter = 3, Month = 7, V01 = 1, V02 = 2, V03 = 3, V04 = 4, V05 = 5, V06 = 6, V07 = 7, V08 = 8, V09 = 9, V10 = 10, V11 = 11, V12 = 12, },
            //    new ReportData() { Quarter = 4, Month = 12, V01 = 1, V02 = 2, V03 = 3, V04 = 4, V05 = 5, V06 = 6, V07 = 7, V08 = 8, V09 = 9, V10 = 10, V11 = 11, V12 = 12, }
            //};

            var results = await ReportService.GetDataReportAllTableAsync(dbContext, year);

            foreach (var month in monthTables)
            {
                var data = results.Find(r => r.Quarter == month.Quarter && r.Month == month.Month);

                row = mainTable.NewRow();
                row["QuarterId"] = month.Quarter;
                row["MonthId"] = month.Month;
                row["V01"] = data?.V01 ?? 0;
                row["V02"] = data?.V02 ?? 0; // 
                row["V03"] = data?.V03 ?? 0; // 
                row["V04"] = data?.V04 ?? 0; // 
                row["V05"] = data?.V05 ?? 0; // 
                row["V06"] = data?.V06 ?? 0; // 
                row["V07"] = data?.V07 ?? 0; //
                row["V08"] = data?.V08 ?? 0; // 
                row["V09"] = data?.V09 ?? 0; //
                row["V10"] = data?.V10 ?? 0; //
                row["V11"] = data?.V11 ?? 0; // 
                row["V12"] = data?.V12 ?? 0; //
                mainTable.Rows.Add(row);
            }

            return dataSet;
        }
    }
}
