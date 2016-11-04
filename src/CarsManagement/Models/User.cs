using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarsManagement.Models
{
    public partial class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
