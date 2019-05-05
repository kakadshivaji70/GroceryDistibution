namespace GroceryDistibution.BLL.ViewModels
{
    public class UserTypeVM
    {
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
        public string Description { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
