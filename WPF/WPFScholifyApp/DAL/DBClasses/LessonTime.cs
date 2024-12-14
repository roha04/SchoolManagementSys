﻿// <copyright file="LessonTime.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.DAL.DBClasses
{
    using System;
    using System.Collections.Generic;

    public class LessonTime
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string? Start { get; set; }

        public string? End { get; set; }

        public virtual ICollection<Schedule>? Schedules { get; set; }
    }
}
