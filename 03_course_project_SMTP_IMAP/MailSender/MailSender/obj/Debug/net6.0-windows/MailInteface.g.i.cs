﻿#pragma checksum "..\..\..\MailInteface.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8067BD4D31A1120357737074E315DF67EDDACDE0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MailSender;
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


namespace MailSender {
    
    
    /// <summary>
    /// MailInteface
    /// </summary>
    public partial class MailInteface : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\MailInteface.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox FolderList;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\MailInteface.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox MessagesList;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\MailInteface.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddToFolderBtn;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\MailInteface.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CreateNewFolderBtn;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\MailInteface.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RenameFolderBtn;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\MailInteface.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteFolderBtn;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\MailInteface.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock DateTxtBlock;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\MailInteface.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SubjectTxtBlock;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\MailInteface.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MessageTxtBlock;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.12.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MailSender;component/mailinteface.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MailInteface.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.12.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 22 "..\..\..\MailInteface.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 23 "..\..\..\MailInteface.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 3:
            this.FolderList = ((System.Windows.Controls.ListBox)(target));
            
            #line 27 "..\..\..\MailInteface.xaml"
            this.FolderList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.FolderList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.MessagesList = ((System.Windows.Controls.ListBox)(target));
            
            #line 28 "..\..\..\MailInteface.xaml"
            this.MessagesList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.MessagesList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.AddToFolderBtn = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\MailInteface.xaml"
            this.AddToFolderBtn.Click += new System.Windows.RoutedEventHandler(this.AddToFolderBtn_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CreateNewFolderBtn = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\MailInteface.xaml"
            this.CreateNewFolderBtn.Click += new System.Windows.RoutedEventHandler(this.CreateNewFolderBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.RenameFolderBtn = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\MailInteface.xaml"
            this.RenameFolderBtn.Click += new System.Windows.RoutedEventHandler(this.RenameFolderBtn_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.DeleteFolderBtn = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\MailInteface.xaml"
            this.DeleteFolderBtn.Click += new System.Windows.RoutedEventHandler(this.DeleteFolderBtn_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.DateTxtBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.SubjectTxtBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.MessageTxtBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

