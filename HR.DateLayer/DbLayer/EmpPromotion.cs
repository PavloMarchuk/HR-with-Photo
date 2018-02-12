using System;
using System.Collections.Generic;

namespace HR.DateLayer.DbLayer
{
    public partial class EmpPromotion
    {
        public int EmpPromotionId { get; set; }
        public int EmployeeId { get; set; }
        public int JobTitleId { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }

        public Employee Employee { get; set; }
        public JobTitle JobTitle { get; set; }
    }
}
