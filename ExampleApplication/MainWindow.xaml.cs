using System.Windows;

namespace ExampleApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PreviewRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ResultsPreviewControl.GetBindingExpression(DataContextProperty)?.UpdateTarget();
            ResultsPreviewControl.GetBindingExpression(ContentProperty)?.UpdateTarget();
        }
    }
}