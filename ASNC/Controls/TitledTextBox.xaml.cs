﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ASNC.Controls
{
    /// <summary>
    /// Interaction logic for DataTextBox.xaml
    /// </summary>
    public partial class TitledTextBox : UserControl
    {

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(TitledTextBox), new PropertyMetadata(null, OnTitleChanged));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(TitledTextBox), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged));
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(nameof(IsReadOnly), typeof(bool), typeof(TitledTextBox), new PropertyMetadata(false, OnReadOnlyChanged));
        public static readonly DependencyProperty LowerBorderBrushProperty = DependencyProperty.Register(nameof(LowerBorderBrush), typeof(SolidColorBrush), typeof(TitledTextBox), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(25, 118, 210)), OnLowerBorderBurshChanged));

        public string Title
        {
            get => (string)this.GetValue(TitleProperty);
            set => this.SetValue(TitleProperty, value);
        }

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
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

        public TitledTextBox()
        {
            this.InitializeComponent();
            //this.Input.TextChanged += this.Input_TextChanged;
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e) => this.Text = this.Input.Text;

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TitledTextBox tb && e.NewValue is string s)
            {
                //tb.Input.Text = s;
            }
        }

        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TitledTextBox tb && e.NewValue is string s)
            {
                tb.TitleLabel.Content = s;
            }
        }

        private static void OnReadOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TitledTextBox tb && e.NewValue is bool b)
            {
                tb.Input.IsReadOnly = b;
            }
        }

        private static void OnLowerBorderBurshChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TitledTextBox tb)
            {
                tb.LowerBorder.BorderBrush = e.NewValue as SolidColorBrush;
            }
        }
    }
}