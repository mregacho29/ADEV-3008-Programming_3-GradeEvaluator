using BITCollege_MR.Data;
using BITCollege_MR.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace BITCollegeWindows
{
    public partial class StudentData : Form
    {

        /// <summary>
        /// Instantiate an object of your BITCollege_FLContext class
        /// </summary>
        BITCollege_MRContext db = new BITCollege_MRContext();



        ///Given: Student and Registration data will be retrieved
        ///in this form and passed throughout application
        ///These variables will be used to store the current
        ///Student and selected Registration
        ConstructorData constructorData = new ConstructorData();

        /// <summary>
        /// This constructor will be used when this form is opened from
        /// the MDI Frame.
        /// </summary>
        public StudentData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// given:  This constructor will be used when returning to StudentData
        /// from another form.  This constructor will pass back
        /// specific information about the student and registration
        /// based on activites taking place in another form.
        /// </summary>
        /// <param name="constructorData">constructorData object containing
        /// specific student and registration data.</param>
        public StudentData(ConstructorData constructor)
        {
            InitializeComponent();
            //Further code to be added.

            // Set the constructorData instance variable to the value of the corresponding argument
            this.constructorData = constructor;

            // Set the Student Number MaskedTextBox value using the Student property of the constructor argument
            studentNumberMaskedTextBox.Text = constructor.Student.StudentNumber.ToString();

            // Call the MaskedTextBox_Leave event passing null for each of the event arguments
            studentNumberMaskedTextBox_Leave(null, null);
        }








        /// <summary>
        /// given: Open grading form passing constructor data.
        /// </summary>
        private void lnkUpdateGrade_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            // Populate constructorData with the current Student and Registration records
            PopulateConstructorData();

            Grading grading = new Grading(constructorData);
            grading.MdiParent = this.MdiParent;
            grading.Show();
            this.Close();
        }


        /// <summary>
        /// given: Open history form passing constructor data.
        /// </summary>
        private void lnkViewDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            // Populate constructorData with the current Student and Registration records
            PopulateConstructorData();

            History history = new History(constructorData);
            history.MdiParent = this.MdiParent;
            history.Show();
            this.Close();
        }







        /// <summary>
        /// a form with the constructor data populated with the current Student record and current Registration record.
        /// </summary>
        private void PopulateConstructorData()
        {
            constructorData.Student = (Student)studentBindingSource.Current;
            constructorData.Registration = (Registration)registrationBindingSource.Current;

        }








        /// <summary>
        /// given:  Opens the form in top right corner of the frame.
        /// </summary>
        private void StudentData_Load(object sender, EventArgs e)
        {


            // Keeps location of form static when opened and closed
            this.Location = new Point(0, 0);

            //IQueryable<Student> studentsQuery = from results in db.Students select results;
            //IQueryable<Registration> registrationQuery = from results in db.Registrations select results;

            //studentBindingSource.DataSource =
            //    (from results in db.Students select results).ToList();


        }















        /// <summary>
        /// Handles the Leave event of the studentNumberMaskedTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The EventArgs instance containing the event data. </param>
        private void studentNumberMaskedTextBox_Leave(object sender, EventArgs e)
        {
            // Instantiate an object of your BITCollege_FLContext class
            BITCollege_MRContext db = new BITCollege_MRContext();


            // Ensure the user has completed the requirements for the Mask
            if (!studentNumberMaskedTextBox.MaskFull)
            {
                MessageBox.Show("Please enter a valid student number in the format XXXX-XXXX.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Set the focus back to the MaskedTextBox
                studentNumberMaskedTextBox.Focus();
                return;
            }




            // Retrieve the student number from the MaskedTextBox
            string studentNumberString = studentNumberMaskedTextBox.Text.Replace("-", ""); // Remove hyphen

            // Define a LINQ-to-SQL query selecting data from the Students table where the StudentNumber matches the value in the MaskedTextBox
            IQueryable<Student> studentsQuery = from student in db.Students
                                                where student.StudentNumber.ToString() == studentNumberString
                                                select student;






            if (!studentsQuery.Any())
            {
                // Disable the Link Labels if no records are retrieved
                lnkUpdateGrade.Enabled = false;
                lnkViewDetails.Enabled = false;

                // Set focus back to the MaskedTextBox
                studentNumberMaskedTextBox.Focus();

                // Clear the Student BindingSource object
                studentBindingSource.DataSource = typeof(Student);

                // Clear the Registration BindingSource object
                registrationBindingSource.DataSource = typeof(Registration);

                // Display a MessageBox indicating that the student number does not exist
                MessageBox.Show($"Student {studentNumberString} does not exist", "Invalid Student Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }



            else
            {

                // Set the DataSource property of the BindingSource object representing the Student controls
                studentBindingSource.DataSource = studentsQuery.ToList();



                // Define a LINQ-to-SQL Server query selecting all Registrations in which the
                // StudentId corresponds to the record represented by the StudentNumber value in the MaskedTextBox
                var registrationQuery = db.Registrations.Where(r => r.StudentId == studentsQuery.FirstOrDefault().StudentId).ToList();


                if (registrationQuery == null)
                {
                    // Disable the LinkLabels if no registrations are found
                    lnkUpdateGrade.Enabled = false;
                    lnkViewDetails.Enabled = false;

                    // Clear the Registration BindingSource object
                    registrationBindingSource.DataSource = typeof(Registration);
                }
                else
                {
                    var registrations = registrationQuery.ToList();

                    if (registrations.Count == 0)
                    {
                        // Disable the LinkLabels if no registrations are found
                        lnkUpdateGrade.Enabled = false;
                        lnkViewDetails.Enabled = false;

                        // Clear the Registration BindingSource object
                        registrationBindingSource.DataSource = typeof(Registration);
                    }
                    else
                    {
                        // Populate the BindingSource object associated with the Registration controls
                        // with the results of the Registration query
                        registrationBindingSource.DataSource = registrations;

                        // If the Registration object of ConstructorData is not null
                        if (constructorData.Registration != null)
                        {
                            // Set the RegistrationNumber Combobox’s text property to the value of the Registration Number
                            registrationNumberComboBox.Text = constructorData.Registration.RegistrationNumber.ToString();
                        }

                        // Enable the LinkLabels if registrations are found
                        lnkUpdateGrade.Enabled = true;
                        lnkViewDetails.Enabled = true;
                    }


                }
            }
        }
    }
}