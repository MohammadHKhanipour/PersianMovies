using Microsoft.Win32;
using PersianMoviesWPFApp.Models;
using PersianMoviesWPFApp.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PersianMoviesWPFApp.Views
{
    /// <summary>
    /// Interaction logic for WMovieAddOrEdit.xaml
    /// </summary>
    public partial class WMovieAddOrEdit : Window
    {
        public Movies Movie = new Movies();
        private DbMovieBankEntities _db = new DbMovieBankEntities();
        OpenFileDialog _dialog = new OpenFileDialog();
        public WMovieAddOrEdit()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var directors = _db.Directors.ToList();
            directors.Insert(0, new Directors() { Id = 0, FullName = "انتخاب کنید" });
            CBDirector.ItemsSource = directors;
            CBDirector.SelectedIndex = 0;
            this.DataContext = Movie;
            if (!string.IsNullOrEmpty(Movie.Poster))
            {
                ImgBackground.Source = ImgPoster.Source = new BitmapImage(new Uri(Variable.ImageFullPath + Movie.Poster));
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_dialog != null && !string.IsNullOrEmpty(_dialog.FileName))
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + "Images\\Movie\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (!string.IsNullOrEmpty(Movie.Poster) && File.Exists(Variable.ImageFullPath + Movie.Poster))
                    File.Delete(Variable.ImageFullPath + Movie.Poster);

                var imageName = Guid.NewGuid().ToString().Replace("-", "");
                var ext = System.IO.Path.GetExtension(_dialog.SafeFileName);
                var fullImageName = imageName + ext;
                File.Copy(_dialog.FileName, path + fullImageName);
                Movie.Poster = fullImageName;
            }

            if (Movie.Id == 0)
            {
                Movie.CreateDate = DateTime.Now;
                _db.Movies.Add(Movie);
            }
            else
            {
                var model = _db.Movies.Single(c => c.Id == Movie.Id);
                model.Title = Movie.Title;
                model.Tagline = Movie.Tagline;
                model.Cast = Movie.Cast;
                model.Poster = Movie.Poster;
                model.Description = Movie.Description;
                model.DirectorId = (int)CBDirector.SelectedValue;
                model.ImdbRate = Movie.ImdbRate;
                model.Year = Movie.Year;
            }
            _db.SaveChanges();
            this.DialogResult = true;
        }

        private void BtnPoster_Click(object sender, RoutedEventArgs e)
        {
            _dialog.Filter = "jpg Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png";
            if (_dialog.ShowDialog() == true)
            {
                LblPosterName.Content = _dialog.FileName;
                ImgBackground.Source = ImgPoster.Source = new BitmapImage(new Uri(_dialog.FileName));
            }
        }
    }
}
