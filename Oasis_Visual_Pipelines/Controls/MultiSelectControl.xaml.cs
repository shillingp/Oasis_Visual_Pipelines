using System.Collections;
using System.Collections.Immutable;
using System.Windows;
using System.Windows.Controls;

namespace Oasis_Visual_Pipelines.Controls
{
    /// <summary>
    /// Interaction logic for MultiSelectControl.xaml
    /// </summary>
    public partial class MultiSelectControl : UserControl
    {
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IEnumerable),
                typeof(MultiSelectControl),
                new PropertyMetadata(Enumerable.Empty<object>()));

        public ImmutableHashSet<object> SelectedItems
        {
            get { return (ImmutableHashSet<object>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(
                "SelectedItems",
                typeof(ImmutableHashSet<object>),
                typeof(MultiSelectControl),
                new PropertyMetadata(ImmutableHashSet<object>.Empty));


        public MultiSelectControl()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            object item = ((CheckBox)sender).DataContext;
            SelectedItems = SelectedItems.Add(item);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            object item = ((CheckBox)sender).DataContext;
            SelectedItems = SelectedItems.Remove(item);
        }
    }
}
