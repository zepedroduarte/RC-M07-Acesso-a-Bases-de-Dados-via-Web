using Dapper.Contrib.Extensions;

namespace SantaShop.Cons.Models
{
    [Table("SantaShop.Presents")]
    public class Present
    {
        [Key]
        public int PresentID { get; set; }
        public string PresentName { get; set; }
        public int Stock { get; set; }
    }
}
