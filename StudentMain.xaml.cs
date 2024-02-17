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

namespace Student_Addresses
{
    /// <summary>
    /// Interaction logic for StudentMain.xaml
    /// </summary>
    public partial class StudentMain : Window
    {
        public StudentMain()
        {
            InitializeComponent();
        }

        private void btnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            wndAddStudent wndAdd = new wndAddStudent();

            wndAdd.ShowDialog();
        }

        private void btnViewAllStudents_Click(object sender, RoutedEventArgs e)
        {
            wndStudentAdresses wndStudentList = new wndStudentAdresses();

            wndStudentList.ShowDialog();
        }

        private void btnExitApplication_Click(object sender, RoutedEventArgs e)
        {
            //clost window
            this.Close();
        }
    }
}
