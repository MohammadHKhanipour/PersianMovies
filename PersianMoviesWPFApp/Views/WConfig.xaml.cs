using PersianMoviesWPFApp.UserControls;
using System;
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
using System.Windows.Shapes;

namespace PersianMoviesWPFApp.Views
{
    /// <summary>
    /// Interaction logic for WConfig.xaml
    /// </summary>
    public partial class WConfig : Window
    {
        public WConfig()
        {
            InitializeComponent();
        }

        private void BtnDirectors_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new UCDirectors());
        }

        private void BtnMovieGenres_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new UCMovieGenres());
        }
    }
}
