using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarsManagement.Models
{
    public partial class Categry
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Categry Name field is required")]
        public string CatName { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
