using Xceed.Wpf.Toolkit;
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

namespace PengingatTugasWPF
{
    /// <summary>
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public Add()
        {
            InitializeComponent();
        }

        private void Tambah(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault(Window => Window is MainWindow);
            mw.Data.Items.Add(new {judul.Text });
            this.Close();
        }
    }
}
