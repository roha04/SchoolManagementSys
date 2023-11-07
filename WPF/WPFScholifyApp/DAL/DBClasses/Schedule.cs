using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPFScholifyApp.DAL.DBClasses
{
    public class Schedule
    {
        [ForeignKey("Class")]
        public int Id { get; set; }
        public string DayOfWeek { get; set; }
        public string Timeslot { get; set; }

        public virtual Class Class { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
