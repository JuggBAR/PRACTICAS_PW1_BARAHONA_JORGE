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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;
namespace FILESTREAMER_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer k = new DispatcherTimer(DispatcherPriority.Normal);
        static FileStream d;
        string[] g;
        string archivo;
        Contenido f;
        public MainWindow()
        {
            InitializeComponent();
            k.Interval = new TimeSpan(0, 0, 1);
            k.Tick += (a, e) =>
            {
                fill();
            };
            k.Start();
        }
        string file_name;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            file_name = tbfile_name.Text;
            archivo = $@"ARCHIVO\{file_name}.txt";
            if (g.Contains(archivo))
            {
                MessageBoxResult resultado = MessageBox.Show("El archivo ya exite desea sobreescribirlo?", "Error", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultado == MessageBoxResult.Yes)
                {
                    file_stream(archivo, FileMode.Truncate);
                    d.Close();
                    instaciaContenido(archivo,0);
                }
            }
            else
            {
                file_stream(archivo, FileMode.Create);
                fill();
                instaciaContenido(archivo,0);
            }
            
            
        }
        void file_stream(string ar, FileMode fileMode)
        {

            d = new FileStream(ar, fileMode);
            if (fileMode == FileMode.Truncate)
                d.SetLength(0);
            d.Close();
            
        }
        private void CbFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                instaciaContenido(g[cbFiles.SelectedIndex], 1);
            }
            catch
            {
                cbFiles.SelectedIndex = -1;
            }
        }
        void fill()
        {
            g = Directory.GetFiles("ARCHIVO");
            cbFiles.ItemsSource = g;
        }
        void instaciaContenido(string arc, byte a)
        {
            f = new Contenido(arc, a);
            f.ShowDialog();
        }
        public static void del(string ar)
        {
            File.Delete(ar);
        }
    }
}
