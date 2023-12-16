// <copyright file="SubjectService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.BLL
{
    using System.Collections.Generic;
    using System.Linq;
    using WPFScholifyApp.DAL.ClassRepository;
    using WPFScholifyApp.DAL.DBClasses;

    public class SubjectService
    {
        private GenericRepository<Subject> subjectRepository;
        private GenericRepository<Teacher> teacherRepository;

        public SubjectService(GenericRepository<Subject> subjectRepository, GenericRepository<Teacher> teacherRepository)
        {
            this.subjectRepository = subjectRepository;
            this.teacherRepository = teacherRepository;
        }

        public Subject GetSubjectById(int id)
        {
            return this.subjectRepository.GetAll().FirstOrDefault(x => x.Id == id) !;
        }

        public List<Subject> GetSubjectsByClassId(int classId)
        {
            return this.subjectRepository.GetAll().Where(x => x.ClassId == classId).ToList();
        }

        public List<Subject> GetAllSubjects()
        {
            var classes = this.subjectRepository?.GetAll().OrderBy(x => x.SubjectName).ToList();
            return classes!;
        }

        public void SaveSubject(Subject subject, Teacher teacher)
        {
            this.subjectRepository.Insert(subject);
            this.subjectRepository.Save();
            this.teacherRepository.Insert(teacher);
            this.teacherRepository.Save();
        }

        public void Delete(int id)
        {
            this.subjectRepository.Delete(id);
            this.subjectRepository.Save();
        }
    }
}
