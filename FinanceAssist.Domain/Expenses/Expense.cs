using System;

namespace FinanceAssist.Domain
{
    public class Expense
    {
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateTime ExpneseDate { get; set; }
        public string Merchant { get; set; }
        public int ID { get; set; }
    }
}