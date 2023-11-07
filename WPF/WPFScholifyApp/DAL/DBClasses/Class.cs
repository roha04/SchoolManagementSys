using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFScholifyApp.DAL.DBClasses
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassName { get; set; }

        public virtual Schedule Schedule { get; set; }
        public virtual ICollection<Pupil> Pupils { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<DayBook> DayBooks { get; set; }
    }
}
