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
            Update();
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
                UpdateError();
                
            }
        }

        void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            try
            {
                StreamReader reader = new StreamReader(e.Result);
                List<string> slist = new List<string>();
                while (!reader.EndOfStream)
                    slist.Add(reader.ReadLine());
                e.Result.Close();
                reader.Close();

                List<LabInfo> TempLabInfos = new List<LabInfo>();
                foreach (string s in slist)
                {
                    string[] delim = new string[] { "<TD>" };
                    if (s.Trim().StartsWith("<TD>"))
                    {
                        List<String> split = new List<String>(s.Trim().Split(delim, StringSplitOptions.RemoveEmptyEntries));
                        split.RemoveAll(x => x == "");
                        LabInfo i = new LabInfo();
                        i.Name = split[0];
                        i.Available = split[1];
                        i.Busy = split[2];
                        i.Total = split[3];
                        i.Percent = split[4];
                        i.Time = split[5];
                        TempLabInfos.Add(i);
                    }
                }

                TempLabInfos = TempLabInfos.OrderBy(x => float.Parse(x.Percent)).ToList<LabInfo>();
                List<LabDetailControl> LabDetails = new List<LabDetailControl>();

                foreach (LabInfo i in TempLabInfos)
                {
                    LabDetailControl ld = new LabDetailControl();
                    ld.LabName.Text = i.Name;
                    ld.Detail.Text = "Usage: " + i.Busy + "/" + i.Total + " (" + i.Percent + "%)";

                    if (float.Parse(i.Percent) > 80)
                    {
                        ld.Background = new SolidColorBrush(Colors.Red);
                    }

                    LabDetails.Add(ld);
                }

                LabList.ItemsSource = LabDetails;
                LastUpdated.Text = "Last update: " + TempLabInfos[0].Time;
            }
            catch
            {
                UpdateError();
            }
        }

        private void UpdateError()
        {
            LabDetailControl ldc = new LabDetailControl();
            ldc.LabNameText = "Error";
            ldc.DetailText = "Cannot load the content.";
            ldc.Background = new SolidColorBrush(Colors.Gray);
            LabList.ItemsSource = new List<object> { ldc };
            LastUpdated.Text = "Last update: ERROR";
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}