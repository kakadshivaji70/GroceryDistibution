using System;

namespace GroceryDistibution.Entity.Models
{
    public class BaseModel
    {
        public int AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public int? ModifiedBy { get; set; } = null;
        public DateTime? ModifiedOn { get; set; } = null;
        public bool? IsDeleted { get; set; } = null;
    }
}
