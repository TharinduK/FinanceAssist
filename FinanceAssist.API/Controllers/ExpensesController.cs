using FinanceAssist.Domain;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace FinanceAssist.API.Controllers
{
    public class ExpensesController : ApiController
    {
        private readonly ITransactionFactory _factory;
        private readonly IApplicationLogger _logger;

        public ExpensesController(ITransactionFactory fact, IApplicationLogger log)
        {
            _factory = fact;
            _logger = log;
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var tran = _factory.CreateDisplayExpensesTransaction();
                tran.Execute();
                var expneses = AutoMapper.Mapper.Map<IEnumerable<ViewModels.Expense>>(tran.Expenses);
                return Ok(expneses);
            }
            catch(RepositoryException) { return BadRequest(); }

            catch (Exception ex)
            {
                _logger.ErrorLog($"Web API failed to get expenses", ex);

                return InternalServerError();
            }
        }

        //[HttpPost]
        //public IHttpActionResult Post([FromBody] ViewModels.Expense expenseToAdd)
        //{
        //    try
        //    {
        //        if (expenseToAdd == null) return BadRequest();

        //        var domainExpneseToAdd = AutoMapper.Mapper.Map<Domain.Expense>(expenseToAdd);
        //        var tran = _factory.CreateAddExpenseTransaction(expenseToAdd);
        //        tran.Execute();
        //        var expneseAdded = AutoMapper.Mapper.Map<ViewModels.Expense>(tran.ExpenseAdded);
        //        return CreatedAtRoute("DefaultRouting", new { id = tran.ExpenseAdded.Id }, expneseAdded);

        //        if (tran.Response.WasSucessfull)
        //        {
        //            expenseToAdd.Id = tran.Response.RestaurantId;
        //            expenseToAdd.CuisineName = tran.Response.CuisineName;
        //            return CreatedAtRoute("DefaultRouting", new { id = expenseToAdd.Id }, expenseToAdd);
        //        }
        //        else return BadRequest();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.ErrorLog($"Web API failed add new expense {expenseToAdd}", ex);
        //        return InternalServerError();
        //    }
        //}


    }
}
