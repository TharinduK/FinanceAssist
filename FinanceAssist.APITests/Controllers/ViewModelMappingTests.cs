using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;

namespace FinanceAssist.APITests.Controllers
{
    [TestClass]
    public class ViewModelMappingTests
    {
        [TestInitialize]
        public void initialize()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Domain.Expense, API.ViewModels.Expense>()
             .ForMember(dest => dest.ExpneseDate, opt => opt.MapFrom(src => src.ExpneseDate.ToString("yyyy/M/d")))
            );
        }
        [TestMethod]
        public void automapper_validDomainModel_sucessfull()
        {
            var source = new Domain.Expense { Amount = 10.11m, Category = "Test Category", ExpneseDate = new DateTime(2016, 11, 17), ID = 55, Merchant = "Test Merchant" };
            var expected = new API.ViewModels.Expense { Amount = 10.11m, Category = "Test Category", ExpneseDate = "2016/11/17", Merchant = "Test Merchant" };

            //act
            var actualResults = Mapper.Map<Domain.Expense, API.ViewModels.Expense>(source);

            //assert
            Assert.AreEqual(expected.Amount, actualResults.Amount);
            Assert.AreEqual(expected.Category, actualResults.Category);
            Assert.AreEqual(expected.ExpneseDate, actualResults.ExpneseDate);
            Assert.AreEqual(expected.Merchant, actualResults.Merchant);
        }

        [TestMethod]
        public void automapper_validDomainModelList_sucessfull()
        {
            API.ViewModels.Expense[] expected = setupThreeExpenseVMList();
            Domain.Expense[] source = setupThreeExpenseDomainList();

            //act
            var actualResults = Mapper.Map<Domain.Expense[], API.ViewModels.Expense[]>(source);

            //assert
            Assert.AreEqual(expected.Length, actualResults.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i].Category, actualResults[i].Category);
                Assert.AreEqual(expected[i].ExpneseDate, actualResults[i].ExpneseDate);
                Assert.AreEqual(expected[i].Merchant, actualResults[i].Merchant);
            }
        }
        private static Domain.Expense[] setupThreeExpenseDomainList()
        {
            return new Domain.Expense[] {
                new Domain.Expense { Amount = 10.11m, Category = "Test Category", ExpneseDate = new DateTime(2016, 11, 17), ID = 55, Merchant = "Test Merchant" },
                new Domain.Expense { Amount = 100.15m, Category = "Test Category2", ExpneseDate = new DateTime(2016, 10, 17), ID = 56, Merchant = "Test Merchant2" },
                new Domain.Expense { Amount = .12m, Category = "Test Category3", ExpneseDate = new DateTime(2016, 9, 17), ID = 57, Merchant = "Test Merchant3" }
            };
        }

        private static API.ViewModels.Expense[] setupThreeExpenseVMList()
        {
            return new API.ViewModels.Expense[] {
                new API.ViewModels.Expense{ Amount = 10.11m, Category = "Test Category", ExpneseDate = "2016/11/17", Merchant = "Test Merchant" },
                new API.ViewModels.Expense{ Amount = 100.15m, Category = "Test Category2", ExpneseDate = "2016/10/17",  Merchant = "Test Merchant2" },
                new API.ViewModels.Expense{ Amount = .12m, Category = "Test Category3", ExpneseDate = "2016/9/17",  Merchant = "Test Merchant3" }
            };
        }

        [TestMethod]
        [ExpectedException(typeof(AutoMapperMappingException))]
        public void automapper_validDomainModelListReverse_fail()
        {
            API.ViewModels.Expense[] source = setupThreeExpenseVMList();
            Domain.Expense[] expected = setupThreeExpenseDomainList();

            //act
            //map in the other direction 
            var actualResults = Mapper.Map<API.ViewModels.Expense[], Domain.Expense[]>(source);

            //assert
            Assert.AreEqual(expected.Length, actualResults.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i].Category, actualResults[i].Category);
                Assert.AreEqual(expected[i].ExpneseDate, actualResults[i].ExpneseDate);
                Assert.AreEqual(expected[i].Merchant, actualResults[i].Merchant);
            }
        }

        //[TestMethod]
        //[ExpectedException(typeof(AutoMapperMappingException))]
        //public void automapper_dateFormatProjection_sucessfull()
        //{
        //    Mapper.Initialize(cfg => cfg.CreateMap<Domain.Expense, API.ViewModels.Expense>());

        //    Mapper.Initialize(cfg => cfg.CreateMap<Domain.Expense, API.ViewModels.Expense>());
        //    var source = new Domain.Expense[] {
        //        new Domain.Expense { Amount = 10.11m, Category = "Test Category", ExpneseDate = new DateTime(2016, 11, 17), ID = 55, Merchant = "Test Merchant" },
        //        new Domain.Expense { Amount = 100.15m, Category = "Test Category2", ExpneseDate = new DateTime(2016, 10, 17), ID = 56, Merchant = "Test Merchant2" },
        //        new Domain.Expense { Amount = .12m, Category = "Test Category3", ExpneseDate = new DateTime(2016, 9, 17), ID = 57, Merchant = "Test Merchant3" }
        //    };
        //    var expected = new API.ViewModels.Expense[] {
        //    new API.ViewModels.Expense{ Amount = 10.11m, Category = "Test Category", ExpneseDate = new DateTime(2016, 11, 17), Merchant = "Test Merchant" },
        //    new API.ViewModels.Expense{ Amount = 100.15m, Category = "Test Category2", ExpneseDate = new DateTime(2016, 10, 17),  Merchant = "Test Merchant2" },
        //    new API.ViewModels.Expense{ Amount = .12m, Category = "Test Category3", ExpneseDate = new DateTime(2016, 9, 17),  Merchant = "Test Merchant3" }
        //    };

        //    //act
        //    //map in the other direction 
        //    var actualResults = Mapper.Map<API.ViewModels.Expense[], Domain.Expense[]>(source);

        //    //assert
        //    Assert.AreEqual(expected.Length, actualResults.Length);
        //    for (int i = 0; i < expected.Length; i++)
        //    {
        //        Assert.AreEqual(expected[i].Category, actualResults[i].Category);
        //        Assert.AreEqual(expected[i].ExpneseDate, actualResults[i].ExpneseDate);
        //        Assert.AreEqual(expected[i].Merchant, actualResults[i].Merchant);
        //    }
        //}
    }
}
