namespace YKMaze.Views.Behaviors
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class KeyDownBehavior
    {
        public static readonly DependencyProperty CallbackProperty = DependencyProperty.RegisterAttached("Callback", typeof(Action<Key, ModifierKeys>), typeof(KeyDownBehavior), new FrameworkPropertyMetadata(null, OnCallBackChanged));
        public static Action<Key, ModifierKeys> GetCallback(DependencyObject target)
        {
            return (Action<Key, ModifierKeys>)target.GetValue(CallbackProperty);
        }
        public static void SetCallback(DependencyObject target, Action<Key, ModifierKeys> value)
        {
            target.SetValue(CallbackProperty, value);
        }
        public static readonly DependencyPropertyKey IsKeyUpPropertyKey = DependencyProperty.RegisterAttachedReadOnly("IsKeyUp", typeof(bool), typeof(KeyDownBehavior), new FrameworkPropertyMetadata(true));
        public static readonly DependencyProperty IsKeyUpProperty = IsKeyUpPropertyKey.DependencyProperty;
        public static bool GetIsKeyUp(DependencyObject target)
        {
            return (bool)target.GetValue(IsKeyUpProperty);
        }
        private static void SetIsKeyUp(DependencyObject target, bool value)
        {
            target.SetValue(IsKeyUpPropertyKey, value);
        }

        private static void OnCallBackChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as UIElement;
            if (control != null)
            {
                var callback = GetCallback(control);
                if (callback != null)
                {
                    control.KeyDown += OnKeyDown;
                    control.KeyUp += OnKeyUp;
                }
                else
                {
                    control.KeyDown -= OnKeyDown;
                    control.KeyUp -= OnKeyUp;
                }
            }
        }

        private static void OnKeyDown(object sender, KeyEventArgs e)
        {
            var isKeyUp = GetIsKeyUp(sender as UIElement);
            if (isKeyUp)
            {
                SetIsKeyUp(sender as UIElement, false);

                var callback = GetCallback(sender as UIElement);
                if (callback != null)
                {
                    callback(e.Key, Keyboard.Modifiers);
                    e.Handled = true;
                }
            }
        }

        private static void OnKeyUp(object sender, KeyEventArgs e)
        {
            var isKeyUp = GetIsKeyUp(sender as UIElement);
            if (!isKeyUp)
            {
                SetIsKeyUp(sender as UIElement, true);
                e.Handled = true;
            }
        }
    }
}
