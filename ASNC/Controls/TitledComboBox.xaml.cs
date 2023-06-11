using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ASNC.Controls
{
    /// <summary>
    /// Interaction logic for DataTextBox.xaml
    /// </summary>
    public partial class TitledComboBox : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(TitledComboBox), new PropertyMetadata(null));
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(nameof(IsReadOnly), typeof(bool), typeof(TitledComboBox), new PropertyMetadata(false));
        public static readonly DependencyProperty LowerBorderBrushProperty = DependencyProperty.Register(nameof(LowerBorderBrush), typeof(SolidColorBrush), typeof(TitledComboBox), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(25, 118, 210)), OnLowerBorderBurshChanged));
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(TitledComboBox), new PropertyMetadata());
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(TitledComboBox), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(nameof(SelectedIndex), typeof(int), typeof(TitledComboBox), new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)this.GetValue(ItemsSourceProperty);
            set => this.SetValue(ItemsSourceProperty, value);
        }

        public object SelectedItem
        {
            get => this.GetValue(SelectedItemProperty);
            set => this.SetValue(SelectedItemProperty, value);
        }

        public int SelectedIndex
        {
            get => (int)this.GetValue(SelectedIndexProperty);
            set => this.SetValue(SelectedIndexProperty, value);
        }

        public string Title
        {
            get => (string)this.GetValue(TitleProperty);
            set => this.SetValue(TitleProperty, value);
        }

        public bool IsReadOnly
        {
            get => (bool)this.GetValue(IsReadOnlyProperty);
            set => this.SetValue(IsReadOnlyProperty, value);
        }

        public SolidColorBrush LowerBorderBrush
        {
            get => (SolidColorBrush)this.GetValue(LowerBorderBrushProperty);
            set => this.SetValue(LowerBorderBrushProperty, value);
        }

        public TitledComboBox()
        {
            this.InitializeComponent();
        }

        private static void OnLowerBorderBurshChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TitledComboBox tb)
            {
                tb.LowerBorder.BorderBrush = e.NewValue as SolidColorBrush;
            }
        }
    }
}
