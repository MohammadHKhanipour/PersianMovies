using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PersianMoviesWPFApp.UserControls
{
    /// <summary>
    /// Interaction logic for UCImageWithBorder.xaml
    /// </summary>
    public partial class UCImageWithBorder : UserControl
    {
        #region DP Properties

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource),
                typeof(UCImageWithBorder), new PropertyMetadata(null));



        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object),
                typeof(UCImageWithBorder), new PropertyMetadata(null));

        #endregion
        public UCImageWithBorder()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
