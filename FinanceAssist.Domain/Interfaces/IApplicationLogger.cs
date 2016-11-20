using System;

namespace FinanceAssist.Domain
{
    public interface IApplicationLogger
    {
        void ErrorLog(string message, Exception ex);
    }
}