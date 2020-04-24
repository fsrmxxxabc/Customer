// Window.Universal.xaml.cs
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Customer.Theme
{
    public static class UniversalWindowStyle
    {
        public static readonly DependencyProperty TitleBarProperty = DependencyProperty.RegisterAttached(
            "TitleBar", typeof(UniversalTitleBar), typeof(UniversalWindowStyle),
            new PropertyMetadata(new UniversalTitleBar(), OnTitleBarChanged));

        public static UniversalTitleBar GetTitleBar(DependencyObject element)
            => (element != null)?(UniversalTitleBar)element.GetValue(TitleBarProperty):null;

        /*public static void SetTitleBar(DependencyObject element, UniversalTitleBar value)
            => element.SetValue(TitleBarProperty, value);*/
        public static void SetTitleBar(DependencyObject element, UniversalTitleBar value)
        {
            if(element != null)element.SetValue(TitleBarProperty, value);
        }

        public static readonly DependencyProperty TitleBarButtonStateProperty = DependencyProperty.RegisterAttached(
            "TitleBarButtonState", typeof(WindowState?), typeof(UniversalWindowStyle),
            new PropertyMetadata(null, OnButtonStateChanged));

        public static WindowState? GetTitleBarButtonState(DependencyObject element)
            => (element != null)?(WindowState?)element.GetValue(TitleBarButtonStateProperty):null;

        /*public static void SetTitleBarButtonState(DependencyObject element, WindowState? value)
            => element.SetValue(TitleBarButtonStateProperty, value);*/

        public static void SetTitleBarButtonState(DependencyObject element, WindowState? value)
        {
            if (element != null)
            {
                element.SetValue(TitleBarButtonStateProperty, value);
            }
        }

        public static readonly DependencyProperty IsTitleBarCloseButtonProperty = DependencyProperty.RegisterAttached(
            "IsTitleBarCloseButton", typeof(bool), typeof(UniversalWindowStyle),
            new PropertyMetadata(false, OnIsCloseButtonChanged));

        public static bool GetIsTitleBarCloseButton(DependencyObject element)
            => (element != null)?(bool)element.GetValue(IsTitleBarCloseButtonProperty):false;

        /*public static void SetIsTitleBarCloseButton(DependencyObject element, bool value)
            => element.SetValue(IsTitleBarCloseButtonProperty, value);*/

        public static void SetIsTitleBarCloseButton(DependencyObject element, bool value)
        {
            if (element != null)element.SetValue(IsTitleBarCloseButtonProperty, value);
        }
        private static void OnTitleBarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
#pragma warning disable CA1303 // 请不要将文本作为本地化参数传递
            if (e.NewValue is null) throw new NotSupportedException("TitleBar property should not be null.");
#pragma warning restore CA1303 // 请不要将文本作为本地化参数传递
        }

        private static void OnButtonStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = (Button)d;

            if (e.OldValue is WindowState)
            {
                button.Click -= StateButton_Click;
            }

            if (e.NewValue is WindowState)
            {
                button.Click -= StateButton_Click;
                button.Click += StateButton_Click;
            }
        }

        private static void OnIsCloseButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var button = (Button)d;

            if (e.OldValue is true)
            {
                button.Click -= CloseButton_Click;
            }

            if (e.NewValue is true)
            {
                button.Click -= CloseButton_Click;
                button.Click += CloseButton_Click;
            }
        }

        private static void StateButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (DependencyObject)sender;
            var window = Window.GetWindow(button);
            var state = GetTitleBarButtonState(button);
            if (window != null && state != null)
            {
                window.WindowState = state.Value;
            }
        }

        private static void CloseButton_Click(object sender, RoutedEventArgs e)
            => Window.GetWindow((DependencyObject)sender)?.Close();
    }

    public class UniversalTitleBar
    {
        public Color ForegroundColor { get; set; } = Colors.Black;
        public Color InactiveForegroundColor { get; set; } = Color.FromRgb(0x99, 0x99, 0x99);
        public Color ButtonHoverForeground { get; set; } = Colors.Black;
        public Color ButtonHoverBackground { get; set; } = Color.FromRgb(0xE6, 0xE6, 0xE6);
        public Color ButtonPressedForeground { get; set; } = Colors.Black;
        public Color ButtonPressedBackground { get; set; } = Color.FromRgb(0xCC, 0xCC, 0xCC);
    }
}
