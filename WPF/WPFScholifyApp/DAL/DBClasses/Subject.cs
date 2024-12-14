﻿// <copyright file="Subject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.DAL.DBClasses
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Subject
    {
        [Key]
        public int Id { get; set; }

        public string? SubjectName { get; set; }

        [Required]
        public int ClassId { get; set; }

        [ForeignKey("ClassId")]
        public Class? Class { get; set; }

        public virtual ICollection<DayBook>? DayBooks { get; set; }

        public virtual ICollection<Teacher>? Teachers { get; set; }

        public virtual ICollection<Schedule>? Schedules { get; set; }
    }
}
