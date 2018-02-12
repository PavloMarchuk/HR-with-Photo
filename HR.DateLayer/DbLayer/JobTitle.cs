using System;
using System.Collections.Generic;

namespace HR.DateLayer.DbLayer
{
    public partial class JobTitle
    {
        public JobTitle()
        {
            EmpPromotion = new HashSet<EmpPromotion>();
        }

        public int JobTitleId { get; set; }
        public string NameJobTitle { get; set; }

        public ICollection<EmpPromotion> EmpPromotion { get; set; }
    }
}
