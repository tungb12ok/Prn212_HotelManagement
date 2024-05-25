﻿#pragma checksum "..\..\..\ManageRoomsWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "85A89B4CEE1F36A8B24CFFBB9E36C04AD26FE46C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WindowsPresentation;


namespace WindowsPresentation {
    
    
    /// <summary>
    /// ManageRoomsWindow
    /// </summary>
    public partial class ManageRoomsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\ManageRoomsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSearchRoom;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\ManageRoomsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RoomIDTextBox;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\ManageRoomsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RoomNumberTextBox;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\ManageRoomsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RoomDescriptionTextBox;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\ManageRoomsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MaxCapacityTextBox;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\ManageRoomsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RoomTypeComboBox;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\ManageRoomsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RoomStatusComboBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\ManageRoomsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PricePerDayTextBox;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\ManageRoomsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid RoomsDataGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.30.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WindowsPresentation;V1.0.0.0;component/manageroomswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ManageRoomsWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.30.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtSearchRoom = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            
            #line 13 "..\..\..\ManageRoomsWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SearchRoom_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.RoomIDTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.RoomNumberTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.RoomDescriptionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.MaxCapacityTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.RoomTypeComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.RoomStatusComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.PricePerDayTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            
            #line 40 "..\..\..\ManageRoomsWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddRoom_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 41 "..\..\..\ManageRoomsWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditRoom_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 42 "..\..\..\ManageRoomsWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteRoom_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.RoomsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 45 "..\..\..\ManageRoomsWindow.xaml"
            this.RoomsDataGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.RoomsDataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

