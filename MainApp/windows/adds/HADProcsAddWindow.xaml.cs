﻿using MainApp.assets.models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace MainApp.windows
{
    /// <summary>
    /// Interaction logic for HADProcsAddWindow.xaml
    /// </summary>
    public partial class HADProcsAddWindow : Window
    {
        readonly BigBoarsEntities db_cont = new BigBoarsEntities();
        readonly NewProcData n = new NewProcData();
        public int[] ProcIds = new int[5];
        
        public HADProcsAddWindow()
        {
            InitializeComponent();

            this.DataContext = n;

            var procs = db_cont.MedProcedures.ToList();
            n.ProcOptions = procs;
        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;

            foreach (PropertyInfo p in typeof(NewProcData).GetProperties())
            {
                if (p.PropertyType == typeof(MedProcedures))
                {
                    MedProcedures selectedProc = (MedProcedures)p.GetValue(n);
                    ProcIds[index++] = selectedProc != null ? selectedProc.id : 0;
                }
            }

            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }

    public class NewProcData
    {
        public MedProcedures SelectedProc1 { get; set; }
        public MedProcedures SelectedProc2 { get; set; }
        public MedProcedures SelectedProc3 { get; set; }
        public MedProcedures SelectedProc4 { get; set; }
        public MedProcedures SelectedProc5 { get; set; }
        public List<MedProcedures> ProcOptions { get; set; }
    }
}
