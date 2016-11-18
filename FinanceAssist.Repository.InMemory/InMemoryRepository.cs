using FinanceAssist.Domain;
using System;
using System.Collections.Generic;

namespace FinanceAssist.Repository.InMemory
{
    public class InMemoryRepository : IExpenseRepository
    {
        private List<Expense> _expences;

        public InMemoryRepository()
        {
            SetupTenExpenses();
        }

        public List<Expense> GetAllExpenses()
        {
            return _expences;
        }

        private void SetupTenExpenses()
        {
            var expense1 = new Expense
            {
                Amount = 100,
                Category = "grocery",
                ExpneseDate = new DateTime(2016, 11, 15),
                ID = 1,
                Merchant = "pns"
            };
            var expense2 = new Expense
            {
                Amount = 10,
                Category = "grocery",
                ExpneseDate = new DateTime(2016, 10, 15),
                ID = 2,
                Merchant = "pns"
            };
            var expense3 = new Expense
            {
                Amount = 155.40m,
                Category = "grocery",
                ExpneseDate = new DateTime(2015, 11, 15),
                ID = 3,
                Merchant = "pns"
            };
            var expense4 = new Expense
            {
                Amount = 23.4m,
                Category = "toy",
                ExpneseDate = new DateTime(2016, 08, 15),
                ID = 4,
                Merchant = "toys are us"
            };
            var expense5 = new Expense
            {
                Amount = 11,
                Category = "toy",
                ExpneseDate = new DateTime(2016, 11, 15),
                ID = 5,
                Merchant = "wm"
            };
            var expense6 = new Expense
            {
                Amount = 10,
                Category = "grocery",
                ExpneseDate = new DateTime(2016, 11, 15),
                ID = 6,
                Merchant = "pns"
            };
            var expense7 = new Expense
            {
                Amount = 88,
                Category = "grocery",
                ExpneseDate = new DateTime(2016, 11, 08),
                ID = 7,
                Merchant = "wholefood"
            };
            var expense8 = new Expense
            {
                Amount = 1.55m,
                Category = "grocery",
                ExpneseDate = new DateTime(2016, 11, 14),
                ID = 8,
                Merchant = "pns"
            };
            var expense9 = new Expense
            {
                Amount = 250,
                Category = "health",
                ExpneseDate = new DateTime(2016, 9, 15),
                ID = 9,
                Merchant = "aurora"
            };
            var expense10 = new Expense
            {
                Amount = 100,
                Category = "grocery",
                ExpneseDate = new DateTime(2016, 11, 15),
                ID = 10,
                Merchant = "pns"
            };
            _expences = new List<Expense>() { expense1, expense2, expense3, expense4, expense5, expense6, expense7, expense8, expense9, expense10 };
        }

    }
}
