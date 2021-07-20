using PersianMoviesWPFApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace PersianMoviesWPFApp.UserControls
{
    /// <summary>
    /// Interaction logic for UCDirectors.xaml
    /// </summary>
    public partial class UCDirectors : UserControl
    {
        Directors _directors = new Directors();
        DbMovieBankEntities _db = new DbMovieBankEntities();
        public UCDirectors()
        {
            InitializeComponent();
            LoadGrid();
            SPAddDirector.DataContext = _directors;
        }

        private void BtnAddDirector_Click(object sender, RoutedEventArgs e)
        {
            _db.Directors.Add(_directors);
            _db.SaveChanges();
            WPFCustomMessageBox.CustomMessageBox.Show("رکورد جدید با موفقیت ثبت گردید");
            LoadGrid();
        }

        private void LoadGrid()
        {
            GrdList.ItemsSource = _db.Directors.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Tag is Directors director)
                {
                    _db.Entry(director).State = EntityState.Modified;
                    _db.SaveChanges();
                    WPFCustomMessageBox.CustomMessageBox.Show("رکورد مورد نظر با موفقیت ویرایش شد");
                    LoadGrid();
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (WPFCustomMessageBox.CustomMessageBox.ShowYesNo("آیا از حذف این رکورد اطمینان دارید؟", "حذف", "بله", "خیر") == MessageBoxResult.Yes)
                if (sender is Button btn)
                {
                    if (btn.Tag is Directors director)
                    {
                        _db.Entry(director).State = EntityState.Deleted;
                        _db.SaveChanges();
                        WPFCustomMessageBox.CustomMessageBox.Show("رکورد مورد نظر با موفقیت حذف گردید");
                        LoadGrid();
                    }
                }
        }
    }
}
