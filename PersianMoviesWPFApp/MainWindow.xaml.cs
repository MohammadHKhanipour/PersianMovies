using PersianMoviesWPFApp.Models;
using PersianMoviesWPFApp.UserControls;
using PersianMoviesWPFApp.Utilities;
using PersianMoviesWPFApp.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace PersianMoviesWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DbMovieBankEntities _db = new DbMovieBankEntities();
        Movies _movies = new Movies();

        public MainWindow()
        {
            InitializeComponent();
            //foreach (UIElement child in SPMovieList.Children)
            //{
            //    child.MouseDown += Child_MouseDown;
            //    child.MouseWheel += Child_MouseWheel;
            //}
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _movies;
            LoadMovies();
        }

        private void LoadMovies()
        {
            SPMovieList.Children.Clear();
            foreach (var movie in _db.Movies)
            {
                var path = Variable.ImageFullPath;
                BitmapImage poster = null;
                if (!string.IsNullOrEmpty(movie.Poster) && File.Exists(path + movie.Poster))
                    poster = new BitmapImage(new Uri(path + movie.Poster));
                else
                    poster = new BitmapImage(new Uri(Variable.ApplicationPath + Variable.DefaultPoster));

                var uc = new UCImageWithBorder() { Value = movie, Source = poster };
                uc.MouseWheel += Child_MouseWheel;
                uc.MouseDown += Child_MouseDown;
                SPMovieList.Children.Add(uc);
            }
        }

        private void Child_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                SVMovieList.LineLeft();
            else
                SVMovieList.LineRight();
        }

        private void Child_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MainGridPanel.Visibility != Visibility.Visible)
            {
                ImgBackground.Visibility = Visibility.Hidden;
                MainGridPanel.Visibility = Visibility.Visible;
            }

            var uc = (UserControl)sender;
            if (uc.Content is Border border)
                if (border.Tag is Movies movie)
                {
                    this.DataContext = movie;
                    _movies = movie;
                }
                else
                    MessageBox.Show($"tag value: {border.Tag}");
        }

        private void RecTop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();

            if (e.MiddleButton == MouseButtonState.Pressed)
                this.WindowState = this.WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnMoveRight_Click(object sender, RoutedEventArgs e)
        {
            SVMovieList.LineRight();
        }

        private void BtnMoveLeft_Click(object sender, RoutedEventArgs e)
        {
            SVMovieList.LineLeft();
        }

        private void BtnAddMovie_Click(object sender, RoutedEventArgs e)
        {
            var w = new WMovieAddOrEdit() { Owner = this };
            if (w.ShowDialog() == true)
                LoadMovies();
        }

        private void BtnEditMovie_Click(object sender, RoutedEventArgs e)
        {
            var w = new WMovieAddOrEdit() { Owner = this, Movie = _movies };
            w.Title = $"ویرایش{_movies.Title}";
            w.BtnSave.Content = "ویرایش";
            var result = w.ShowDialog();
            DataContext = _movies = _db.Movies.AsNoTracking().SingleOrDefault(c => c.Id == _movies.Id);
            if (result == true)
                LoadMovies();
        }

        private void BtnDeleteMovie_Click(object sender, RoutedEventArgs e)
        {
            if (_movies != null)
            {
                _db.Movies.Remove(_movies);
                _db.SaveChanges();

                //if (!string.IsNullOrEmpty(_movies.Poster) && File.Exists(Variable.ImageFullPath + _movies.Poster))
                //    File.Delete(Variable.ImageFullPath + _movies.Poster);
            }
        }

        private void BtnConfig_Click(object sender, RoutedEventArgs e)
        {
            var w = new WConfig() { Owner = this, WindowStartupLocation=WindowStartupLocation.CenterOwner };
            w.ShowDialog();
        }
    }
}
