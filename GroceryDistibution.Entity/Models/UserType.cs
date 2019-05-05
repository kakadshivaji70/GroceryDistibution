using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryDistibution.Entity.Models
{
    [Table("UserType")]
    public class UserType
    {
        [Key]
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
        public string Description { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
