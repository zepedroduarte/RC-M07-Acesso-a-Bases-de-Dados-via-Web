﻿using Dapper.Contrib.Extensions;

namespace SantaShop.Core.TablesModels
{

    [Table("SantaShop.Children")]
    public class Child
    {
        [Key]
        public long ChildID { get; set; }
        public string ChildName { get; set; }
        public int YearOfBirth { get; set; }
        public long? BehaviorID { get; set; }
        public long? PresentID { get; set; }
    }
}
