using System;
using System.Collections;
using System.Collections.Generic;
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

        public IList SelectedItems
        {
            get { return (IList)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(
                "SelectedItems", 
                typeof(IList), 
                typeof(MultiSelectControl), 
                new PropertyMetadata(new HashSet<object>()));

        public MultiSelectControl()
        {
            InitializeComponent();
        }


    }
}
