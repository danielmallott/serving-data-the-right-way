﻿using System;
using System.Collections.Generic;

namespace ServingDataTheRightWay.Data.Models
{
    public partial class PurchaseOrderLine
    {
        public int PurchaseOrderLineId { get; set; }
        public int PurchaseOrderId { get; set; }
        public int StockItemId { get; set; }
        public int OrderedOuters { get; set; }
        public string Description { get; set; }
        public int ReceivedOuters { get; set; }
        public int PackageTypeId { get; set; }
        public decimal? ExpectedUnitPricePerOuter { get; set; }
        public DateTime? LastReceiptDate { get; set; }
        public bool IsOrderLineFinalized { get; set; }
        public int LastEditedBy { get; set; }
        public DateTime LastEditedWhen { get; set; }

        public virtual Person LastEditedByNavigation { get; set; }
        public virtual PackageType PackageType { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual StockItem StockItem { get; set; }
    }
}
