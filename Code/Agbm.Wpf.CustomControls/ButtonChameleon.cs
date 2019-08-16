/*
 * Changing the IsEnabled property causes the OnRender mehod to be called and disables the control events
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Agbm.Wpf.CustomControls.Helpers;

namespace Agbm.Wpf.CustomControls
{
    public sealed class ButtonChameleon : Decorator
    {
        #region Fields

        private const double GAP = 1.0;

        #endregion


        #region BackgroundProperty

        /// <summary>
        /// DependencyProperty for <see cref="Background" /> property.
        /// </summary>
        public static readonly DependencyProperty BackgroundProperty =
                    Control.BackgroundProperty.AddOwner(
                            typeof(ButtonChameleon),
                            new FrameworkPropertyMetadata(
                                    null,
                                    FrameworkPropertyMetadataOptions.AffectsRender,
                                    OnBackgroundPropertyChanged));

        /// <summary>
        /// The Background property defines the brush used to fill the background of the button.
        /// </summary>
        public Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        private static void OnBackgroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Trace.WriteLine($"OnBackground thread id: {Thread.CurrentThread.ManagedThreadId}");
        }

        #endregion


        #region BorderBrushProperty

        /// <summary>
        /// DependencyProperty for <see cref="BorderBrush" /> property.
        /// </summary>
        public static readonly DependencyProperty BorderBrushProperty =
                Border.BorderBrushProperty.AddOwner(
                        typeof(ButtonChameleon),
                        new FrameworkPropertyMetadata(
                                null,
                                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// The BorderBrush property defines the brush used to draw the outer border.
        /// </summary>
        public Brush BorderBrush
        {
            get => (Brush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }

        #endregion


        #region CornerRadiusProperty

        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(ButtonChameleon),
                                                  new FrameworkPropertyMetadata(default(CornerRadius),
                                                                                 FrameworkPropertyMetadataOptions.AffectsRender));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        #endregion


        #region RenderMouseOverProperty

        /// <summary>
        /// DependencyProperty for <see cref="RenderMouseOver" /> property.
        /// </summary>
        public static readonly DependencyProperty RenderMouseOverProperty =
                 DependencyProperty.Register("RenderMouseOver",
                         typeof(bool),
                         typeof(ButtonChameleon),
                         new FrameworkPropertyMetadata(
                                false,
                                new PropertyChangedCallback(OnRenderMouseOverChanged)));

        /// <summary>
        /// When true the chrome renders with a mouse over look.
        /// </summary>
        public bool RenderMouseOver
        {
            get => (bool)GetValue(RenderMouseOverProperty);
            set => SetValue(RenderMouseOverProperty, value);
        }

        private static void OnRenderMouseOverChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonChameleon chameleon = ((ButtonChameleon)o);

            if ( Animates )
            {
                Brush bo;

                if ( !chameleon.RenderPressed ) 
                {
                    if ( (bool)e.NewValue ) {

                        if ( chameleon._localResouces == null ) 
                        {
                            chameleon._localResouces = new LocalResouces();
                            // ставит в очередь перерисовку
                            chameleon.InvalidateVisual();
                        }

                        // TODO animations setup:

                        Duration d = new Duration( TimeSpan.FromSeconds( 0.3 ) );
                        DoubleAnimation da = new DoubleAnimation(1 , d);

                        bo = chameleon.BackgroundOverlay;

                        if ( bo is SolidColorBrush ) 
                        {
                            Color c = (( SolidColorBrush )chameleon.NormalBackgroundBrush).Color.Modify( 15 );

                            ColorAnimation ca = new ColorAnimation( c, d );
                            bo.BeginAnimation( SolidColorBrush.ColorProperty, ca );
                        }
                        else if ( bo is GradientBrush ) {

                        }

                        bo.BeginAnimation( Brush.OpacityProperty, da );
                    }
                    // Mouse is not hovered
                    else if (chameleon._localResouces == null ) {
                        chameleon.InvalidateVisual();
                    }
                    else {
                        // TODO to normal state animations:
                        Duration d = new Duration(TimeSpan.FromSeconds(0.3));
                        DoubleAnimation da = new DoubleAnimation(0, d);

                        bo = chameleon.BackgroundOverlay;

                        if ( bo is SolidColorBrush ) {

                            Color c = ((SolidColorBrush)chameleon.NormalBackgroundBrush).Color;
                            ColorAnimation ca = new ColorAnimation(c, d);
                            ca.Completed += CaOnCompleted;

                            chameleon.BeginAnimation(SolidColorBrush.ColorProperty, ca);

                            void CaOnCompleted( object sender, EventArgs args )
                            {
                                ca.Completed -= CaOnCompleted;
                                ((SolidColorBrush)bo).Color = c;
                            }
                        }

                        bo.BeginAnimation(Brush.OpacityProperty, da);
                    }
                }
            }
            else {
                chameleon._localResouces = null;
                chameleon.InvalidateVisual();
            }
        }


        #endregion


        #region RenderPressedProperty

        /// <summary>
        /// DependencyProperty for <see cref="RenderPressed" /> property.
        /// </summary>
        public static readonly DependencyProperty RenderPressedProperty =
                 DependencyProperty.Register("RenderPressed",
                         typeof(bool),
                         typeof(ButtonChameleon),
                         new FrameworkPropertyMetadata(
                                false,
                                new PropertyChangedCallback(OnRenderPressedChanged)));

        /// <summary>
        /// When true the chrome renders with a pressed look.
        /// </summary>
        public bool RenderPressed
        {
            get => (bool)GetValue(RenderPressedProperty);
            set => SetValue(RenderPressedProperty, value);
        }

        private static void OnRenderPressedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            //ButtonChameleon chrome = ((ButtonChameleon)o);

            //if (chrome.Animates)
            //{
            //    if (((bool)e.NewValue))
            //    {
            //        if (chrome._localResources == null)
            //        {
            //            chrome._localResources = new LocalResources();
            //            chrome.InvalidateVisual();
            //        }

            //        Duration duration = new Duration(TimeSpan.FromSeconds(0.1));

            //        DoubleAnimation da = new DoubleAnimation(1, duration);

            //        chrome.BackgroundOverlay.BeginAnimation(SolidColorBrush.OpacityProperty, da);
            //        chrome.BorderOverlayPen.Brush.BeginAnimation(SolidColorBrush.OpacityProperty, da);
            //        chrome.LeftDropShadowBrush.BeginAnimation(LinearGradientBrush.OpacityProperty, da);
            //        chrome.TopDropShadowBrush.BeginAnimation(LinearGradientBrush.OpacityProperty, da);

            //        da = new DoubleAnimation(0, duration);
            //        chrome.InnerBorderPen.Brush.BeginAnimation(LinearGradientBrush.OpacityProperty, da);

            //        ColorAnimation ca = new ColorAnimation(Color.FromRgb(0xC2, 0xE4, 0xF6), duration);
            //        GradientStopCollection gsc = ((LinearGradientBrush)chrome.BackgroundOverlay).GradientStops;
            //        gsc[0].BeginAnimation(GradientStop.ColorProperty, ca);
            //        gsc[1].BeginAnimation(GradientStop.ColorProperty, ca);

            //        ca = new ColorAnimation(Color.FromRgb(0xAB, 0xDA, 0xF3), duration);
            //        gsc[2].BeginAnimation(GradientStop.ColorProperty, ca);

            //        ca = new ColorAnimation(Color.FromRgb(0x90, 0xCB, 0xEB), duration);
            //        gsc[3].BeginAnimation(GradientStop.ColorProperty, ca);

            //        ca = new ColorAnimation(Color.FromRgb(0x2C, 0x62, 0x8B), duration);
            //        chrome.BorderOverlayPen.Brush.BeginAnimation(SolidColorBrush.ColorProperty, ca);
            //    }
            //    else if (chrome._localResources == null)
            //    {
            //        chrome.InvalidateVisual();
            //    }
            //    else
            //    {
            //        bool renderMouseOver = chrome.RenderMouseOver;
            //        Duration duration = new Duration(TimeSpan.FromSeconds(0.1));

            //        DoubleAnimation da = new DoubleAnimation();
            //        da.Duration = duration;
            //        chrome.LeftDropShadowBrush.BeginAnimation(LinearGradientBrush.OpacityProperty, da);
            //        chrome.TopDropShadowBrush.BeginAnimation(LinearGradientBrush.OpacityProperty, da);
            //        chrome.InnerBorderPen.Brush.BeginAnimation(LinearGradientBrush.OpacityProperty, da);

            //        if (!renderMouseOver)
            //        {
            //            chrome.BorderOverlayPen.Brush.BeginAnimation(SolidColorBrush.OpacityProperty, da);
            //            chrome.BackgroundOverlay.BeginAnimation(SolidColorBrush.OpacityProperty, da);
            //        }

            //        ColorAnimation ca = new ColorAnimation();
            //        ca.Duration = duration;
            //        chrome.BorderOverlayPen.Brush.BeginAnimation(SolidColorBrush.ColorProperty, ca);

            //        GradientStopCollection gsc = ((LinearGradientBrush)chrome.BackgroundOverlay).GradientStops;
            //        gsc[0].BeginAnimation(GradientStop.ColorProperty, ca);
            //        gsc[1].BeginAnimation(GradientStop.ColorProperty, ca);
            //        gsc[2].BeginAnimation(GradientStop.ColorProperty, ca);
            //        gsc[3].BeginAnimation(GradientStop.ColorProperty, ca);
            //    }
            //}
            //else
            //{
            //    chrome._localResources = null;
            //    chrome.InvalidateVisual();
            //}
        }

        #endregion


        #region RenderMouseOverProperty

        /// <summary>
        /// DependencyProperty for <see cref="RenderMouseOver" /> property.
        /// </summary>
        public static readonly DependencyProperty RenderDisabledProperty =
                 DependencyProperty.Register("RenderDisabled",
                         typeof(bool),
                         typeof(ButtonChameleon),
                         new FrameworkPropertyMetadata(
                                false,
                                new PropertyChangedCallback(OnRenderDisabledChanged)));

        /// <summary>
        /// When true the chrome renders with a mouse over look.
        /// </summary>
        public bool RenderDisabled
        {
            get => (bool)GetValue(RenderDisabledProperty);
            set => SetValue(RenderDisabledProperty, value);
        }

        private static void OnRenderDisabledChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ButtonChameleon chameleon = ((ButtonChameleon)o);


        }

        #endregion


        #region CLR Properties

        private static readonly object _lock = new object();

        private static bool Animates =>
            SystemParameters.PowerLineStatus == PowerLineStatus.Online &&
            SystemParameters.ClientAreaAnimation &&
            RenderCapability.Tier > 0;



        private static Brush _commonDisabledBackgroundBrush;
        private static Brush CommonDisabledBackgroundBrush
        {
            get {
                if ( _commonDisabledBackgroundBrush == null ) {
                    bool lockTaken = false;
                    Monitor.Enter( _lock, ref lockTaken );

                    if ( _commonDisabledBackgroundBrush == null ) {
                        _commonDisabledBackgroundBrush = new SolidColorBrush();

                        if ( _commonDisabledBackgroundBrush.CanFreeze ) {
                            _commonDisabledBackgroundBrush.Freeze();
                        }
                    }

                    if ( lockTaken ) Monitor.Exit( _lock );
                }

                return _commonDisabledBackgroundBrush;
            }
        }

        private Brush _disabledBackgroundBrush;

        private Brush DisabledBackgroundBrush
        {
            get {
                if ( Background == null ) {
                    return CommonDisabledBackgroundBrush;
                }

                return _disabledBackgroundBrush;
            }
        }



        private static Brush _commonMouseHoverBackgroundBrush;

        private static Brush CommonMouseHoverBackgroundBrush
        {
            get {
                if (_commonMouseHoverBackgroundBrush == null)
                {
                    bool lockTaken = false;
                    Monitor.Enter(_lock, ref lockTaken);

                    if (_commonMouseHoverBackgroundBrush == null)
                    {
                        _commonMouseHoverBackgroundBrush = new SolidColorBrush();

                        if (_commonMouseHoverBackgroundBrush.CanFreeze)
                        {
                            _commonMouseHoverBackgroundBrush.Freeze();
                        }
                    }

                    if (lockTaken) Monitor.Exit(_lock);
                }

                return _commonMouseHoverBackgroundBrush;
            }
        }

        private Brush _mouseHoverBackgroundBrush;

        private Brush MouseHoverBackgroundBrush
        {
            get {
                if (Background == null)
                {
                    return CommonMouseHoverBackgroundBrush;
                }

                return _mouseHoverBackgroundBrush;
            }
        }



        private static Brush _commonPressedBackgroundBrush;

        private static Brush CommonPressedBackgroundBrush
        {
            get {
                if (_commonPressedBackgroundBrush == null)
                {
                    bool lockTaken = false;
                    Monitor.Enter(_lock, ref lockTaken);

                    if (_commonPressedBackgroundBrush == null)
                    {
                        _commonPressedBackgroundBrush = new SolidColorBrush();

                        if (_commonPressedBackgroundBrush.CanFreeze)
                        {
                            _commonPressedBackgroundBrush.Freeze();
                        }
                    }

                    if (lockTaken) Monitor.Exit(_lock);
                }

                return _commonPressedBackgroundBrush;
            }
        }

        private Brush _pressedBackgroundBrush;

        private Brush PressedBackgroundBrush
        {
            get {
                if (Background == null)
                {
                    return CommonPressedBackgroundBrush;
                }

                return _pressedBackgroundBrush;
            }
        }



        private static Brush _commonNormalBackgroundBrush;

        private static Brush CommonNormalBackgroundBrush
        {
            get {
                if (_commonNormalBackgroundBrush == null)
                {
                    bool lockTaken = false;
                    Monitor.Enter(_lock, ref lockTaken);

                    if (_commonNormalBackgroundBrush == null)
                    {
                        _commonNormalBackgroundBrush = new SolidColorBrush( new Color() { A = 0, R = 0, G = 0, B = 0 } );

                        if (_commonNormalBackgroundBrush.CanFreeze)
                        {
                            _commonNormalBackgroundBrush.Freeze();
                        }
                    }

                    if (lockTaken) Monitor.Exit(_lock);
                }

                return _commonNormalBackgroundBrush;
            }
        }

        private Brush _normalBackgroundBrush;

        private Brush NormalBackgroundBrush
        {
            get {
                if ( Background == null 
                     || ( Background != null 
                          && Background.GetType() != typeof( SolidColorBrush )
                          && Background.GetType() != typeof( GradientBrush ) ) )
                {
                    return CommonNormalBackgroundBrush;
                }

                return Background;
            }
        }



        private Brush BackgroundOverlay
        {
            get {
                if (!Animates)
                {
                    if ( RenderDisabled ) {
                        return DisabledBackgroundBrush;
                    }

                    if ( RenderMouseOver ) {
                        return MouseHoverBackgroundBrush;
                    }

                    if ( RenderPressed ) {
                        return PressedBackgroundBrush;
                    }

                    return null;
                }

                if ( _localResouces != null ) {

                    if ( _localResouces.BackgroundOverlay == null ) {

                        _localResouces.BackgroundOverlay = NormalBackgroundBrush.Clone();
                        _localResouces.BackgroundOverlay.Opacity = 0;

                    }

                    return _localResouces.BackgroundOverlay;
                }

                return null;
            }
        }

        #endregion


        #region Protected Methods

        protected override Size MeasureOverride(Size availableSize)
        {
            Size desired;
            UIElement child = Child;

            if (child != null)
            {
                child.Measure(availableSize);
                desired = child.DesiredSize;

                if (availableSize.Width < 2)
                {
                    desired.Width += 2;
                }

                if (availableSize.Height < 2)
                {
                    desired.Height += 2;
                }
            }
            else
            {
                desired = new Size(
                   width: Math.Min(2, availableSize.Width),
                   height: Math.Min(2, availableSize.Height));
            }

            return desired;
        }

        /// <summary>
        /// Computes the position of its single child
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            UIElement child = Child;
            if (child == null) return finalSize;


            Rect childArrangeRect = new Rect
            {
                Width = Math.Max(0d, finalSize.Width - 2),
                Height = Math.Max(0d, finalSize.Height - 2)
            };
            childArrangeRect.X = (finalSize.Width - childArrangeRect.Width) * 0.5;
            childArrangeRect.Y = (finalSize.Height - childArrangeRect.Height) * 0.5;

            child.Arrange(childArrangeRect);

            return finalSize;
        }

        /// <summary>
        /// Render callback.
        /// </summary>
        protected override void OnRender(DrawingContext drawingContext)
        {
            Trace.WriteLine($"OnRender thread id: {Thread.CurrentThread.ManagedThreadId}");

            Rect bounds = new Rect(0, 0, ActualWidth, ActualHeight);

            // Draw Background (if we don't  the system draws white rectangle)
            DrawBackground(drawingContext, ref bounds);

            // Draw Border dropshadows
            //DrawDropShadows( drawingContext, ref bounds );

            // Draw outer border
            //DrawBorder( drawingContext, ref bounds );
        }

        private void DrawBackground(DrawingContext dc, ref Rect bounds)
        {
            if (!IsEnabled)
                return;

            Brush fill = Background;

            if ((bounds.Width > 2) && (bounds.Height > 2))
            {
                Rect backgroundRect = new Rect(bounds.Left + 1,
                                    bounds.Top + 1,
                                    bounds.Width - 2,
                                    bounds.Height - 2);

                // Draw Background
                if (fill != null)
                    dc.DrawRectangle(fill, null, backgroundRect);

                // Draw BackgroundOverlay
                fill = BackgroundOverlay;
                if (fill != null)
                    dc.DrawRectangle(fill, null, backgroundRect);
            }
        }


        private void DrawBorder(DrawingContext dc, ref Rect bounds)
        {

        }

        #endregion

        private LocalResouces _localResouces;

        private class LocalResouces
        {
            public Brush BackgroundOverlay;
        }
    }
}
