﻿// <copyright file="JournalService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.BLL
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using WPFScholifyApp.DAL.ClassRepository;
    using WPFScholifyApp.DAL.DBClasses;
    using DayOfWeek = WPFScholifyApp.DAL.DBClasses.DayOfWeek;

    public class JournalService
    {
        private GenericRepository<DayOfWeek> dayOfWeekRepository;
        private GenericRepository<DayBook> dayBookRepository;
        private GenericRepository<Subject> subjectRepository;
        private GenericRepository<Class> classRepository;
        private ScheduleService scheduleService;

        public JournalService(GenericRepository<DayBook> dayBookRepository, GenericRepository<Subject> subjectRepository, GenericRepository<Class> classRepository, GenericRepository<DayOfWeek> dayOfWeekRepository, ScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
            this.dayBookRepository = dayBookRepository;
            this.subjectRepository = subjectRepository;
            this.classRepository = classRepository;
            this.dayOfWeekRepository = dayOfWeekRepository;
        }

        public List<DayBook> GetDayBooks(int classId)
        {
            var result = this.dayBookRepository.GetAllq()
                .Include(x => x.Pupil)
                .ThenInclude(x => x!.User)
                .Include(x => x.Schedule)
                .ThenInclude(x => x!.Subject)
                .Include(x => x.Schedule)
                .ThenInclude(x => x!.DayOfWeek)
                .AsNoTracking().ToList();

            return result;
        }

        public List<DayBook> GetDayBooksForUserId(int userId)
        {
            var result = this.dayBookRepository.GetAllq()
                .Include(x => x.Pupil)
                .ThenInclude(x => x!.User)
                .Include(x => x.Schedule)
                .ThenInclude(x => x!.Subject)
                .Include(x => x.Schedule)
                .ThenInclude(x => x!.DayOfWeek)
                .Where(x => x.Pupil!.User!.Id == userId)
                .AsNoTracking().ToList();

            return result;
        }

        public void AddGrade(DayOfWeek dayOfWeek, DayBook dayBook, int subjectId)
        {
            if (this.dayOfWeekRepository.GetAll().FirstOrDefault(x => x.Date.Date.Equals(dayOfWeek!.Date.Date)) != null)
            {
                dayOfWeek = this.dayOfWeekRepository.GetAll().FirstOrDefault(x => x.Date.Date.Equals(dayOfWeek!.Date.Date)) !;
                var dayOfWeekId = dayOfWeek != null ? dayOfWeek!.Id : 0;
                var schedulesForDay = new List<Schedule>();
            }
            else
            {
                this.dayOfWeekRepository.Insert(dayOfWeek);
                this.dayOfWeekRepository.Save();
            }

            var subject = this.subjectRepository.GetAll().FirstOrDefault(x => x.Id == subjectId);

            var schedule = this.scheduleService.GetAllSchedulesForSubjectId(subjectId).FirstOrDefault(x => x.DayOfWeek!.Day!.Equals(dayOfWeek!.Day));

            dayBook.ScheduleId = schedule!.Id;

            this.dayBookRepository.Insert(dayBook);
            this.dayBookRepository.Save();
        }

        public void DeleteGrade(int gradeId)
        {
            this.dayBookRepository.Delete(gradeId);
            this.dayBookRepository.Save();
        }
    }
}
