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

namespace TravianBestUnitComp
{
    /// <summary>
    /// Interaction logic for FillAnimalsWindow.xaml
    /// </summary>
    public partial class FillAnimalsWindow : Window
    {
        Animals animals;
        public FillAnimalsWindow()
        {
            InitializeComponent();
            animals = Animals.Instance;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            int rats = ParseTextBoxValue(txtRat);
            int spiders = ParseTextBoxValue(txtSpider);
            int snakes = ParseTextBoxValue(txtSnake);
            int bats = ParseTextBoxValue(txtBat);
            int boars = ParseTextBoxValue(txtBoar);
            int wolfs = ParseTextBoxValue(txtWolf);
            int bears = ParseTextBoxValue(txtBear);
            int crocs = ParseTextBoxValue(txtCroc);
            int tigers = ParseTextBoxValue(txtTiger);
            int elephants = ParseTextBoxValue(txtElephant);

            animals.animals[0] = rats;
            animals.animals[1] = spiders;
            animals.animals[2] = snakes;
            animals.animals[3] = bats;
            animals.animals[4] = boars;
            animals.animals[5] = wolfs;
            animals.animals[6] = bears;
            animals.animals[7] = crocs;
            animals.animals[8] = tigers;
            animals.animals[9] = elephants;

            this.Close();
        }

        private int ParseTextBoxValue(TextBox textBox)
        {
            return int.TryParse(textBox.Text, out int result) ? result : 0;
        }
    }
}
