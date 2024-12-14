﻿// <copyright file="AdvertisementService.cs" company="PlaceholderCompany">
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

    public class AdvertisementService
    {
        private GenericRepository<Advertisement> advertisementRepository;
        private GenericRepository<Class> classRepository;
        private GenericRepository<Pupil> pupilRepository;

        public AdvertisementService(GenericRepository<Advertisement> advertisementRepository, GenericRepository<Class> classRepository, GenericRepository<Pupil> pupilRepository)
        {
            this.advertisementRepository = advertisementRepository;
            this.classRepository = classRepository;
            this.pupilRepository = pupilRepository;
        }

        public Advertisement AddAdvertisement(Advertisement advertisement)
        {
            this.advertisementRepository.Insert(advertisement);
            this.advertisementRepository.Save();
            return advertisement;
        }

        public Advertisement GetInfoByAdvertisement(string name, string description)
        {
            var advertisement = this.advertisementRepository.GetAll().FirstOrDefault(u => u.Name == name && u.Description == description);
            if (advertisement != null)
            {
                return advertisement;
            }
            else
            {
                throw new InvalidOperationException("Entity not found");
            }
        }

        public List<Advertisement> GetAllAdvertisementsForClassId(int classId)
        {
            var advertisementsForClass = this.advertisementRepository.GetAllq()
                .Include(a => a.Class)
                .Where(a => a.ClassId == classId).ToList();

            return advertisementsForClass!;
        }

        public List<Advertisement> GetAdvertisementsForClassId(int classId)
        {
            return this.advertisementRepository.GetAllq()
            .Include(x => x.Class!)
            .Where(x => x.Class!.Id == classId)
            .ToList();
        }

        public void DeletedAvertisementl(int userId)
        {
            this.advertisementRepository.Delete(userId);
            this.advertisementRepository.Save();
        }
    }
}
