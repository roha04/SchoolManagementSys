using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPFScholifyApp.DAL.DBClasses
{
    public class Pupil
    {
        [ForeignKey("User")]
        public int Id { get; set; }

        public virtual User User { get; set; }
        public virtual Class Class { get; set; }
        public virtual ICollection<Parents> Parents { get; set; }
    }
}
