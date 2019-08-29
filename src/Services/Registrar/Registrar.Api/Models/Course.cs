using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Registrar.Api.Data
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public string ReferenceCode { get; set; }

        [Required]
        public Subject Subject { get; set; }

        [Required]
        public string TitleEn { get; set; }

        public string DescriptionEn { get; set; }

        public Instructor Instructor { get; set; }
    }
}
