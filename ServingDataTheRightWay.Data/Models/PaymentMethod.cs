using System;
using System.Collections.Generic;

namespace ServingDataTheRightWay.Data.Models
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            CustomerTransactions = new HashSet<CustomerTransactions>();
            SupplierTransactions = new HashSet<SupplierTransactions>();
        }

        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public int LastEditedBy { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public virtual Person LastEditedByNavigation { get; set; }
        public virtual ICollection<CustomerTransactions> CustomerTransactions { get; set; }
        public virtual ICollection<SupplierTransactions> SupplierTransactions { get; set; }
    }
}
