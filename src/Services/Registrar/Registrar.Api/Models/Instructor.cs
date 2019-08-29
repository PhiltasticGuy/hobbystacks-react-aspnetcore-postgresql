using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Registrar.Api.Data
{
    public class Instructor
    {
        [Key]
        public int InstructorId { get; set; }

        //[ForeignKey("CourseForeignKey")]
        //public int CourseId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string TitleEn { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
