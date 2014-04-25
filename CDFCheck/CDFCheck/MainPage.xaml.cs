using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CDFCheck.Resources;
using System.IO;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace CDFCheck
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
            Update();
            /*LabDetail ld = new LabDetail();
            Labs.ItemsSource = new List<object> { 
                new LabDetail(),  new LabDetail() };*/
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void Update()
        {
            try
            {
                WebClient client = new WebClient();
                client.OpenReadCompleted += client_OpenReadCompleted;
                client.OpenReadAsync(new Uri("http://www.cdf.toronto.edu/usage/"));
            }
            catch
            {
                LabList.ItemsSource = new List<object> { "Error: the list cannot be generated." };
                LastUpdated.Text = "Last update: --";
            }
        }

        void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            StreamReader reader = new StreamReader(e.Result);
            List<string> slist = new List<string>();
            while (!reader.EndOfStream)
                slist.Add(reader.ReadLine());
            e.Result.Close();
            reader.Close();

            List<Info> Infos = new List<Info>();
            foreach (string s in slist)
            {
                string[] delim = new string[] { "<TD>" };
                if (s.Trim().StartsWith("<TD>"))
                {
                    List<String> split = new List<String>(s.Trim().Split(delim, StringSplitOptions.RemoveEmptyEntries));
                    split.RemoveAll(x => x == "");
                    Info i = new Info();
                    i.Name = split[0];
                    i.Available = split[1];
                    i.Busy = split[2];
                    i.Total = split[3];
                    i.Percent = split[4];
                    i.Time = split[5];
                    Infos.Add(i);
                }
            }

            Infos.OrderBy(x => x.Percent);
            List<LabDetail> LabDetails = new List<LabDetail>();

            foreach (Info i in Infos)
            {
                LabDetail ld = new LabDetail();
                ld.LabName.Text = i.Name;
                ld.Detail.Text = "Usage: " + i.Busy + "/" + i.Total + " (" + i.Percent + "%)";

                if (float.Parse(i.Percent) > 80)
                {
                    ld.LayoutRoot.Background = new SolidColorBrush(Colors.Red);
                }

                LabDetails.Add(ld);
            }

            LabList.ItemsSource = LabDetails;
            LastUpdated.Text = "Last update: " + Infos[0].Time;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}