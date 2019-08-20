using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Agbm.Wpf.CustomControls.Infrastructure;
using Microsoft.Win32;

namespace Agbm.Wpf.CustomControls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Agbm.Wpf.CustomControls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Agbm.Wpf.CustomControls;assembly=Agbm.Wpf.CustomControls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:TetrisWindow/>
    ///
    /// </summary>
    public class TetrisWindow : Window
    {
        static TetrisWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TetrisWindow), new FrameworkPropertyMetadata(typeof(TetrisWindow)));
            SetUpCommands();
        }

        public TetrisWindow()
        {
            //double currentDPIScaleFactor = (double)SystemHelper.GetCurrentDPIScaleFactor();
            //Screen screen = Screen.FromHandle((new WindowInteropHelper(this)).Handle);

            //base.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
            //base.StateChanged += new EventHandler(this.OnStateChanged);
            //base.Loaded += new RoutedEventHandler(this.OnLoaded);
            //Rectangle workingArea = screen.WorkingArea;
            //base.MaxHeight = (double)(workingArea.Height + 16) / currentDPIScaleFactor;

            //SystemEvents.DisplaySettingsChanged += new EventHandler(this.SystemEvents_DisplaySettingsChanged);
            //this.AddHandler(Window.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.OnMouseButtonUp), true);
            //this.AddHandler(Window.MouseMoveEvent, new System.Windows.Input.MouseEventHandler(this.OnMouseMove));
        }

        private static void SetUpCommands()
        {
            CommandManager.RegisterClassCommandBinding( typeof(TetrisWindow),
                new CommandBinding( WindowCommands.Maximize, Maximize_Executed, Maximize_CanExecute ));

            CommandManager.RegisterClassCommandBinding(typeof(TetrisWindow),
                new CommandBinding(WindowCommands.Restore, Restore_Executed, Restore_CanExecute));

            CommandManager.RegisterClassCommandBinding(typeof(TetrisWindow),
                new CommandBinding(WindowCommands.Minimize, Minimize_Executed, Minimize_CanExecute));

            CommandManager.RegisterClassCommandBinding(typeof(TetrisWindow),
                new CommandBinding(WindowCommands.Close, Close_Executed ));
        }

        #region Properties
        private Button MinimizeButton { get; set; }
        private Button MaximizeButton { get; set; }
        private Button RestoreButton { get; set; }
        private Button CloseButton { get; set; }

        #endregion

        private void OnLoaded( object sender, RoutedEventArgs args )
        {
            var hwnd = (new WindowInteropHelper( this )).Handle;

            if ( WindowState == WindowState.Maximized ) 
            {
                if ( MaximizeButton != null ) MinimizeButton.Visibility = Visibility.Collapsed;
                if ( RestoreButton != null ) RestoreButton.Visibility = Visibility.Visible;
            }
        }

        #region Commands

        private static void Maximize_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var wnd = (TetrisWindow)sender;
            e.CanExecute = wnd.WindowState == WindowState.Normal;
        }

        private static void Maximize_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var wnd = (TetrisWindow)sender;
            wnd.WindowState = WindowState.Maximized;
            wnd.MaximizeButton.Visibility = Visibility.Collapsed;
            wnd.RestoreButton.Visibility = Visibility.Visible;
        }

        private static void Restore_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var wnd = (TetrisWindow)sender;
            e.CanExecute = wnd.WindowState == WindowState.Maximized;
        }

        private static void Restore_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var wnd = (TetrisWindow)sender;
            wnd.WindowState = WindowState.Normal;
            wnd.RestoreButton.Visibility = Visibility.Collapsed;
            wnd.MaximizeButton.Visibility = Visibility.Visible;
        }

        private static void Minimize_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var wnd = (TetrisWindow)sender;
            e.CanExecute = wnd.WindowState == WindowState.Normal;
        }

        private static void Minimize_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var wnd = (TetrisWindow)sender;
            wnd.WindowState = WindowState.Minimized;
        }

        private static void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var wnd = (TetrisWindow)sender;
            wnd.Close();
        }

        #endregion


        #region protected

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            MinimizeButton = GetTemplateChild( "PART_MinimizeButton" ) as Button;
            if ( MinimizeButton != null ) {
                MinimizeButton.Command = WindowCommands.Minimize;
            }

            MaximizeButton = GetTemplateChild("PART_MaximizeButton") as Button;
            if (MaximizeButton != null){
                MaximizeButton.Command = WindowCommands.Maximize;
            }

            RestoreButton = GetTemplateChild("PART_RestoreButton") as Button;
            if (RestoreButton != null){
                RestoreButton.Command = WindowCommands.Restore;
            }

            CloseButton = GetTemplateChild("PART_CloseButton") as Button;
            if (CloseButton != null){
                CloseButton.Command = WindowCommands.Close;
            }
        }

        #endregion
    }
}
