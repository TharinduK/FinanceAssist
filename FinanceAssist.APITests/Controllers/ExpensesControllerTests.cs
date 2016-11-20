using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using FinanceAssist.Domain;
using System.Web.Http.Results;
using AutoMapper;
using Moq;

namespace FinanceAssist.API.Controllers.Tests
{
    [TestClass()]
    public class ExpensesControllerTests
    {
        private Mock<IApplicationLogger> _logger;

        [TestInitialize]
        public void TestSetup()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Domain.Expense, API.ViewModels.Expense>()
            .ForMember(dest => dest.ExpneseDate, opt => opt.MapFrom(src => src.ExpneseDate.ToString("yyyy/M/d")))
            );

            _logger = new Moq.Mock<IApplicationLogger>();
            _logger.Setup(m => m.ErrorLog(Moq.It.IsAny<string>(), Moq.It.IsAny<Exception>()));
        }

        [TestMethod()]
        public void GetExpenses_oneValidExpense_Ok()
        {
            var repoExpense1 = new Domain.Expense
            {
                Amount = 100,
                Category = "grocery",
                ExpneseDate = new DateTime(2016, 11, 15),
                ID = 1,
                Merchant = "pns"
            };
            var repoExpenses = new List<Domain.Expense>() { repoExpense1 };
            var repo = new Moq.Mock<IExpenseRepository>();
            repo.Setup<List<Expense>>(t => t.GetAllExpenses())
                .Returns(repoExpenses);

            var fac = new Moq.Mock<ITransactionFactory>();
            fac.Setup<DisplayExpensesTransaction>(m => m.CreateDisplayExpensesTransaction())
                .Returns(new DisplayExpensesTransaction(repo.Object));

            var sut = new ExpensesController(fac.Object, _logger.Object);

            //act
            var actualResponse = sut.Get();
            var contentResult = actualResponse as OkNegotiatedContentResult<IEnumerable<ViewModels.Expense>>;

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

            var sut = new ExpensesController(fac.Object, _logger.Object);

            //act
            var actualResponse = sut.Get();
            var contentResult = actualResponse as OkNegotiatedContentResult<IEnumerable<ViewModels.Expense>>;

            //assert
            Assert.IsNotNull(contentResult, "Ok-200 status was not returned"+ actualResponse.GetType().Name);
            Assert.IsNotNull(contentResult.Content, "Empty content was not returned");
        }

        [TestMethod()]
        public void GetExpenses_repositoryException_BadData()
        {
            var repo = new Moq.Mock<IExpenseRepository>();
            repo.Setup<List<Domain.Expense>>(t => t.GetAllExpenses())
                .Throws(new Exception("Database Exception"));

            var fac = new Moq.Mock<ITransactionFactory>();
            fac.Setup<DisplayExpensesTransaction>(m => m.CreateDisplayExpensesTransaction())
                .Returns(new DisplayExpensesTransaction(repo.Object));

            var sut = new ExpensesController(fac.Object, _logger.Object);

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

            var sut = new ExpensesController(fac.Object, _logger.Object);

            //act
            var actualResponse = sut.Get();
            var contentResult = actualResponse as InternalServerErrorResult;

            //assert
            Assert.IsNotNull(contentResult, "Ok-500 status was not returned");
            _logger.Verify(m => m.ErrorLog(Moq.It.IsAny<string>(), Moq.It.IsAny<Exception>()), Moq.Times.Exactly(1));
                
        }
    }
}