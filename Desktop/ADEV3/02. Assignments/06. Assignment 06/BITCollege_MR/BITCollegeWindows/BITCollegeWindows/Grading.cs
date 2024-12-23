using BITCollege_MR.Data;
using BITCollege_MR.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;
using static System.Windows.Forms.AxHost;


namespace BITCollegeWindows
{
    public partial class Grading : Form
    {
        /// <summary>
        /// Instantiate an object of your BITCollege_FLContext class
        /// </summary>
        BITCollege_MRContext db = new BITCollege_MRContext();

        /// <summary>
        /// Instantiate an object of BITCollegeWindows class
        /// </summary>
        ServiceReference1.CollegeRegistrationClient service =
            new ServiceReference1.CollegeRegistrationClient();

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
        public Grading(ConstructorData constructor)
        {
            InitializeComponent();


            this.constructorData = constructor;


            // Populate upper controls
            studentNumberMaskedLabel.Text = constructor.Student.StudentNumber.ToString();
            fullNameLabels.Text = constructor.Student.FullName;
            descriptionLabels.Text = constructor.Student.AcademicProgram.Description;


            // Populate lower controls
            courseNumberMaskedLabel.Text = constructor.Registration.Course.CourseNumber.ToString();
            courseTitleLabels.Text = constructor.Registration.Course.Title;
            courseTypeLabels.Text = constructor.Registration.Course.CourseType;
            gradeTextBox.Text = constructor.Registration.Grade?.ToString("P2") ?? string.Empty;

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
        /// given:  Always open in this form in the top right corner of the frame.
        /// further code required:
        /// </summary>
        private void Grading_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);


            // Use the CourseFormat method to set the Course Number MaskedLabel mask
            courseNumberMaskedLabel.Mask = Utility.BusinessRules.CourseFormat(constructorData.Registration.Course.CourseType.ToString());




            // Assume constructorData.Registration.Grade is the property that holds the grade
            if (constructorData.Registration.Grade != null)
            {
                // If a grade has previously been entered
                gradeTextBox.Enabled = false;
                lnkUpdate.Enabled = false;
                lblExisting.Visible = true;
            }
            else
            {
                // If no grade has been previously entered
                gradeTextBox.Enabled = true;
                lnkUpdate.Enabled = true;
                lblExisting.Visible = false;
            }


        }


        /// <summary>
        /// Handles the LinkClicked event of the lnkUpdate control.
        /// Retrieves the grade from the TextBox, validates it, and updates the grade using a WCF Web Service if valid.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">the instance containing the data</param>
        private void lnkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {



            // Retrieve the value from the TextBox
            string gradeText = gradeTextBox.Text;

            // Use the helper method from the Utility project's Numeric class to remove the percent formatting
            string gradeWithoutPercent = Utility.Numeric.ClearFormatting(gradeText, "%");

            // Use the given functionality in the Utility project's Numeric class to ensure that the above value is numeric
            if (!Utility.Numeric.IsNumeric(gradeWithoutPercent, NumberStyles.AllowDecimalPoint))
            {
                MessageBox.Show("The grade must be a numeric value.", "Invalid Grade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // If the input value is numeric
            if (Utility.Numeric.IsNumeric(gradeWithoutPercent, NumberStyles.AllowDecimalPoint))
            {
                // Divide the value by 100 to convert it to a decimal value between 0 and 1
                double grade = Convert.ToDouble(gradeWithoutPercent) / 100;

                // Ensure the value is within the 0 - 1 range of numeric values
                if (grade < 0 || grade > 1)
                {
                    // If not within the range, display an error message to the end user and do not proceed with the update
                    MessageBox.Show("Please enter a grade value between 0 and 1. The grade must be entered as a decimal value such that it appropriately displays when formatted as a percent.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                //If the data is within the proper range of values, use the Client Endpoint of
                //the WCF Web Service(created in an earlier assignment) to update the
                //Grade, the Student’s GradePointAverage and corresponding Grade Point
                //State.
                int registrationId = constructorData.Registration.RegistrationId;
                string notes = constructorData.Registration.Notes;
                service.UpdateGrade(grade, registrationId, notes);

            }

            // Disable the Grade TextBox
            gradeTextBox.Enabled = false;

        }
    }
}
