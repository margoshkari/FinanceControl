using System;

namespace FinanceControl
{
    public class FinanceInfo
    {
        public int Count { get; set; }
        public DateTime Date { get; set; }
        public int CategoryID { get; set; }
        public int ManagementID { get; set; }
        public FinanceInfo(int count, DateTime dateTime, int categoryID, int managementID)
        {
            Count = count;
            Date = dateTime;
            CategoryID = categoryID;
            ManagementID = managementID;
        }
    }
}
