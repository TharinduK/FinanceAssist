using FinanceAssist.Domain;
using System;
using System.Web.Http;

namespace FinanceAssist.API.Controllers
{
    public class ExpensesController : ApiController
    {
        private readonly ITransactionFactory _factory;

        public ExpensesController(ITransactionFactory fact)
        {
            _factory = fact;
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var tran = _factory.CreateDisplayExpensesTransaction();
                tran.Execute();
                var expneses = AutoMapper.Mapper.Map<ViewModels.Expense[]>(tran.Expenses);
                return Ok(expneses);
            }
            catch(RepositoryException) { return BadRequest(); }

            catch (Exception ex)
            {
                //todo: log

                return InternalServerError();
            }
        }
    }
}
