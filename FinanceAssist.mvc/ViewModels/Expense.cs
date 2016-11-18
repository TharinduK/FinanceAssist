using System;

namespace FinanceAssist.mvc.ViewModels
{
    public class Expense
    {
        public decimal Amount { get; set; }

        public string Category
        {
            get
            {
                return category;
            }

            set
            {
                category = value;
            }
        }

        public string ExpneseDate
        {
            get
            {
                return expneseDate;
            }

            set
            {
                expneseDate = value;
            }
        }

        public string Merchant
        {
            get
            {
                return merchant;
            }

            set
            {
                merchant = value;
            }
        }

        public int Id { get; internal set; }
        public string UserID { get; internal set; }

        private string category;
        private string expneseDate;
        private string merchant;
    }
}