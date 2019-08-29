using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Registrar.Api.Data
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }

        //[ForeignKey("CourseForeignKey")]
        //public int CourseId { get; set; }

        [Required]
        public string NameEn { get; set; }
    }
}
