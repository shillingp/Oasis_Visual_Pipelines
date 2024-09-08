using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Oasis_Visual_Pipelines.Controls
{
    public class EditableTextBox : TextBox
    {
        public Style ReadOnlyStyle
        {
            get { return (Style)GetValue(ReadOnlyStyleProperty); }
            set { SetValue(ReadOnlyStyleProperty, value); }
        }

        public static readonly DependencyProperty ReadOnlyStyleProperty =
            DependencyProperty.Register("ReadOnlyStyle", typeof(Style), typeof(EditableTextBox), new PropertyMetadata(null));

        public Style EditingStyle
        {
            get { return (Style)GetValue(EditingStyleProperty); }
            set { SetValue(EditingStyleProperty, value); }
        }

        public static readonly DependencyProperty EditingStyleProperty =
            DependencyProperty.Register("EditingStyle", typeof(Style), typeof(EditableTextBox), new PropertyMetadata(null));

        public EditableTextBox() : base()
        {
            this.IsReadOnly = true;
            Loaded += EditableTextBox_Loaded;
        }

        private void EditableTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            this.Style = ReadOnlyStyle;
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            this.IsReadOnly = false;
            this.Style = EditingStyle;
            this.Select(0, 0);
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            this.IsReadOnly = true;
            this.Style = ReadOnlyStyle;
            base.OnLostFocus(e);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (e.Key == Key.Enter)
                FocusManager.SetFocusedElement(Application.Current.MainWindow, Application.Current.MainWindow);
        }
    }
}
