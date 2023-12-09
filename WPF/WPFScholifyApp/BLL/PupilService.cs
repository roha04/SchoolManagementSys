// <copyright file="PupilService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.BLL
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using WPFScholifyApp.DAL.ClassRepository;
    using WPFScholifyApp.DAL.DBClasses;

    public class PupilService
    {
        private GenericRepository<Pupil> pupilRepository;
        private GenericRepository<Schedule> scheduleRepository;

        public PupilService(GenericRepository<Pupil> pupilRepository, GenericRepository<Schedule> scheduleRepository)
        {
            this.pupilRepository = pupilRepository;
            this.scheduleRepository = scheduleRepository;
        }

        public List<Schedule> GetAllSchedules(int classId, int dayOfWeek)
        {
            return this.scheduleRepository.GetAllq()
                .Include(x => x.DayOfWeek)
                .Include(x => x.Class)
                .Include(x => x.Teacher)
                .Include(x => x.LessonTime)
                .Include(x => x.Subject)
                .Where(x => x.Class!.Id == classId && x.DayOfWeekId == dayOfWeek).ToList();
        }

        public List<Pupil> GetAllPupils()
        {
            return this.pupilRepository.GetAllq().Include(x => x.ParentsPupil) !.ThenInclude(x => x.Parent).ToList().ToList();
        }
    }
}
