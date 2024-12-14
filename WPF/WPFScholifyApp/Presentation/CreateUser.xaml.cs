﻿// <copyright file="CreateUser.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.Presentation
{
    using System;
    using System.Windows;
    using WPFScholifyApp.BLL;
    using WPFScholifyApp.DAL.DBClasses;

    /// <summary>
    /// Interaction logic for CreateUser.xaml.
    /// </summary>
    public partial class CreateUser : Window
    {
        private AdminService adminService;
        private AdvertisementService advertisementService;
        private JournalService journalService;
        private ParentsService parentsService;
        private PupilService pupilService;
        private UserService userService;
        private ScheduleService scheduleService;
        private TeacherService teacherService;
        private WindowService windowService;
        private MainWindow mainWindow;

        public int ClassId { get; set; }

        public string TestFirstName { get; set; } = string.Empty;

        public CreateUser(
            AdminService adminService,
            AdvertisementService advertisementService,
            JournalService journalService,
            ParentsService parentsService,
            PupilService pupilService,
            UserService userService,
            ScheduleService scheduleService,
            TeacherService teacherService,
            WindowService windowService,
            MainWindow mainWindow)
        {
            this.adminService = adminService;
            this.advertisementService = advertisementService;
            this.journalService = journalService;
            this.parentsService = parentsService;
            this.pupilService = pupilService;
            this.userService = userService;
            this.scheduleService = scheduleService;
            this.teacherService = teacherService;
            this.windowService = windowService;
            this.mainWindow = mainWindow;

            this.InitializeComponent();
            this.InitializeComponent();
        }

        private void SaveUser(object sender, RoutedEventArgs e)
        {
            string email = this.Email.Text;
            string password = this.Password.Text;
            string firstName = this.FirstName.Text;
            string middleName = this.MiddleName.Text;
            string lastName = this.LastName.Text;
            string gender = this.Gender.Text;
            DateTime birthday = this.Birthday.DisplayDate.ToUniversalTime();
            string adress = this.Adress.Text;
            string phoneNumber = this.PhoneNumber.Text;

            var user = new User
            {
                Id = this.adminService.GetNewUserId(),
                Email = email,
                Password = password,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Gender = gender,
                Birthday = birthday,
                Address = adress,
                PhoneNumber = phoneNumber,
                Role = "учень",
            };

            var pupil = new Pupil
            {
                Id = this.adminService.GetNewPupilId(),
                ClassId = this.ClassId,
                UserId = user.Id,
            };

            this.userService.AddUser(user, pupil);
            this.Hide();

            this.windowService.Show<AdminWindow>(window =>
            {
                window.DeleteFromAdminPanels();
                window.ShowAllClasses();
                window.ShowAllPupilsForClassId(this.ClassId);
                window.UpdateAdminPanels();
            });

            this.Email.Text = string.Empty;
            this.Password.Text = string.Empty;
            this.FirstName.Text = string.Empty;
            this.MiddleName.Text = string.Empty;
            this.LastName.Text = string.Empty;
            this.Gender.Text = string.Empty;
            this.Birthday.DisplayDate = DateTime.UtcNow;
            this.Adress.Text = string.Empty;
            this.PhoneNumber.Text = string.Empty;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
