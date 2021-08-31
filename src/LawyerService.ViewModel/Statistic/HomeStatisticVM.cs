using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerService.ViewModel.Statistic
{
    public class HomeStatisticVM
    {
        public int CountClients { get; set; }
        public int CountLawyers { get; set; }
        public List<StatisticSpecialization> ListSpecialization{ get; set; }
        public List<StatisticOrder> ListOrders{ get; set; }
    }

    public class StatisticSpecialization
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }

    public class StatisticOrder
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}
