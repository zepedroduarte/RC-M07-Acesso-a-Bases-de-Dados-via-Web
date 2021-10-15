using Dapper.Contrib.Extensions;

namespace SantaShop.ConsV2.Models
{
    [Table("SantaShop.Behaviors")]
    public class Behavior
    {
     
        [Key]
        public long BehaviorID { get; set; }
        public string BehaviorDescription { get; set; }
        public bool IsEligibleForPresent { get; set; }

    }
}
