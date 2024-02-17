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

namespace Student_Addresses
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class wndAddStudent : Window
    {
        //field variable for collection of Student objects
        List<Student> studentObjects = new List<Student>();
        public wndAddStudent()
        {
            InitializeComponent();

            //Set focus to the first input control
            txtStudentID.Focus();
        }

        private void btnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            //string to hold error message
            string errorMessage = "";

            //validate student ID is in XX-99999 format
            errorMessage += UserValidation.IsValidID(txtStudentID.Text);

            //validate all other fields are filled
            errorMessage += UserValidation.IsPresent(txtFirstName.Text, "First name");
            errorMessage += UserValidation.IsPresent(txtLastName.Text, "Last name");
            errorMessage += UserValidation.IsPresent(txtAddress.Text, "Address");
            errorMessage += UserValidation.IsPresent(txtCity.Text, "City");
            errorMessage += UserValidation.IsPresent(txtState.Text, "State");
            errorMessage += UserValidation.IsPresent(txtZipcode.Text, "Zip code");

            //if an error message was added to the errorMessage string
            if (errorMessage != "")
            {
                //show a message box with the error
                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //if there are no errors
            else
            {
                //creat instance of new student object
                Student Student = new Student();

                //populate student properties with the input
                Student.StudentID = txtStudentID.Text;
                Student.FirstName = txtFirstName.Text;
                Student.LastName = txtLastName.Text;
                Student.Address = txtAddress.Text;
                Student.City = txtCity.Text;
                Student.State = txtState.Text;
                Student.ZipCode = txtZipcode.Text;

                //add properties to collection of student objects
                studentObjects.Add(Student);

                //clear all textbox controls
                txtStudentID.Clear();
                txtFirstName.Clear();
                txtLastName.Clear();
                txtAddress.Clear();
                txtCity.Clear();
                txtState.Clear();
                txtZipcode.Clear();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //if there are no items in the collection
            if(studentObjects.Count == 0)
            {
                //tell user to enter a student before saving
                MessageBox.Show("Please enter a student before saving.", "Students List", MessageBoxButton.OK, MessageBoxImage.Stop);

                //exit the event
                return;
            }

            //if there is at least one item in the collection:
            //try statement for disk file
            try
            {
                //1 - open disk file
                StreamWriter outputFile;

                //2 - create streamwriter instance
                //AmmendText adds on to what is there, CreateText earases it
                outputFile = File.CreateText("StudentData.txt");

                //get number of students in list
                int studentCount = studentObjects.Count;

                //string to layout data
                string strOutput;

                //3 - write to disk file
                    //student refers to each object in studentObjects
                foreach(Student student in studentObjects)
                {
                    //format the data for data file with | separating values
                    strOutput = student.StudentID + "|" + student.FirstName + "|" + student.LastName +
                        "|" + student.Address + "|" + student.City + "|" + student.State +
                        "|" + student.ZipCode;

                    //write to disk file - WriteLine adds \n line for you
                    outputFile.WriteLine(strOutput);
                }

                //4 - close file
                outputFile.Close();
            }
            //catch exceptions:
            catch (FileNotFoundException)
            {
                MessageBox.Show("StudentData.txt not found.", "File Not Found");
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Directory of StudentData.txt" +  "not found.", "Directory Not Found");
            }
            catch (IOException ex)//could be multiple issues, so variable "ex" represents an instance or IOException
            {
                MessageBox.Show(ex.Message, "IOException");
            }
            catch(Exception ex)//catches all other exceptions
            {
                MessageBox.Show("Disk problem, please note the error message \n" + ex.Message, "Exception");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
