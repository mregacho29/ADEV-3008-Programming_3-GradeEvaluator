using BITCollege_MR.Data;
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
    public partial class BatchUpdate : Form
    {
        // Define a private instance of the BIT_College Context, and Batch classes
        BITCollege_MRContext db = new BITCollege_MRContext();
        private Batch batch = new Batch();

        /// <summary>
        /// Initializes a new instance of the Batchupdate class
        /// </summary>
        public BatchUpdate()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Batch processing
        /// Further code to be added.
        /// </summary>
        private void lnkProcess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (radSelect.Checked)
            {
                // Single transmission selection
                string programAcronym = descriptionComboBox.SelectedValue.ToString();

                // Call the ProcessTransmission method of the Batch class
                batch.ProcessTransmission(programAcronym);

                // Call the WriteLogData method of the Batch class
                string logData = batch.WriteLogData();

                // Append the return value to the richText control’s Text property
                rtxtLog.AppendText(logData + "\n");
            }
            else if (radAll.Checked)
            {
                // All transmissions selection
                foreach (var item in descriptionComboBox.Items)
                {
                    var program = (dynamic)item;
                    string programAcronym = program.ProgramAcronym;

                    // Call the ProcessTransmission method of the Batch class
                    batch.ProcessTransmission(programAcronym);

                    // Call the WriteLogData method of the Batch class
                    string logData = batch.WriteLogData();

                    // Append the return value to the richText control’s Text property
                    rtxtLog.AppendText(logData + "\n");
                }
            }
        }





        /// <summary>
        /// given:  Always open this form in top right of frame.
        /// Further code to be added.
        /// </summary>
        private void BatchUpdate_Load(object sender, EventArgs e)
        {
            // Populate the BindingSource with a LINQ-to-SQL Server query retrieving all records from the AcademicPrograms table
            var programs = db.AcademicPrograms.ToList();
            academicProgramBindingSource.DataSource = programs;
        }




        /// <summary>
        /// Handles the CheckedChanged event of the radAll RadioButton.
        /// Enables or disables the descriptionComboBox based on the Checked property of the RadioButton.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">the instance containing the event</param>
        private void radAll_CheckedChanged(object sender, EventArgs e)
        {
            descriptionComboBox.Enabled = radSelect.Checked;
        }

        
    }
}
