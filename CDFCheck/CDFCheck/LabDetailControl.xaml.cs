using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace CDFCheck
{
    public partial class LabDetailControl : UserControl
    {
        public LabDetailControl()
        {
            InitializeComponent();
        }

        new public System.Windows.Media.Brush Background
        {
            get { return LayoutRoot.Background; }
            set { LayoutRoot.Background = value; }
        }

        public string LabNameText
        {
            get { return LabName.Text; }
            set { LabName.Text = value; }
        }

        public string DetailText
        {
            get { return Detail.Text; }
            set { Detail.Text = value; }
        }
    }
}
