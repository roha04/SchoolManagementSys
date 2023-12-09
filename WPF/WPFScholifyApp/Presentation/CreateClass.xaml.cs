// <copyright file="CreateClass.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.Presentation
{
    using System.Windows;
    using WPFScholifyApp.BLL;
    using WPFScholifyApp.DAL.DBClasses;

    /// <summary>
    /// Interaction logic for CreateClass.xaml.
    /// </summary>
    public partial class CreateClass : Window
    {
        private AdminService adminService;
        private AdvertisementService advertisementService;
        private ClassService classService;
        private JournalService journalService;
        private ParentsService parentsService;
        private PupilService pupilService;
        private UserService userService;
        private ScheduleService scheduleService;
        private TeacherService teacherService;
        private WindowService windowService;
        private MainWindow mainWindow;

        public CreateClass(
            AdminService adminService,
            AdvertisementService advertisementService,
            ClassService classService,
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
            this.classService = classService;
            this.journalService = journalService;
            this.parentsService = parentsService;
            this.pupilService = pupilService;
            this.userService = userService;
            this.scheduleService = scheduleService;
            this.teacherService = teacherService;
            this.windowService = windowService;
            this.mainWindow = mainWindow;
            this.InitializeComponent();
        }

        private void SaveClass(object sender, RoutedEventArgs e)
        {
            string className = this.ClassName.Text;
            this.Hide();
            var clases = new Class
            {
                Id = this.adminService.GetNewClassId(),
                ClassName = className,
            };

            this.classService.Save(clases);
            this.windowService.Show<AdminWindow>(window =>
            {
                window.LeftPanel.Children.Clear();
                window.LeftAction.Children.Clear();
                window.ShowAllClasses();
                window.LeftPanel.UpdateLayout();
                window.LeftAction.UpdateLayout();
            });
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
