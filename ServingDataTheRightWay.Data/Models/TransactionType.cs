using System;
using System.Collections.Generic;

namespace ServingDataTheRightWay.Data.Models
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            CustomerTransactions = new HashSet<CustomerTransactions>();
            StockItemTransactions = new HashSet<StockItemTransaction>();
            SupplierTransactions = new HashSet<SupplierTransactions>();
        }

        public int TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; }
        public int LastEditedBy { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public virtual Person LastEditedByNavigation { get; set; }
        public virtual ICollection<CustomerTransactions> CustomerTransactions { get; set; }
        public virtual ICollection<StockItemTransaction> StockItemTransactions { get; set; }
        public virtual ICollection<SupplierTransactions> SupplierTransactions { get; set; }
    }
}
