using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Slider : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Designation {  get; set; }

        [Required]
        public string ? ImageUrl { get; set; }
        [NotMapped] 
        public IFormFile ? ImageFile { get; set; }
    }
}
