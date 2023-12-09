// <copyright file="DayOfWeekService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.BLL
{
    using System.Collections.Generic;
    using System.Linq;
    using WPFScholifyApp.DAL.ClassRepository;
    using DayOfWeek = WPFScholifyApp.DAL.DBClasses.DayOfWeek;

    public class DayOfWeekService
    {
        private GenericRepository<DayOfWeek> dayOfWeekRepository;

        public DayOfWeekService(GenericRepository<DayOfWeek> dayOfWeekRepository)
        {
            this.dayOfWeekRepository = dayOfWeekRepository;
        }

        public List<DayOfWeek> GetAll()
        {
            return this.dayOfWeekRepository.GetAll().ToList();
        }

        public void Save(DayOfWeek dayOfWeek)
        {
            this.dayOfWeekRepository.Insert(dayOfWeek);
            this.dayOfWeekRepository.Save();
        }
    }
}
