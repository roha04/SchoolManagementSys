using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFScholifyApp.DAL.DBClasses
{
    public class Subject
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<DayBook> DayBooks { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
