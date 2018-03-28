using Xceed.Wpf.Toolkit;
using Xceed.Wpf.DataGrid;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Threading;
using System.IO;
namespace PengingatTugasWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> jadwal = new List<string>();
        DataTable dt = new DataTable();
        List<bool> _1jam = new List<bool>();
        List<DateTime> waktu = new List<DateTime>();
        public MainWindow()
        {
            InitializeComponent();
            tugas.Text = "Input Tugas";
            deadline.Value = DateTime.Now;
            try
            {
                string l;
                using (StreamReader sr = new StreamReader("tugas.txt"))
                {
                    while ((l = sr.ReadLine()) != null)
                    {
                        jadwal.Add(l);
                    }
                    sr.Close();
                }
            }
            
            catch {
                using (StreamWriter sw = new StreamWriter("tugas.txt")) {
                    sw.Close();
                }
            }
            dt.Columns.Add("Tugas");
            dt.Columns.Add("Deadline");
            int z = 0;
            for (int x = 0; x < jadwal.Count / 2; x++) {
                dt.Rows.Add(jadwal[z], jadwal[z + 1]);
                z += 2;
            }
            z = 1;
            while (z < jadwal.Count) {
                waktu.Add(DateTime.ParseExact(jadwal[z], "dd/MM/yyyy HH.mm.ss", null));
                _1jam.Add(false);
                z += 2;
            }
            Data.DataContext = dt;
            var window = SystemParameters.WorkArea;
            this.Left = (window.Width / 2)-(this.Width/2);
            this.Top = window.Height - this.Height;
            DispatcherTimer Timer1 = new DispatcherTimer();
            Timer1.Tick += new EventHandler(Timer1_Tick);
            Timer1.Interval = new TimeSpan(0, 0, 1);
            Timer1.Start();
            
        }
        public void tambah(String nama,String waktu)
        {
            dt.Rows.Add(nama, waktu);
            this.waktu.Add(DateTime.ParseExact(waktu, "dd/MM/yyyy HH.mm.ss",null));
            _1jam.Add(false);
            jadwal.Add(nama);
            jadwal.Add(waktu);
        }
        private void Timer1_Tick(object sender, EventArgs e) {
            
            if (waktu.Count > 0) {
                for (int x = 0; x < waktu.Count; x++) {
                    if (waktu[x].Date==DateTime.Now.Date&& waktu[x].Month == DateTime.Now.Month && waktu[x].Year == DateTime.Now.Year) {
                        if (waktu[x].Hour-DateTime.Now.Hour == 1 && _1jam[x] == false)
                        {
                            System.Windows.MessageBox.Show("Tugas " + dt.Rows[x][0] + " tersisa kurang dari 1 jam");
                            _1jam[x] = true;
                        }
                    }
                    else if (waktu[x] <= DateTime.Now) {
                        string t = dt.Rows[x][0].ToString();
                        hapus(x);
                        System.Windows.MessageBox.Show("Batas waktu " + t + " sudah habis");
                    }
                    
                }
            }
        }

        private void Tambah(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (deadline.Value < DateTime.Now)
                {
                    System.Windows.MessageBox.Show("Masukkan waktu yang benar");
                    tugas.Text = "Input Tugas";
                }
                else
                {
                    if (tugas.Text.ToUpper() == "INPUT TUGAS")
                    {
                        System.Windows.MessageBox.Show("Input harus diisi");
                    }
                    else
                    {
                        tambah(tugas.Text, deadline.Value.ToString());
                        tugas.Text = "Input Tugas";
                        deadline.Value = DateTime.Now;
                    }
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Input harus diisi");
            }
            
        }

        private void Save(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("tugas.txt")) {
                for (int x = 0; x < jadwal.Count; x++) {
                    sw.WriteLine(jadwal[x]);
                }
                sw.Close();
           }
        }

        private void TextboxNotClicked(object sender, RoutedEventArgs e)
        {
            if (tugas.Text == "") {
                tugas.Text = "Input Tugas";
            }
        }

        private void TextBoxClicked(object sender, RoutedEventArgs e)
        {
            if (tugas.Text == "Input Tugas") {
                tugas.Text = "";
            }
        }

        private void TextHapusClicked(object sender, RoutedEventArgs e)
        {
            if (Hapus.Text == "Input di sini") {
                Hapus.Text = "";
            }
        }

        private void HapusBaris(object sender, RoutedEventArgs e)
        {
            try {
                hapus(Convert.ToInt32(Hapus.Text)-1);
                Hapus.Text = "";
                System.Windows.MessageBox.Show("Hapus berhasil");
            }
            catch {
                System.Windows.MessageBox.Show("Input salah");
            }
        }
        private void hapus(int baris) {
                dt.Rows[baris].Delete();
                _1jam.RemoveAt(baris);
                jadwal.Clear();
                waktu.Clear();
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    for (int y = 0; y < 2; y++)
                    {
                        jadwal.Add(dt.Rows[x][y].ToString());
                    }
                }
                int z = 1;
                while (z <= jadwal.Count)
                {
                    waktu.Add(DateTime.ParseExact(jadwal[z], "dd/MM/yyyy HH.mm.ss", null));
                    z += 2;
                }
            
        }
        private void HapusTextNotClicked(object sender, RoutedEventArgs e)
        {
            
            if (Hapus.Text == "") {
                Hapus.Text = "Input di sini";
                
            }
        }
    }
}
