using HR.DateLayer.DbLayer;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HR.WebUI.Models
{
    public class EmployeeFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? StartBirthday { get; set; }
        public DateTime? EndBirthday { get; set; }
        public Expression<Func<Employee, bool>> predicate
        {
            get
            {
                var predicate = PredicateBuilder.New<Employee>(true);
                if (!string.IsNullOrEmpty(FirstName))
                    predicate = predicate.And(p => p.FirstName.ToLower()
                    .Contains(FirstName.ToLower()));

                if (!string.IsNullOrEmpty(LastName))
                    predicate = predicate.And(p => p.LastName.ToLower()
                    .Contains(LastName.ToLower()));

                if (!string.IsNullOrEmpty(StartBirthday.ToString()) && !string.IsNullOrEmpty(EndBirthday.ToString()))
                {
                    predicate = predicate.And(p => p.DateBirthday >= StartBirthday && p.DateBirthday <= EndBirthday);
                }

                if (!string.IsNullOrEmpty(StartBirthday.ToString()))
                {
                    predicate = predicate.And(p => p.DateBirthday >= StartBirthday);
                }

                if (!string.IsNullOrEmpty(EndBirthday.ToString()))
                {
                    predicate = predicate.And(p => p.DateBirthday <= EndBirthday);
                }
                return predicate;
            }
        }
    }
}
