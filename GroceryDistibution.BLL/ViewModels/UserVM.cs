using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryDistibution.BLL.ViewModels
{
    public class UserVM
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int UserTypeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public short CountryCode { get; set; }
        public string MobileNumber { get; set; }
        public string AltMobileNumber { get; set; }
        public string ShopName { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public int? Pincode { get; set; }
        public string UserPhoto { get; set; }
        public string UserSign { get; set; }
        public DateTime AddedOn { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordHashNew { get; set; }
        public string SaltValue { get; set; }
        public string FCMToken { get; set; }
        public bool IsUserNameExists { get; set; }
        public bool IsPasswordMatch { get; set; }
        public bool IsObjectChanged { get; set; } = false;
    }
}
