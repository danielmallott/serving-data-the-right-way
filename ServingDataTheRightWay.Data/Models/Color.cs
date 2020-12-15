﻿using System;
using System.Collections.Generic;

namespace ServingDataTheRightWay.Data.Models
{
    public partial class Color
    {
        public Color()
        {
            StockItems = new HashSet<StockItem>();
        }

        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public int LastEditedBy { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public virtual Person LastEditedByNavigation { get; set; }
        public virtual ICollection<StockItem> StockItems { get; set; }
    }
}
