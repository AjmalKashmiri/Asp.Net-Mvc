using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarsManagement.Models
{
    public partial class Products
    {
        [Key]
        public int Id { get; set; }
        public int? CatId { get; set; }
        public int? SubCatId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public string ProductPrice { get; set; }
        [Required(ErrorMessage ="Image is Required")]
        public string ProductImage { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
