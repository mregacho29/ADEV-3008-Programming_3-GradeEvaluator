using BITCollege_MR.Data;
using BITCollege_MR.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BITCollegeWindows
{
    public partial class History : Form
    {
        /// <summary>
        /// Instantiate an object of your BITCollege_FLContext clas
        /// </summary>
        BITCollege_MRContext db = new BITCollege_MRContext();

        ///given:  student and registration data will passed throughout 
        ///application. This object will be used to store the current
        ///student and selected registration
        ConstructorData constructorData;



        /// <summary>
        /// given:  This constructor will be used when called from the
        /// Student form.  This constructor will receive 
        /// specific information about the student and registration
        /// further code required:  
        /// </summary>
        /// <param name="constructorData">constructorData object containing
        /// specific student and registration data.</param>
        public History(ConstructorData constructorData)
        {
            InitializeComponent();


            /// Store the received constructorData in the form's constructorData object
            this.constructorData = constructorData;


            // Populate upper controls
            studentNumberMaskedLabel.Text = constructorData.Student.StudentNumber.ToString();
            fullNameLabels.Text = constructorData.Student.FullName;
            descriptionLabels.Text = constructorData.Student.AcademicProgram.Description;


            // Add more controls as needed


        }







        /// <summary>
        /// given: This code will navigate back to the Student form with
        /// the specific student and registration data that launched
        /// this form.
        /// </summary>
        private void lnkReturn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //return to student with the data selected for this form
            StudentData student = new StudentData(constructorData);
            student.MdiParent = this.MdiParent;
            student.Show();
            this.Close();
        }

        /// <summary>
        /// given:  Open this form in top right corner of the frame.
        /// further code required:
        /// </summary>
        private void History_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            try
            {
                // Define the LINQ-to-SQL query
                var query = from registration in db.Registrations
                            join course in db.Courses
                            on registration.CourseId equals course.CourseId
                            where registration.StudentId == constructorData.Student.StudentId
                            select new
                            {
                                registration.RegistrationNumber,
                                registration.RegistrationDate,
                                course.Title,
                                registration.Grade,
                                registration.Notes
                            };

                // Set the DataSource property of the BindingSource object
                registrationBindingSource.DataSource = query.ToList();

            }
            catch (Exception ex)
            {

                // Show a MessageBox with the exception details
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}
