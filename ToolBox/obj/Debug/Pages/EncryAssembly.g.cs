﻿#pragma checksum "..\..\..\Pages\EncryAssembly.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3E0C2808A37B5CEA6981BFB240B028C6"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18063
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Converters;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace ToolBox.Pages {
    
    
    /// <summary>
    /// EncryAssembly
    /// </summary>
    public partial class EncryAssembly : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\Pages\EncryAssembly.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_OpenAssembly;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Pages\EncryAssembly.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tb_AssemblyPath;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Pages\EncryAssembly.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_encryKey;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Pages\EncryAssembly.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_UnEncryKey;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Pages\EncryAssembly.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView TypeList;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Pages\EncryAssembly.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView MethodList;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ToolBox;component/pages/encryassembly.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\EncryAssembly.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btn_OpenAssembly = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\Pages\EncryAssembly.xaml"
            this.btn_OpenAssembly.Click += new System.Windows.RoutedEventHandler(this.Button_Open);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tb_AssemblyPath = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.tb_encryKey = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            
            #line 16 "..\..\..\Pages\EncryAssembly.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_EncryAssembly_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tb_UnEncryKey = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            
            #line 20 "..\..\..\Pages\EncryAssembly.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_UnEncryAssembly_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.TypeList = ((System.Windows.Controls.ListView)(target));
            return;
            case 8:
            
            #line 24 "..\..\..\Pages\EncryAssembly.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_CreateType_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.MethodList = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

