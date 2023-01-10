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
    /// Interaction logic for callingNumbers.xaml
    /// </summary>
    public partial class callingNumbers : Page
    {

        public static Dictionary<string, string> Tier1 = new Dictionary<string, string>();
        public static List<string> level1 = new List<string>();
        public static List<string> level2 = new List<string>();
        public static List<string> level3 = new List<string>();
        public int userLevel { get; set; }
        public Node tier2Key { get; set; }
        public string tier1Key { get; set; }
        public Node tier3Key { get; set; }
        Tree treee = new Tree();
     
        public callingNumbers()
        {
            InitializeComponent();       
            deweryReading();
        }
        /// <summary>
        /// Reading from the file the storing into a node and a tree
        /// </summary>
        public void deweryReading()
        {
            lstItemslvl1.IsEnabled = true;
            lstItemslvl2.IsEnabled = true;
            lstItemslvl3.IsEnabled = true;
            lstItemslvl2.Visibility = Visibility.Collapsed;
            txtLevel2.Visibility = Visibility.Collapsed;
            lstItemslvl3.Visibility = Visibility.Collapsed;
            txtLevel3.Visibility = Visibility.Collapsed;
            userLevel = 1;
            Tier1.Clear();
            treee = new Tree();
            level1.Clear();
            level2.Clear();
            level3.Clear();

            txtPoints.Text = "Points: "+userPoints.points.ToString();
            //read from file

            using (StreamReader reader = new StreamReader("DeweyDecimalFull.txt"))
            {

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    int spaceIndex = line.IndexOf("-");
                    string sub = line.Substring(0, spaceIndex);
                    int num = Convert.ToInt32(sub);
                    string information = line.Substring(spaceIndex + 1);

                    if (num % 100 == 0)
                    {
                        treee.startBranch(sub, information);
                        Tier1.Add(sub, information);
                    }
                    else if (num % 10 == 0)
                    {
                        treee.startBranch(sub, information);                     
                    }
                    else
                    {
                        treee.Add(sub, information);
                    }
                }
            }
            
            
            Node nn = treee.Find(treee.Root, selectRandom());


            if (nn == null)
            {
                MessageBox.Show("An error has occured");
            }
            else
            {

                Node n3 = treee.Find(treee.Root, nn.Key);
                tier1Key = nn.Key.Substring(0, 1)+"00";
                tier2Key = n3.Parent;
                tier3Key = n3;

                //generating random pulls
                addingrandomList();
                
                string question = n3.Value;
                if(question.Equals("[Unassigned]") || question.Equals("(Optional number)"))
                {
                    deweryReading();
                }
                else
                {
                    txtWork.Text = question;
                }
               
            }


        }

        /// <summary>
        /// Generating a random number to pull a tier 3 call number
        /// then getting parents to use to get to the final number
        /// </summary>
        private void addingrandomList()
        {
            Random random = new Random();
            List<string> usedNumbers = new List<string>();
            List<string> usedNumbers2 = new List<string>();
            List<string> usedNumbers3 = new List<string>();
            level1.Add(tier1Key);
            level2.Add(tier2Key.Key);
            level3.Add(tier3Key.Key);
            usedNumbers.Add(tier1Key);
            usedNumbers2.Add(tier2Key.Key);
            usedNumbers3.Add(tier3Key.Key);


            while (usedNumbers.Count < 4)
            {
                int randomNum = random.Next(0, 10);
                string callNum = randomNum + "00";
                if (!usedNumbers.Contains(callNum))
                {
                    level1.Add(callNum);                          
                   usedNumbers.Add(callNum);
                }
            }
            listSort(level1);

            while(usedNumbers2.Count < 4)
            {
                int randomNum = random.Next(0, 10);
                int randomNum2 = random.Next(1, 10);
                string callNum = randomNum +randomNum2+ "0";
                if (!usedNumbers2.Contains(callNum)) 
                {
                    level2.Add(callNum);
                    usedNumbers2.Add(callNum);
                }
            }
            listSort(level2);


            while (usedNumbers3.Count < 4)
            {  
                int randomNum = random.Next(0, 10);
                int randomNum2 = random.Next(0, 10);
                int randomNum3 = random.Next(1, 10);
                string callNum = randomNum.ToString() + randomNum2.ToString() + randomNum3.ToString();
                if (!usedNumbers3.Contains(callNum))
                {
                    level3.Add(callNum);
                    usedNumbers3.Add(callNum);
                }
            }
            listSort(level3);

            bindgrids();

        }

        /// <summary>
        /// Putting items in the list boxs
        /// </summary>
        private void bindgrids()
        {
            lstItemslvl1.Items.Clear();
            lstItemslvl2.Items.Clear();
            lstItemslvl3.Items.Clear();

          

            foreach (string lvl1 in level1)
            {
               
                lstItemslvl1.Items.Add(lvl1 +" "+ Tier1[lvl1]);

            }
            foreach (string lvl2 in level2)
            {  string value;

                if(lvl2.Length == 2)
                {
                    value = "0" + lvl2;
                }
                else
                {
                    value = lvl2;
                   
;                }
                if (value.Equals("100"))
                {
                    value = "110";
                }
                Node nn = treee.Find(treee.Root, value);
                lstItemslvl2.Items.Add(value + " " + nn.Value);
  
            }
            foreach (string lvl3 in level3)
            {
                //Node nn = treee.Find(treee.Root, lvl3);
                lstItemslvl3.Items.Add(lvl3);
            }


        }

        /// <summary>
        /// This code will select a random tier 3 call number to user
        /// 
        /// </summary>
        public string selectRandom()
        {
            Random random = new Random();
            int num,num2,num3;
            string fullNum ;
            num = random.Next(0,10);
            num2 = random.Next(0,10);
            num3 = random.Next(1,10);
            fullNum = num.ToString() + num2.ToString() + num3.ToString();//random number
            return fullNum;
        }

        /// <summary>
        /// This is a bubble sort to make sure the items are in numerical order
        /// </summary>
        /// <param name="lst"></param>

        public void listSort(List<string> lst)//using bubble sort alogrithm to sort list
        {
            for (int j = 0; j <= lst.Count - 2; j++)//sortin numbers
            {
                for (int i = 0; i <= lst.Count - 2; i++)
                {
                    if (int.Parse(lst[i]) > int.Parse(lst[i + 1]))
                    {
                        string temp = lst[i + 1];
                        lst[i + 1] = lst[i];
                        lst[i] = temp;

                    }
                  
                }
            }
        }
        /// <summary>
        /// THis code works when the user clicks submit and then will check if the relevant selected items 
        /// is correct in relation to the tree
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (userLevel == 1)
            {
                if (lstItemslvl1.SelectedValue.Equals(tier1Key + " " + Tier1[tier1Key]))
                {
                    lstItemslvl2.Visibility = Visibility.Visible;
                    txtLevel2.Visibility = Visibility.Visible;
                    lstItemslvl1.IsEnabled = false;
                    userLevel = 2;
                    userPoints.points += 1;
                    txtPoints.Text = "Points: "+userPoints.points;
                    MessageBox.Show("Well Done you won 1 point");
                }
                else
                {
                    MessageBox.Show("Try again!");
                    deweryReading();
                }
            }
            else if (userLevel == 2)
            {
                if (lstItemslvl2.SelectedValue.Equals(tier2Key.Key + " " + tier2Key.Value))
                {
                    lstItemslvl3.Visibility = Visibility.Visible;
                    txtLevel3.Visibility = Visibility.Visible;
                    lstItemslvl2.IsEnabled = false;
                    userLevel = 3;
                    MessageBox.Show("Well Done you won 2 points");
                    userPoints.points += 2;
                    txtPoints.Text = "Points: " + userPoints.points;
                }
                else
                {
                    MessageBox.Show("Try again!");
                    deweryReading();
                }

            }
            else if (userLevel == 3)
            {
                if (lstItemslvl3.SelectedValue.Equals(tier3Key.Key))
                {
                    MessageBox.Show("Congratulations, you have won 3 points!");
                    userPoints.points += 3;
                    txtPoints.Text = "Points: " + userPoints.points;
                    userLevel = 3;

                }
                else
                {
                    MessageBox.Show("Try again!");
                    deweryReading();
                }
            }
        }

    

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            deweryReading();
        }
    }

}
