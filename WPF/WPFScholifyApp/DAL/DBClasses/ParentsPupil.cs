// <copyright file="ParentsPupil.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.DAL.DBClasses
{
    public class ParentsPupil
    {
        public int ParentId { get; set; }

        public Parents? Parent { get; set; }

        public int PupilId { get; set; }

        public Pupil? Pupil { get; set; }
    }
}
