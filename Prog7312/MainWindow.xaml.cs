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
//ST10121065 Michael Silver
namespace Prog7312
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
        }

  

        private void btnReplacingBooks_Click(object sender, RoutedEventArgs e)
        {
            ReplacingBooks Replacingbooks = new ReplacingBooks(this);
            this.Content = Replacingbooks;
        }

        private void btnAreas_Click(object sender, RoutedEventArgs e)
        {
            identifyingAreas identifyingAreas = new identifyingAreas(this);
           this.Content = identifyingAreas;
        }

        private void btnFindingnum2_Click(object sender, RoutedEventArgs e)
        {
            callingNumbers cn = new callingNumbers();
            this.Content = cn;

        }
    }
}
