// <copyright file="Schedule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.DAL.DBClasses
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Schedule
    {
        [ForeignKey("Class")]
        public int Id { get; set; }

        public string? DayOfWeek { get; set; }

        public string? Timeslot { get; set; }

        public virtual Class? Class { get; set; }

        public virtual Teacher? Teacher { get; set; }

        public virtual ICollection<Subject>? Subjects { get; set; }
    }
}
