using System.Collections.Generic;

namespace FinanceAssist.Domain
{
    public class DisplayExpensesTransaction : ITransaction
    {
        private readonly IExpenseRepository _expenseRepo;

        public IEnumerable<Expense> Expenses { get; private set; }

        public DisplayExpensesTransaction(IExpenseRepository repo)
        {
            _expenseRepo = repo;
        }
 
        public void Execute()
        {
            try
            {
                Expenses = _expenseRepo.GetAllExpenses();//TODO: Must update for user
            }
            catch (System.Exception)
            {
                //to do: log
                throw new RepositoryException("Unable to fetch all expenses");
            }
        }
    }
}
