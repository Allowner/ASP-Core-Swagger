using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Service.Model
{
    public class UpdateStudentRequest
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Surname { get; set; }

        [Required]
        [Range(1, 11)]
        public int Form { get; set; }

        [Required]
        [Range(0, 10)]
        public double AverageMark { get; set; }
    }
}
