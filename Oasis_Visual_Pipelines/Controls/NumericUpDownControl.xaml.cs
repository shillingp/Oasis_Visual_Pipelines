using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Oasis_Visual_Pipelines.Controls
{
    /// <summary>
    /// Interaction logic for NumericUpDownControl.xaml
    /// </summary>
    public partial class NumericUpDownControl : UserControl
    {
        public int NumberValue
        {
            get { return (int)GetValue(NumberValueProperty); }
            set { SetValue(NumberValueProperty, value); }
        }

        public static readonly DependencyProperty NumberValueProperty =
            DependencyProperty.Register(
                nameof(NumberValue),
                typeof(int),
                typeof(NumericUpDownControl),
                new PropertyMetadata(0));

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                nameof(Maximum),
                typeof(int),
                typeof(NumericUpDownControl),
                new PropertyMetadata(int.MaxValue));

        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(
                nameof(Minimum),
                typeof(int),
                typeof(NumericUpDownControl),
                new PropertyMetadata(int.MinValue));

        public NumericUpDownControl()
        {
            InitializeComponent();
        }

        public ICommand IncrementNumberCommand => new RelayCommand(
            () => NumberValue++,
            () => NumberValue < Maximum);

        public ICommand DecrementNumberCommand => new RelayCommand(
            () => NumberValue--,
            () => NumberValue > Minimum);
    }
}
