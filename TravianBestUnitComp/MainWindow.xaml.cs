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

namespace TravianBestUnitComp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Animals animals;
        public MainWindow()
        {
            InitializeComponent();
            animals = Animals.Instance;
        }

        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            int infDef = ParseTextBoxValue(txtInfDef);
            int cavDef = ParseTextBoxValue(txtCavDef);

            int infOff1Units = ParseTextBoxValue(txtInfOff1Units);
            int infOff1 = ParseTextBoxValue(txtInfOff1);
            int infOff1Cost = ParseTextBoxValue(txtInfOff1Cost);

            int infOff2Units = ParseTextBoxValue(txtInfOff2Units);
            int infOff2 = ParseTextBoxValue(txtInfOff2);
            int infOff2Cost = ParseTextBoxValue(txtInfOff2Cost);

            int cavOff1Units = ParseTextBoxValue(txtCavOff1Units);
            int cavOff1 = ParseTextBoxValue(txtCavOff1);
            int cavOff1Cost = ParseTextBoxValue(txtCavOff1Cost);

            int cavOff2Units = ParseTextBoxValue(txtCavOff2Units);
            int cavOff2 = ParseTextBoxValue(txtCavOff2);
            int cavOff2Cost = ParseTextBoxValue(txtCavOff2Cost);

            string res = "";

            int totalUnits = infOff1Units + infOff2Units + cavOff1Units + cavOff2Units;
            if (totalUnits > 1000)
            {
                res = "Too many units. Put less than 1000 total units.";
                MessageBoxResult msgRes = MessageBox.Show(res, "Calculation Result", MessageBoxButton.OK);
                if (msgRes == MessageBoxResult.OK)
                    return;
            }

            List<Dictionary<string, int>> lossesList = new List<Dictionary<string, int>>();


            for (int i = 0; i <= infOff1Units; i++)
            {
                for (int j = 0; j <= infOff2Units; j++)
                {
                    for (int k = 0; k <= cavOff1Units; k++)
                    {
                        for (int l = 0; l <= cavOff2Units; l++)
                        {
                            double totalInfOff = infOff1 * i + infOff2 * j;
                            double totalCavOff = cavOff1 * k + cavOff2 * l;
                            double totalOff = totalInfOff + totalCavOff;
                            if (totalOff == 0) break;
                            double infOffRatio = totalInfOff / totalOff;
                            double cavOffRatio = totalCavOff / totalOff;

                            double totalDef = infOffRatio * infDef + cavOffRatio * cavDef + 10;
                            if (totalDef > totalOff) break;

                            double offLossPercentage = 100 * Math.Pow((totalDef / totalOff), 1.5);
                            double offLossPercentageRaid = 100 * offLossPercentage / (100 + offLossPercentage);

                            int infOff1UnitLoss = (int)Math.Round(i * (offLossPercentageRaid / 100));
                            int infOff2UnitLoss = (int)Math.Round(j * (offLossPercentageRaid / 100));
                            int cavOff1UnitLoss = (int)Math.Round(k * (offLossPercentageRaid / 100));
                            int cavOff2UnitLoss = (int)Math.Round(l * (offLossPercentageRaid / 100));

                            int infOff1CostLoss = infOff1Cost * infOff1UnitLoss;
                            int infOff2CostLoss = infOff2Cost * infOff2UnitLoss;
                            int cavOff1CostLoss = cavOff1Cost * cavOff1UnitLoss;
                            int cavOff2CostLoss = cavOff2Cost * cavOff2UnitLoss;

                            int totalCostLoss = infOff1CostLoss + infOff2CostLoss + cavOff1CostLoss + cavOff2CostLoss;
                            Dictionary<string, int> leastLosses = new Dictionary<string, int>();
                            leastLosses["infOff1"] = i;
                            leastLosses["infOff2"] = j;
                            leastLosses["cavOff1"] = k;
                            leastLosses["cavOff2"] = l;
                            leastLosses["totalCostLoss"] = totalCostLoss;
                            leastLosses["totalUnits"] = i + j + k + l;
                            lossesList.Add(leastLosses);
                        }
                    }
                }
            }
            List<Dictionary<string, int>> lowestCostLosses = lossesList
                .OrderBy(dict => dict["totalCostLoss"])
                    .ThenBy(dict => dict["totalUnits"])
                .Take(10)
                .ToList();

            foreach (var lowestCostLoss in lowestCostLosses)
                res += $"Infantry 1: {lowestCostLoss["infOff1"]}, Infantry 2: {lowestCostLoss["infOff2"]}, Cavalry 1: {lowestCostLoss["cavOff1"]}, Cavalry 2: {lowestCostLoss["cavOff2"]}, Total Loss: {lowestCostLoss["totalCostLoss"]}" + Environment.NewLine;
            MessageBox.Show(res, "Calculation Result", MessageBoxButton.OK);
        }
        private int ParseTextBoxValue(TextBox textBox)
        {
            return int.TryParse(textBox.Text, out int result) ? result : 0;
        }

        private void btnFillAnimals_Click(object sender, RoutedEventArgs e)
        {
           
                FillAnimalsWindow fillAnimalsWindow = new FillAnimalsWindow();
                fillAnimalsWindow.Closed += fillAnimalsWindow_Closed;
                fillAnimalsWindow.Show();
        }
        private void fillAnimalsWindow_Closed(object sender, EventArgs e)
        {
            int infantryDefense = 0;
            int cavalryDefense = 0;
            for (int i = 0; i < 10; i++)
            {
                infantryDefense += getDefense(i, 0) * animals.animals[i];
                cavalryDefense += getDefense(i, 1) * animals.animals[i];
            }
            txtInfDef.Text = infantryDefense.ToString();
            txtCavDef.Text = cavalryDefense.ToString();
        }

        private int getDefense(int index, int type)
        {
            int[,] table = new int[,]
            {
                { 25, 20 },
                { 35, 40 },
                { 40, 60 },
                { 66, 50 },
                { 70, 33 },
                { 80, 70 },
                { 140, 200 },
                { 380, 240 },
                { 170, 250 },
                { 440, 520 }
            };
            return table[index, type];
        }
    }
}
