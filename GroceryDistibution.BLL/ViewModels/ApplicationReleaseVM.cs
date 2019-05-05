using GroceryDistibution.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GroceryDistibution.BLL.ViewModels
{
    public class ApplicationReleaseVM : BaseModel
    {
        public int ApplicationReleaseId { get; set; }
        public string ApplicationReleaseName { get; set; }
        public string Version { get; set; }
        public bool IsUpdateMadatory { get; set; }
        public string ApplicationLink { get; set; }
        public string RemoveSupportFrom { get; set; }
        public string ReleaseDate { get; set; }
        public string ReleaseNotes { get; set; }
        public string Message { get; set; }
        public new string AddedOn { get; set; }
        //public string AddedBy { get; set; }
        //public string ModifiedOn { get; set; }
        //public string ModifiedBy { get; set; }
        //public string IsDeleted { get; set; }
        public bool IsVersionPresent { get; set; }
    }
}
