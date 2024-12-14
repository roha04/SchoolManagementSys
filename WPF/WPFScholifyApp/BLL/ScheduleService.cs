// <copyright file="ScheduleService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.BLL
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using WPFScholifyApp.DAL.ClassRepository;
    using WPFScholifyApp.DAL.DBClasses;

    public class ScheduleService
    {
        private GenericRepository<Schedule> scheduleRepository;

        public ScheduleService(GenericRepository<Schedule> scheduleRepository)
        {
            this.scheduleRepository = scheduleRepository;
        }

        public List<Schedule> GetAllSchedulesForSubjectId(int subjectId)
        {
            return this.scheduleRepository.GetAllq()
                .Include(x => x.Subject)
                .Include(x => x.DayOfWeek)
                .Include(x => x.Class)
                .Include(x => x.LessonTime)
                .Where(x => x.Subject!.Id == subjectId).ToList();
        }

        public List<DayOfWeek?> GetDatesBySubjectId(int subjectId)
        {
            return this.scheduleRepository.GetAllq().AsNoTracking().Include(x => x.DayOfWeek).Where(x => x.SubjectId == subjectId).Select(x => x.DayOfWeek).Distinct().ToList();
        }

        public List<DayOfWeek?> GetDatesByClassId(int classId)
        {
            return this.scheduleRepository.GetAllq().AsNoTracking().Include(x => x.Class).Where(x => x.ClassId == classId).Select(x => x.DayOfWeek).Distinct().ToList();
        }

        public void Save(Schedule schedule)
        {
            this.scheduleRepository.Insert(schedule);
            this.scheduleRepository.Save();
        }

        public void Delete(int schedule)
        {
            this.scheduleRepository.Delete(schedule);
            this.scheduleRepository.Save();
        }
    }
}
