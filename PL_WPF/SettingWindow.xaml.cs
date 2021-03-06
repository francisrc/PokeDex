﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BS_PokedexManager;
using DAL_JSON;

namespace PL_WPF
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow
    {
        private Business.Generation generation;
        private ListClass _listClass;

        public SettingWindow()
        {
            InitializeComponent();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            var v = new MainWindow(_listClass);
            v.Show();
            this.Close();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _listClass = BS_PokedexManager.Business.GeneratePokeList(generation, sender as BackgroundWorker);
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {    
            ProgressBar.Value = e.ProgressPercentage;
            //TODO: Doesn't work!!!!
            DescriptionTextBlock.DataContext = BS_PokedexManager.Business.DescriptionProgress;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            generation = (Business.Generation)Enum.Parse(typeof(Business.Generation), rb.Content.ToString());

            RadioButtonStackPanel.IsEnabled = false;

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            worker.RunWorkerAsync();
        }

    }
}
