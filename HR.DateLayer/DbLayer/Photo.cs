using System;
using System.Collections.Generic;

namespace HR.DateLayer.DbLayer
{
    public partial class Photo
    {
        public int PhotoId { get; set; }
        public int EmployeeId { get; set; }
        public string PhotoPath { get; set; }

        public Employee Employee { get; set; }
    }
}
