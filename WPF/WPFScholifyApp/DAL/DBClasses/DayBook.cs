using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFScholifyApp.DAL.DBClasses
{
    public class DayBook
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public string Attendance { get; set; }
        public DateTime Date { get; set; }

        public virtual Class Class { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Subject Subject { get; set; }

    }
}
