using FinanceAssist.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceAssist.API
{
    internal class ApplicationLogger : IApplicationLogger
    {
        public void ErrorLog(string message, Exception ex)
        {
            //todo: define a logger and use it to log
        }
    }
}