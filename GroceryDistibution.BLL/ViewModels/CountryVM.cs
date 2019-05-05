using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GroceryDistibution.BLL.ViewModels
{
    public class CountryVM
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public short CountryCode { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
