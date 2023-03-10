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

namespace IbeamPluginProject
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private object convert;

        public double Width { get; set; }
        public double Height { get; set; }
        public double Thickness { get; set; }
       
        
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Width = double.Parse(txtwidth.Text);
            Height = double.Parse(txtheight.Text);
            Thickness = double.Parse(txtthickness.Text);
           }
    }
}
