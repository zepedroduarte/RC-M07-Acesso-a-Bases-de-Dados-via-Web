using Dapper.Contrib.Extensions;

namespace SantaShop.Cons.Models
{

    [Table("SantaShop.Children")]
    public class Child
    {
        [Key]
        public int ChildID { get; set; }
        public string ChildName { get; set; }
        public int YearOfBirth { get; set; }
        public int? BehaviorID { get; set; }
        public int? PresentID { get; set; }
    }
}
