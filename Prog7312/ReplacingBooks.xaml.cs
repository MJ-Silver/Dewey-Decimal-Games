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
    /// Interaction logic for ReplacingBooks.xaml
    /// </summary>
    public partial class ReplacingBooks : Page
    {
     
        public string[] author = new string[] {"JAM","PAM","EAT","MSC","LEC","VET","SAI","EAP","TTW","MTB","POE", "EST","LOP","FRM","FRD"};
        List<books> sortedNumbers = new List<books>();//list for storing sorted numbers
        List<books> lstUsernumbers = new List<books>();//list for displaying random numbers to user
        List<books> lstDuplicated = new List<books>();//list for comparing
        List<books> lstUserInput = new List<books>();//list for storing user input
        string currentItem = string.Empty;//storing current items
        int index = 0;//int for finding index of list
        string cheat = "";//for finding cheat information for list
        //constructro for points 
        int pointToAdd = 10;//number of points the user will get
        private MainWindow _mw;
        public ReplacingBooks(MainWindow mw)
        {
            InitializeComponent();
            _mw = mw;
            try//error handling
            {
                generateNumbers();//calling method to generate number
                lstUnsorted.ItemsSource = lstUsernumbers;//bingin lists for compare
                lstDuplicated = new List<books>(lstUsernumbers);//storing lists for comparison
                listSort(sortedNumbers);//sorting the list using buble sort
                prgItem.Value = lstUserInput.Count;//progress bar
               
                foreach (books book in sortedNumbers)
                {
                    cheat += book.ToString() + "\n";//getting sorted information with cheat
                }
                txtCheat.Text = cheat;
                txtCheat.Visibility = Visibility.Hidden;//making text visible for cheat


                txtPoints.Text = "Points: " + userPoints.points.ToString();//showing user points
            }catch(Exception error)
            {
                MessageBox.Show("An Error has occured!");
            }
}

        public void generateNumbers()
        {   Random random = new Random();
              Random random2 = new Random();
            Random random3 = new Random();
            for(int i = 0; i < 10; i++)//populating array with random numbers 
            {            
               //getting random interger
                double dec = random.Next(100, 1000)+random2.NextDouble();//getting decimal
                dec = Math.Round(dec, 2);//rounding to 2 places             
            //stroing in list
                books bk = new books(dec, author[random.Next(0,14)]);
                sortedNumbers.Add(bk);
                lstUsernumbers.Add(bk);
            }
        }

        public void listSort(List<books> lst)//using bubble sort alogrithm to sort list
        {
            for (int j = 0; j <= lst.Count - 2; j++)//sortin numbers
            {
                for (int i = 0; i <= lst.Count - 2; i++)
                {
                    if (lst[i].randomNum > lst[i + 1].randomNum)
                    {
                        var temp = lst[i + 1];
                        lst[i + 1] = lst[i];
                        lst[i] = temp;

                    }
                    if (lst[i].randomNum == lst[i + 1].randomNum)//sorting names if numbers are the same
                    {
                        if (String.Compare(lst[i].author, lst[i + 1].author) > 0)
                        {
                            var temp = lst[i + 1];
                            lst[i + 1] = lst[i];
                            lst[i] = temp;
                        }
                    }
                }
            }
        }
        
        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            currentItem = lstUnsorted.SelectedValue.ToString();
            index = lstUnsorted.SelectedIndex;//storing list information and index
            lstSorted.Items.Add(currentItem);//adding list items to listbox
            if(lstUsernumbers != null)//checking if list is not null
            { 
                lstUserInput.Add(lstUsernumbers[index]);
                lstUsernumbers.RemoveAt(index);
               
            }
            bindnewlist();//refershing list
                prgItem.Value = lstUserInput.Count;//updaing progress bar

            }
            catch (Exception error)
            {
                MessageBox.Show("An Error has occured!");
            }
        }
 
        private void bindnewlist()
        {//rebing listing for update items
            lstUnsorted.ItemsSource = null;
            lstUnsorted.ItemsSource = lstUsernumbers;
            
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //clearing all lists and resetting for the user
            
            lstUsernumbers.Clear();
            lstUserInput.Clear();
            lstUsernumbers = new List<books>(lstDuplicated);
            bindnewlist();
            lstSorted.ItemsSource = null ;
            lstSorted.Items.Clear();
              prgItem.Value = lstUserInput.Count;
            }
            catch (Exception error)
            {
                MessageBox.Show("An Error has occured!");
            }


        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try { 
          
            if(Enumerable.SequenceEqual(lstUserInput,sortedNumbers))//comparing the user input and sorted list
            {
                userPoints.points += pointToAdd;
                MessageBox.Show("You Won: " + pointToAdd.ToString()+" points!");//adding points and displaying to user

            }
            else
            {
                MessageBox.Show("Try Again");//telling the user is wrong
            }

            txtPoints.Text = "Points: "+userPoints.points;
            }catch(Exception error)
            {
                MessageBox.Show("An Error has occured!");
            }
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            try//error handling
            {
                lstSorted.ItemsSource = null;
                lstUsernumbers.Add(lstUserInput[lstSorted.SelectedIndex]);//back button sending items back to list from user list
                lstUserInput.RemoveAt(lstSorted.SelectedIndex);
                lstSorted.Items.RemoveAt(lstSorted.SelectedIndex);
                bindnewlist();
                prgItem.Value = lstUserInput.Count;
            }catch(Exception error)
            {
                MessageBox.Show("Make sure you have selected an item!");
            }

        }

        private void btnCheat_Click(object sender, RoutedEventArgs e)
        {
            if (txtCheat.IsVisible)//creating cheat visbile
            {
                txtCheat.Visibility = Visibility.Hidden;
            }
            else
            {
                txtCheat.Visibility = Visibility.Visible ;
                pointToAdd = 5;//chaning poitns if user is cheating

            }
        
        }

        private void btnHomeReplacing_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
