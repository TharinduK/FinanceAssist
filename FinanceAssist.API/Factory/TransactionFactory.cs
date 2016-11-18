using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinanceAssist.Domain;

namespace FinanceAssist.API
{
    //TODO: must change to abstract factory
    public class TransactionFactory : ITransactionFactory
    {
        private readonly IExpenseRepository _ExpenseRepo;

        public TransactionFactory(IExpenseRepository repo)
        {
            _ExpenseRepo = repo;
        }
        public DisplayExpensesTransaction CreateDisplayExpensesTransaction()
        {
            return new DisplayExpensesTransaction(_ExpenseRepo);
        }
    }
}