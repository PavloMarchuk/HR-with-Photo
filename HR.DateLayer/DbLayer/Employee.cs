using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HR.DateLayer.DbLayer
{
    public partial class Employee
    {
        public Employee()
        {
            EmpPromotion = new HashSet<EmpPromotion>();
            //Photo = new HashSet<Photo>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy'/'MM'/'dd}", ApplyFormatInEditMode = true)]
        public DateTime DateBirthday { get; set; }
        public string Inn { get; set; }

        public ICollection<EmpPromotion> EmpPromotion { get; set; }
        public ICollection<Photo> Photo { get; set; }
    }
}
