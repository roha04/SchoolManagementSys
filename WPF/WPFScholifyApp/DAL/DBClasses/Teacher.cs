using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPFScholifyApp.DAL.DBClasses
{
    public class Teacher
    {
        [ForeignKey("User")]
        public int Id { get; set; }

        public virtual User User { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<DayBook> DayBooks { get; set; }
    }
}
