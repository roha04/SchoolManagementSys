// <copyright file="EnumsTime.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.DAL.ClassRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EnumsTime
    {
        private static readonly DateTime BaseDate = DateTime.MinValue.Date.ToUniversalTime();

        public static readonly DateTime T830 = BaseDate.Add(new TimeSpan(8, 30, 0)).ToUniversalTime();
        public static readonly DateTime T915 = BaseDate.Add(new TimeSpan(9, 15, 0)).ToUniversalTime();

        public static readonly DateTime T930 = BaseDate.Add(new TimeSpan(9, 30, 0)).ToUniversalTime();
        public static readonly DateTime T1015 = BaseDate.Add(new TimeSpan(10, 15, 0)).ToUniversalTime();

        public static readonly DateTime T1030 = BaseDate.Add(new TimeSpan(10, 30, 0)).ToUniversalTime();
        public static readonly DateTime T1115 = BaseDate.Add(new TimeSpan(11, 15, 0)).ToUniversalTime();

        public static readonly DateTime T1135 = BaseDate.Add(new TimeSpan(11, 35, 0)).ToUniversalTime();
        public static readonly DateTime T1220 = BaseDate.Add(new TimeSpan(12, 20, 0)).ToUniversalTime();

        public static readonly DateTime T1240 = BaseDate.Add(new TimeSpan(12, 40, 0)).ToUniversalTime();
        public static readonly DateTime T1325 = BaseDate.Add(new TimeSpan(13, 25, 0)).ToUniversalTime();

        public static readonly DateTime T1335 = BaseDate.Add(new TimeSpan(13, 35, 0)).ToUniversalTime();
        public static readonly DateTime T1420 = BaseDate.Add(new TimeSpan(14, 20, 0)).ToUniversalTime();

        public static readonly DateTime T1435 = BaseDate.Add(new TimeSpan(14, 35, 0)).ToUniversalTime();
        public static readonly DateTime T1520 = BaseDate.Add(new TimeSpan(15, 20, 0)).ToUniversalTime();

        public static readonly DateTime T1530 = BaseDate.Add(new TimeSpan(15, 30, 0)).ToUniversalTime();
        public static readonly DateTime T1615 = BaseDate.Add(new TimeSpan(16, 15, 0)).ToUniversalTime();
    }
}
