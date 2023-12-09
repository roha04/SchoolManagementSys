// <copyright file="LessonTimeService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.BLL
{
    using System.Collections.Generic;
    using System.Linq;
    using WPFScholifyApp.DAL.ClassRepository;
    using WPFScholifyApp.DAL.DBClasses;

    public class LessonTimeService
    {
        private GenericRepository<LessonTime> lessonTimeRepository;

        public LessonTimeService(GenericRepository<LessonTime> lessonTimeRepository)
        {
            this.lessonTimeRepository = lessonTimeRepository;
        }

        public List<LessonTime> GetAllLessonTimes()
        {
            return this.lessonTimeRepository.GetAll().ToList();
        }

        public LessonTime GetLessonTimeById(int id)
        {
            return this.lessonTimeRepository.GetAll().FirstOrDefault(x => x.Id == id) !;
        }
    }
}
