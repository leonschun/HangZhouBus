﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using HangZhouBus.DB;
using System.Collections.ObjectModel;
using System.Threading;

namespace HangZhouBus
{
    public partial class MainPage : PhoneApplicationPage
    {
        private BusDataContext db = new BusDataContext();
        private object o = new object();

        // 构造函数
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            GetBusList(this);
        }

        private void GetBusList(object state)
        {
            lock (o)
            {
                var list = from bus in db.BusTable.ToList()
                           where bus.Name.IndexOf(textBox.Text) != -1
                           select bus;

                Dispatcher.BeginInvoke(() =>
                {
                    listBox.ItemsSource = list;
                });
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(GetBusList);
        }
    }
}