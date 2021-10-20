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
using System.IO;
namespace FILESTREAMER_WPF
{
    /// <summary>
    /// Interaction logic for Contenido.xaml
    /// </summary>
    public partial class Contenido : Window
    {
        string File;
        byte l;
        FileStream d;
        string r;
        public Contenido(string file, byte a)
        {
            InitializeComponent();
            tbContenido.LostFocus += (sender, e)=>{
                if (string.IsNullOrWhiteSpace(tbContenido.Text))
                {
                    tbContenido.Text = "Ingrese contenido aqui";
                }
            };
            tbContenido.GotFocus += (sender, e) =>
            {
                if(tbContenido.Text == "Ingrese contenido aqui")
                {
                    tbContenido.Text = "";
                }
            };
            this.KeyDown += (sender, e) =>
            {
                if(e.Key == Key.Enter)
                {
                    r += "\n";
                }
            };
            File = file;
            if (a == 0)
            { btn.Visibility = Visibility.Visible; btnDel.Visibility = Visibility.Hidden; }
            else
            {
                d = new FileStream(file, FileMode.Open, FileAccess.Read);
                tbContenido.Text = new StreamReader(d).ReadToEnd();
                btn.Visibility = Visibility.Hidden;
                btnDel.Visibility = Visibility.Visible;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            d = new FileStream(File, FileMode.Append);
            d.Write(Encoding.ASCII.GetBytes(r), 0, Encoding.ASCII.GetBytes(r).Length);
            d.Close();
            this.Close();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            d.Close();
            MainWindow.del(File);
            this.Close();
        }

        private void TbContenido_TextChanged(object sender, TextChangedEventArgs e)
        {
            r += tbContenido.Text[tbContenido.Text.Length-1];
        }
    }
}
