﻿using Dapper.Contrib.Extensions;

using System;
using System.Collections.Generic;
using System.Text;

namespace SantaShop.Core.TablesModels
{
    [Table("SantaShop.Presents")]
    public class Present
    {
        [Key]
        public long PresentID { get; set; }
        public string PresentName { get; set; }
        public int Stock { get; set; }
    }
}
