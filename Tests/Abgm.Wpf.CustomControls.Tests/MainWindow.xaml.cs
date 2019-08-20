using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Agbm.Wpf.CustomControls;

namespace Abgm.Wpf.CustomControls.Tests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : TetrisWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var wc = WindowChrome.GetWindowChrome( this );
            Trace.WriteLine( $"CaptionHeight: { wc.CaptionHeight }" );

            var dp = ( DockPanel )GetTemplateChild( "IconTitleCaptionBar" );
            Trace.WriteLine( $"CaptionHeight: { dp?.ActualHeight }" );
            Trace.WriteLine( $"CaptionHeight: { dp?.Height }" );
        }


        private void ButtonBase_OnClick( object sender, RoutedEventArgs e )
        {
            Trace.WriteLine( SystemParameters.WindowResizeBorderThickness );
            Info2.Text = SystemParameters.MaximizedPrimaryScreenHeight.ToString();
        }
    }
}
