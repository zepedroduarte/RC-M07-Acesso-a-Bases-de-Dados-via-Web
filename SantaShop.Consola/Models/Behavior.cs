using Dapper.Contrib.Extensions;

namespace SantaShop.Cons.Models
{
    [Table("SantaShop.Behaviors")]
    public class Behavior
    {
     
        [Key]
        public int BehaviorID { get; set; }
        public string BehaviorDescription { get; set; }
        public bool IsEligibleForPresent { get; set; }

    }
}
