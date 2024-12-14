// <copyright file="ClassService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using WPFScholifyApp.DAL.ClassRepository;
    using WPFScholifyApp.DAL.DBClasses;

    public class ClassService
    {
        private GenericRepository<Class> classRepository;

        public ClassService(GenericRepository<Class> classRepository)
        {
            this.classRepository = classRepository;
        }

        public List<Class> GetAllClasses()
        {
            return this.classRepository.GetAll().ToList();
        }

        public Class GetClassBySubjectId(int subjectId)
        {
            return this.classRepository.GetAllq().Include(x => x.Subjects).Where(x => x.Subjects!.Select(y => y.Id).Contains(subjectId)).FirstOrDefault() !;
        }

        public Class GetClassByUserId(int userId)
        {
            return this.classRepository.GetAllq().Include(x => x.Pupils).Where(x => x.Pupils!.Select(y => y.UserId).Contains(userId)).FirstOrDefault() !;
        }

        public void Save(Class entity)
        {
            this.classRepository.Insert(entity);
            this.classRepository.Save();
        }

        public void Delete(int id)
        {
            this.classRepository.Delete(id);
            this.classRepository.Save();
        }
    }
}
