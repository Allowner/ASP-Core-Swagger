using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class StudentEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Form { get; set; }
        public double AverageMark { get; set; }
    }
}
