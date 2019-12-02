using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Conference.Areas.Admin.Models
{
    public class EditionViewModel
    {
       
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage="Edition name must be at leat 3 characters long")]
        public string Name { get; set; }
        [Required]
        public string TagLine { get; set; }
        [Required]
        [Range(2010,2019)]
       
        public int Year { get; set; }
        [Required]
        public bool Active { get; set; }

    }
}
