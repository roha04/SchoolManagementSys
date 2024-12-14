// <copyright file="DayBookService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.BLL
{
    using System.Collections.Generic;
    using System.Linq;
    using WPFScholifyApp.DAL.ClassRepository;
    using WPFScholifyApp.DAL.DBClasses;

    public class DayBookService
    {
        private GenericRepository<DayBook> dayBookRepository;

        public DayBookService(GenericRepository<DayBook> dayOfWeekRepository)
        {
            this.dayBookRepository = dayOfWeekRepository;
        }

        public List<DayBook> GetAll()
        {
            return this.dayBookRepository.GetAll().ToList();
        }
    }
}
