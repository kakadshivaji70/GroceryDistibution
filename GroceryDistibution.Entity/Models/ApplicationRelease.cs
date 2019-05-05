using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GroceryDistibution.Entity.Models
{
    [Table("ApplicationRelease")]
    public class ApplicationRelease : BaseModel
    {
        [Key]
        public int ApplicationReleaseId { get; set; }
        public string ApplicationReleaseName { get; set; }
        public string Version { get; set; }
        public bool IsUpdateMadatory { get; set; }
        public string ApplicationLink { get; set; }
        public string RemoveSupportFrom { get; set; }
        public string ReleaseDate { get; set; }
        public string ReleaseNotes { get; set; }
        public string Message { get; set; }
    }
}
