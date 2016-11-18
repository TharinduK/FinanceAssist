using FinanceAssist.Domain;
using System;

namespace FinanceAssist.API
{
    public interface ITransactionFactory
    {
        DisplayExpensesTransaction CreateDisplayExpensesTransaction();
    }
}