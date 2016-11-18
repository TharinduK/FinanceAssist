using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceAssist.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace FinanceAssist.Domain.Tests
{
    [TestClass()]
    public class DisplayExpensesTransactionTests
    {
        [TestMethod()]
        public void DisplayAllExpenses_WithOneExpense_Sucess()
        {
            Moq.Mock<IExpenseRepository> repo = new Moq.Mock<IExpenseRepository>();
            List<Expense> expectedExpense = UpdateRepoToReturnOneExpense(repo);
            var sut = new DisplayExpensesTransaction(repo.Object);

            //act
            sut.Execute();
            var actulaExpenses = sut.Expenses;

            //assert
            AssertExpenses(expectedExpense, actulaExpenses);
        }

        private List<Expense> UpdateRepoToReturnOneExpense(Moq.Mock<IExpenseRepository> repo)
        {
            var expense1 = new Expense
            {
                Amount = 100,
                Category = "grocery",
                ExpneseDate = new DateTime(2016, 11, 15),
                ID = 1,
                Merchant = "pns"
            };
            var expectedExpense = new List<Expense>() { expense1 };
            repo.Setup<List<Expense>>(t => t.GetAllExpenses())
                .Returns(new List<Expense>() { expense1 });
            return expectedExpense;
        }

        private static void AssertExpenses(List<Expense> expectedExpense, IEnumerable<Expense> actulaExpenses)
        {
            var index = 0;
            foreach (var actualExp in actulaExpenses)
            {
                Assert.AreEqual(expectedExpense[index].Amount, actualExp.Amount, "Invalid Amount");
                Assert.AreEqual(expectedExpense[index].Category, actualExp.Category);
                Assert.AreEqual(expectedExpense[index].ExpneseDate, actualExp.ExpneseDate);
                Assert.AreEqual(expectedExpense[index].ID, actualExp.ID);
                Assert.AreEqual(expectedExpense[index].Merchant, actualExp.Merchant);

                index++;
            }

            Assert.AreEqual(expectedExpense.Count, index, "Unexpected Count");
        }

        [TestMethod()]
        public void DisplayAllExpenses_WithTenExpense_Sucess()
        {
            Moq.Mock<IExpenseRepository> repo = new Moq.Mock<IExpenseRepository>();
            List<Expense> expectedExpense = UpdateRepoToReturnTenExpenses(repo);
            var sut = new DisplayExpensesTransaction(repo.Object);

            //act
            sut.Execute();
            var actulaExpenses = sut.Expenses;

            //assert
            AssertExpenses(expectedExpense, actulaExpenses);
        }

        private List<Expense> UpdateRepoToReturnTenExpenses(Mock<IExpenseRepository> repo)
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
            var expectedExpense = new List<Expense>() { expense1, expense2, expense3, expense4, expense5, expense6, expense7, expense8, expense9, expense10 };
            repo.Setup<List<Expense>>(t => t.GetAllExpenses())
                .Returns(expectedExpense);
            return expectedExpense;
        }
    }
}