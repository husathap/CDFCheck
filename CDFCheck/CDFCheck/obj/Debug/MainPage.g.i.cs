﻿#pragma checksum "C:\Users\Hubert\documents\visual studio 2012\Projects\CDFCheck\CDFCheck\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "99D0786109D3B96F310E1A18AA23B81C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace CDFCheck {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.LongListSelector LabList;
        
        internal System.Windows.Controls.TextBlock LastUpdated;
        
        internal System.Windows.Controls.Button btnRefresh;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/CDFCheck;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.LabList = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("LabList")));
            this.LastUpdated = ((System.Windows.Controls.TextBlock)(this.FindName("LastUpdated")));
            this.btnRefresh = ((System.Windows.Controls.Button)(this.FindName("btnRefresh")));
        }
    }
}

