using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarsManagement.Models
{
    public partial class SubCategry
    {
        [Key]
        public int Id { get; set; }
        
        public int CatId { get; set; }
        [Required(ErrorMessage = "The Sub-Categry Name field is required.")]
        public string SubCatName { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
