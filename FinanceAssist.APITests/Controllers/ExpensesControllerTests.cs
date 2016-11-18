using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceAssist.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceAssist.Domain;
using System.Web.Http.Results;

namespace FinanceAssist.API.Controllers.Tests
{
    [TestClass()]
    public class ExpensesControllerTests
    {
        [TestMethod()]
        public void GetExpenses_oneValidExpense_Ok()
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
            var repo = new Moq.Mock<IExpenseRepository>();
            repo.Setup<List<Expense>>(t => t.GetAllExpenses())
                .Returns(new List<Expense>() { expense1 });

            var fac = new Moq.Mock<ITransactionFactory>();
            fac.Setup<DisplayExpensesTransaction>(m => m.CreateDisplayExpensesTransaction())
                .Returns(new DisplayExpensesTransaction(repo.Object));

            var sut = new ExpensesController(fac.Object);

            //act
           var actualResponse = sut.Get();
            var contentResult = actualResponse as OkNegotiatedContentResult<IEnumerable<Expense>>;

            //assert
            Assert.IsNotNull(contentResult, "Ok-200 status was not returned");
            Assert.IsNotNull(contentResult.Content, "No content was returned");
        }
        [TestMethod()]
        public void GetExpenses_noValidExpense_Ok()
        {
            var repo = new Moq.Mock<IExpenseRepository>();
            repo.Setup<List<Expense>>(t => t.GetAllExpenses())
                .Returns(new List<Expense>());

            var fac = new Moq.Mock<ITransactionFactory>();
            fac.Setup<DisplayExpensesTransaction>(m => m.CreateDisplayExpensesTransaction())
                .Returns(new DisplayExpensesTransaction(repo.Object));

            var sut = new ExpensesController(fac.Object);

            //act
            var actualResponse = sut.Get();
            var contentResult = actualResponse as OkNegotiatedContentResult<IEnumerable<Expense>>;

            //assert
            Assert.IsNotNull(contentResult, "Ok-200 status was not returned");
            Assert.IsNotNull(contentResult.Content, "Empty content was not returned");
        }

        [TestMethod()]
        public void GetExpenses_repositoryException_BadData()
        {
            var repo = new Moq.Mock<IExpenseRepository>();
            repo.Setup<List<Expense>>(t => t.GetAllExpenses())
                .Throws(new Exception("Database Exception"));

            var fac = new Moq.Mock<ITransactionFactory>();
            fac.Setup<DisplayExpensesTransaction>(m => m.CreateDisplayExpensesTransaction())
                .Returns(new DisplayExpensesTransaction(repo.Object));

            var sut = new ExpensesController(fac.Object);

            //act
            var actualResponse = sut.Get();
            var contentResult = actualResponse as BadRequestResult;

            //assert
            Assert.IsNotNull(contentResult, "Ok-400 status was not returned");
        }

        [TestMethod()]
        public void GetExpenses_nullRepoObject_InternalServerError()
        {
            var fac = new Moq.Mock<ITransactionFactory>();
            fac.Setup<DisplayExpensesTransaction>(m => m.CreateDisplayExpensesTransaction())
                .Throws(new Exception("Server Exception"));

            var sut = new ExpensesController(fac.Object);

            //act
            var actualResponse = sut.Get();
            var contentResult = actualResponse as InternalServerErrorResult;

            //assert
            Assert.IsNotNull(contentResult, "Ok-500 status was not returned");
        }
    }
}