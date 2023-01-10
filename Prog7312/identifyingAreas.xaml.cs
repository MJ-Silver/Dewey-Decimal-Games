using System;
using System.Collections.Generic;
using System.IO;
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

namespace Prog7312
{
    /// <summary>
    /// Interaction logic for identifyingAreas.xaml
    /// </summary>
    public partial class identifyingAreas : Page
    { 
        List<string> callNumbers = new List<string>();
        List<string> descriptions = new List<string>();
        public int swap = 0;
        Dictionary<string, string> deweyDecimal = new Dictionary<string, string>();
        Dictionary<string, string> controlItems = new Dictionary<string, string>();
        private bool checkpoints = false;
        private MainWindow _mw;

        public identifyingAreas(MainWindow mw)
        {
            InitializeComponent(); 
            _mw = mw;
            readFromFile();
            playGame();
           
            txtPoints.Text = "Points: " + userPoints.points.ToString();//showing user points
        }

        private void reset()
        {
            callNumbers.Clear();
            descriptions.Clear();
            controlItems.Clear();
            checkpoints = false;
           
            txtPoints.Text = "Points: " + userPoints.points.ToString();
            playGame();
        }
        /// <summary>
        /// 
        /// </summary>
        private void playGame()
        {
            randomItems();
            bindGrids();
        }
      
        /// <summary>
        /// 
        /// </summary>
        private void randomItems()
        {
          Random random = new Random();
            List<int> usedNumbers = new List<int>();
            List<int> usedDescriptions = new List<int>();
          

            while (usedNumbers.Count < 4)
            {
                int randomNum = random.Next(0, 10);
                string callNum = randomNum + "00";
                if (!usedNumbers.Contains(randomNum))
                {
                    callNumbers.Add(callNum);
                    controlItems.Add(callNum, deweyDecimal[callNum]);
                    descriptions.Add(deweyDecimal[callNum]);
                    usedDescriptions.Add(randomNum);
                    usedNumbers.Add(randomNum);
                }
            }

            while(usedDescriptions.Count < 7)
            {
                int randomNum = random.Next(0, 10);
                string callNum = randomNum + "00";
 
                if (!usedDescriptions.Contains(randomNum))
                {
                    descriptions.Add(deweyDecimal[callNum]);
                    usedDescriptions.Add(randomNum);
                }
            }

            descriptions.Shuffle();



        }



        private void bindGrids()
        {
            
                lstCallNumber.Items.Clear();
                lstDescription.Items.Clear();

                foreach (string callNum in callNumbers)
                {
                    lstCallNumber.Items.Add(callNum);

                }
                foreach (string callDesc in descriptions)
                {
                    lstDescription.Items.Add(callDesc);
                }

            
        }

        public void readFromFile()
        {
           

            //read from file

            using (StreamReader reader = new StreamReader("DeweyDecimalSystem.txt"))
            {

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] lines = line.Split(new char[] { '-' });
                

                    deweyDecimal.Add(lines[0], lines[1]); ;
                }


            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
           int pointToadd = checkItems();
          
            

            if ((checkpoints == false) &&( pointToadd == 4))
            {
                checkpoints = true;
                userPoints.points += pointToadd;
                //add points here
            }
            else
            {
                if(pointToadd == 4)
                {
                MessageBox.Show("You have gained the maxium number of points this round,click replay!");
                }
                
            }
       
            txtPoints.Text = "Points: " + userPoints.points.ToString();//showing user points
        }

        private int checkItems()
        {
            
            Dictionary<string, string> userItems = new Dictionary<string, string>();
            int count = 0;
            int points = 0; 
            try
            {
                for (int i = 0; i < lstCallNumber.Items.Count; i++)
                {
                    userItems.Add(lstCallNumber.Items[i].ToString(), lstDescription.Items[i].ToString());
                }

                foreach (KeyValuePair<string, string> kvp in userItems)
                {
                    if (controlItems.TryGetValue(kvp.Key, out  string secondValue))
                    {
                        if (kvp.Value.Equals(secondValue))
                        {
                            count++;
                        }
                    }
                }
             
                if (count >= 4)
                {
                    MessageBox.Show("Congratulations!");
                    points = 4;
                }
                else
                {
                    MessageBox.Show("You did not get them all right please try again or click replay!");
                    points = 0;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error Try again");
            }
            finally
            {
              
            }
            return points;
        }

        private void btnMoveUp_Click(object sender, RoutedEventArgs e)
        {
            try {
            int selectedIndex = this.lstDescription.SelectedIndex;

            if (selectedIndex > 0)
            {
                string itemToMoveUp = this.descriptions[selectedIndex];
                this.descriptions.RemoveAt(selectedIndex);
                this.descriptions.Insert(selectedIndex - 1, itemToMoveUp);
                this.lstDescription.SelectedIndex = selectedIndex - 1;
            }
            bindGrids();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error Try again");
            }
        }

        private void btnMoveDown_Click(object sender, RoutedEventArgs e)
        {
            try {
                int selectedIndex = this.lstDescription.SelectedIndex;

                if (selectedIndex + 1 > 0 && selectedIndex + 1 != descriptions.Count)
                {
                    string itemToMovedDown = this.descriptions[selectedIndex];
                    this.descriptions.RemoveAt(selectedIndex);
                    this.descriptions.Insert(selectedIndex + 1, itemToMovedDown);
                    this.lstDescription.SelectedIndex = selectedIndex + 1;
                }
                bindGrids();
            }catch(Exception error)
            {
                MessageBox.Show("Error Try again");
            }
            }

        private void btnReplay_Click(object sender, RoutedEventArgs e)
        {
            reset();
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedIndex = this.lstCallNumber.SelectedIndex;

                if (selectedIndex > 0)
                {
                    string itemToMoveUp = this.callNumbers[selectedIndex];
                    this.callNumbers.RemoveAt(selectedIndex);
                    this.callNumbers.Insert(selectedIndex - 1, itemToMoveUp);
                    this.lstCallNumber.SelectedIndex = selectedIndex - 1;
                }
                bindGrids();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error Try again");
            }
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedIndex = this.lstCallNumber.SelectedIndex;

                if (selectedIndex + 1 > 0 && selectedIndex + 1 != callNumbers.Count)
                {
                    string itemToMovedDown = this.callNumbers[selectedIndex];
                    this.callNumbers.RemoveAt(selectedIndex);
                    this.callNumbers.Insert(selectedIndex + 1, itemToMovedDown);
                    this.lstCallNumber.SelectedIndex = selectedIndex + 1;
                }
                bindGrids();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error Try again");
            }
        }

        private void btnSwap_Click(object sender, RoutedEventArgs e)
        {
            if(swap == 0)
            {

          
            lstCallNumber.Items.Clear();
            lstDescription.Items.Clear();

            foreach (string callNum in callNumbers)
            {
                lstDescription.Items.Add(callNum);

            }
            foreach (string callDesc in descriptions)
            {
                lstCallNumber.Items.Add(callDesc);
            }
            
            swap += 1;
        }
            else
            {
                bindGrids();
        swap = 0;
            }
}
    }
}
public static class lstshuffle
{
    private static Random rng = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}