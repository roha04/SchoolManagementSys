// <copyright file="CreateSchedule.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WPFScholifyApp.Presentation
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using WPFScholifyApp.BLL;
    using WPFScholifyApp.DAL.ClassRepository;
    using WPFScholifyApp.DAL.DBClasses;
    using DayOfWeek = WPFScholifyApp.DAL.DBClasses.DayOfWeek;

    /// <summary>
    /// Interaction logic for CreateTeacher.xaml.
    /// </summary>
    public partial class CreateSchedule : Window
    {
        private AdminService adminService;
        private AdvertisementService advertisementService;
        private ClassService classService;
        private DayOfWeekService dayOfWeekService;
        private JournalService journalService;
        private LessonTimeService lessonTimeService;
        private ParentsService parentsService;
        private PupilService pupilService;
        private UserService userService;
        private ScheduleService scheduleService;
        private SubjectService subjectService;
        private TeacherService teacherService;
        private WindowService windowService;
        private MainWindow mainWindow;

        public Class? Clas { get; set; }

        public int SelectedClassId { get; set; }

        public ObservableCollection<ComboBoxItem>? CbItems { get; set; }

        public ObservableCollection<ComboBoxItem>? CbItems2 { get; set; }

        public CreateSchedule(
            AdminService adminService,
            AdvertisementService advertisementService,
            ClassService classService,
            DayOfWeekService dayOfWeekService,
            JournalService journalService,
            LessonTimeService lessonTimeService,
            ParentsService parentsService,
            PupilService pupilService,
            UserService userService,
            ScheduleService scheduleService,
            SubjectService subjectService,
            TeacherService teacherService,
            WindowService windowService,
            MainWindow mainWindow)
        {
            this.InitializeComponent();

            this.adminService = adminService;
            this.advertisementService = advertisementService;
            this.classService = classService;
            this.dayOfWeekService = dayOfWeekService;
            this.journalService = journalService;
            this.lessonTimeService = lessonTimeService;
            this.parentsService = parentsService;
            this.pupilService = pupilService;
            this.userService = userService;
            this.scheduleService = scheduleService;
            this.subjectService = subjectService;
            this.teacherService = teacherService;
            this.windowService = windowService;
            this.mainWindow = mainWindow;
            this.CbItems = new ObservableCollection<ComboBoxItem>();
            this.Clas = this.classService.GetAllClasses().FirstOrDefault(x => x.Id == this.SelectedClassId) !;

            this.CbItems.Add(new ComboBoxItem { Content = $"{EnumsTime.T830.ToString("HH:mm")} - {EnumsTime.T915.ToString("HH:mm")}", Tag = 1 });
            this.CbItems.Add(new ComboBoxItem { Content = $"{EnumsTime.T930.ToString("HH:mm")} - {EnumsTime.T1015.ToString("HH:mm")}", Tag = 2 });
            this.CbItems.Add(new ComboBoxItem { Content = $"{EnumsTime.T1030.ToString("HH:mm")} - {EnumsTime.T1115.ToString("HH:mm")}", Tag = 3 });
            this.CbItems.Add(new ComboBoxItem { Content = $"{EnumsTime.T1135.ToString("HH:mm")} - {EnumsTime.T1220.ToString("HH:mm")}", Tag = 4 });
            this.CbItems.Add(new ComboBoxItem { Content = $"{EnumsTime.T1240.ToString("HH:mm")} - {EnumsTime.T1325.ToString("HH:mm")}", Tag = 5 });
            this.CbItems.Add(new ComboBoxItem { Content = $"{EnumsTime.T1335.ToString("HH:mm")} - {EnumsTime.T1420.ToString("HH:mm")}", Tag = 6 });
            this.CbItems.Add(new ComboBoxItem { Content = $"{EnumsTime.T1435.ToString("HH:mm")} - {EnumsTime.T1520.ToString("HH:mm")}", Tag = 7 });
            this.CbItems.Add(new ComboBoxItem { Content = $"{EnumsTime.T1530.ToString("HH:mm")} - {EnumsTime.T1615.ToString("HH:mm")}", Tag = 8 });
            this.CbItems = this.CbItems;
            this.TimeComboBox.ItemsSource = this.CbItems;
        }

        public void SetClassId(int classId)
        {
            this.SelectedClassId = classId;
            this.Clas = this.classService.GetAllClasses().FirstOrDefault(x => x.Id == this.SelectedClassId) !;
            this.ClassLabel.Content = this.Clas != null ? this.Clas!.ClassName : string.Empty;
            var subjects = this.subjectService.GetSubjectsByClassId(this.SelectedClassId);
            this.CbItems2 = new ObservableCollection<ComboBoxItem>();
            foreach (var subject in subjects)
            {
                this.CbItems2.Add(new ComboBoxItem { Content = subject.SubjectName, Tag = subject.Id });
            }

            this.CbItems2 = this.CbItems2;
            this.SubjectComboBox.ItemsSource = this.CbItems2;

            this.PopulateTimeComboBox();
        }

        private void PopulateTimeComboBox()
        {
            this.CbItems = new ObservableCollection<ComboBoxItem>();

            this.CbItems.Add(new ComboBoxItem { Content = $"{EnumsTime.T830.ToString("HH:mm")} - {EnumsTime.T915.ToString("HH:mm")}", Tag = 1 });
            this.CbItems.Add(new ComboBoxItem { Content = $"{EnumsTime.T930.ToString("HH:mm")} - {EnumsTime.T1015.ToString("HH:mm")}", Tag = 2 });
            this.CbItems.Add(new ComboBoxItem { Content = $"{EnumsTime.T1030.ToString("HH:mm")} - {EnumsTime.T1115.ToString("HH:mm")}", Tag = 3 });
            this.CbItems.Add(new ComboBoxItem { Content = $"{EnumsTime.T1135.ToString("HH:mm")} - {EnumsTime.T1220.ToString("HH:mm")}", Tag = 4 });
            this.CbItems.Add(new ComboBoxItem { Content = $"{EnumsTime.T1240.ToString("HH:mm")} - {EnumsTime.T1325.ToString("HH:mm")}", Tag = 5 });
            this.CbItems.Add(new ComboBoxItem { Content = $"{EnumsTime.T1335.ToString("HH:mm")} - {EnumsTime.T1420.ToString("HH:mm")}", Tag = 6 });
            this.CbItems.Add(new ComboBoxItem { Content = $"{EnumsTime.T1435.ToString("HH:mm")} - {EnumsTime.T1520.ToString("HH:mm")}", Tag = 7 });
            this.CbItems.Add(new ComboBoxItem { Content = $"{EnumsTime.T1530.ToString("HH:mm")} - {EnumsTime.T1615.ToString("HH:mm")}", Tag = 8 });

            this.TimeComboBox.ItemsSource = this.CbItems;
        }

        private void ClassComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var timeId = ((ComboBoxItem)this.TimeComboBox.SelectedItem).Tag != null ? (int)((ComboBoxItem)this.TimeComboBox.SelectedItem).Tag : 0;
            var lessonTime = this.lessonTimeService.GetLessonTimeById(timeId);
            var subjectId = ((ComboBoxItem)this.SubjectComboBox.SelectedItem).Tag != null ? (int)((ComboBoxItem)this.SubjectComboBox.SelectedItem).Tag : 0;
            var dayOfWeek = this.dayOfWeekService.GetAll().FirstOrDefault(x => x.Date.AddDays(1).Date.Equals(this.Date.SelectedDate!.Value.Date));

            if (dayOfWeek == null)
            {
                var newDayOfWeek = new DayOfWeek
                {
                    Id = this.adminService.GetNewDayOfWeekId(),
                    Date = this.Date.SelectedDate!.Value.ToUniversalTime(),
                    Day = this.Date.SelectedDate!.Value.ToUniversalTime().ToString("d"),
                };

                this.dayOfWeekService.Save(newDayOfWeek);
                dayOfWeek = new DayOfWeek { Id = newDayOfWeek.Id };
            }

            var teacher = this.teacherService.GetTeacherBySubjectId(subjectId);
            var classOfSubject = this.classService.GetClassBySubjectId(subjectId);
            var newSchedule = new Schedule()
            {
                TeacherId = teacher!.Id,
                ClassId = classOfSubject.Id,
                DayOfWeekId = dayOfWeek!.Id,
                LessonTimeId = lessonTime!.Id,
                SubjectId = subjectId,
            };

            this.scheduleService.Save(newSchedule);

            this.Hide();

            this.windowService.Show<AdminWindow>(window =>
            {
                window.ShowAllWeek(classOfSubject.Id);
            });
        }
    }
}