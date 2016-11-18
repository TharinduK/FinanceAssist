using System.Collections.Generic;

namespace FinanceAssist.Domain
{
    public interface IExpenseRepository
    {
        List<Expense> GetAllExpenses();
    }
}