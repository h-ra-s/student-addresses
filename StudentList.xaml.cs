using System;
using System.IO;
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

namespace Student_Addresses
{
    /// <summary>
    /// Interaction logic for StudentList.xaml
    /// </summary>
    public partial class wndStudentAdresses : Window
    {
        // Create a field variable to represent a collection (List) of
        // Student objects
        //field variable for collection of Student objects
        List<Student> studentObjects = new List<Student>();

        public wndStudentAdresses()
        {
            InitializeComponent();

            // Call method to Get Data from Disk File
            GetDataFromDiskFile();
        }

        private void GetDataFromDiskFile()
        {
            try
            {
                //if disk file is not available
                if (File.Exists("StudentData.txt") == false)
                {
                    //messagebox to alert user
                    MessageBox.Show("StudentData.txt is missing, create a new contact list.", "User Information");

                    //exit
                    return;
                }

                //if the file was found:
                //define StreamReader var to read file data to
                StreamReader inputFile;

                //open text file to StreamReader var
                inputFile = File.OpenText("StudentData.txt");

                //string var for reading line of file
                string strInput, strOutput;

                //while not at end of file:
                while(inputFile.EndOfStream == false)
                {
                    //reads stream of characters until \n is hit
                    strInput = inputFile.ReadLine();

                    //tokenize string with | as delimiter (" or ' can be used)
                    string[] StudentData = strInput.Split('|');
                    //StudentData[0] is StudentID

                    //create instance of student object
                    Student student = new Student();

                    //update properties of Student object
                    student.StudentID = StudentData[0];
                    student.FirstName = StudentData[1];
                    student.LastName = StudentData[2];
                    student.Address = StudentData[3];
                    student.City = StudentData[4];
                    student.State = StudentData[5];
                    student.ZipCode = StudentData[6];

                    //add student object to the List of student objects
                    studentObjects.Add(student);

                    //add StudentID to string in order to add to list box control
                    strOutput = student.StudentID;

                    //add strOutput string (holding StudentID) to lstbox
                    lstStudentsID.Items.Add(strOutput);
                }

                //close disk file
                inputFile.Close();
            }
            //catch exceptions
            catch (FileNotFoundException)
            {
                MessageBox.Show("StudentData.txt not found.", "File Not Found");
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Directory of StudentData.txt" + "not found.", "Directory Not Found");
            }
            catch (IOException ex)//could be multiple issues, so variable "ex" represents an instance or IOException
            {
                MessageBox.Show(ex.Message, "IOException");
            }
            catch (Exception ex)//catches all other exceptions
            {
                MessageBox.Show("Disk problem, please note the error message \n" + ex.Message, "Exception");
            }

            // Select the first Student ID in ListBox control
            lstStudentsID.SelectedIndex = 0;
        }
       
        private void lstStudentsID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //define new student object variable
            Student StudentInfo;

            //set var to SelectedIndex of lstbox
            int intIndex = lstStudentsID.SelectedIndex;

            //set index of StudentInfo to match student selected in lstbox
            StudentInfo = studentObjects[intIndex];

            //populate lblFullName
            lblFullName.Content = StudentInfo.FirstName + " " + StudentInfo.LastName;

            //populate lblAddress
            lblAddress.Content = StudentInfo.Address + "\n" +
                StudentInfo.City + ", " + StudentInfo.State + "  " +
                StudentInfo.ZipCode;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            // close window
            this.Close();
        }
    }
}
