using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//the following for linq to xml
using System.Xml.Linq;
//for writing to files
using System.IO;


namespace ADEV3008Windows
{
    public partial class FileIOExample : Form
    {
        public FileIOExample()
        {
            InitializeComponent();
        }

        XDocument xDocument;

        /// <summary>
        /// given
        /// Extracts attribute from xml element
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAttributes_Click(object sender, EventArgs e)
        {
            XElement xElement = xDocument.Element("humanresources");
            XAttribute xAttribute = xElement.Attribute("date");

            rtxtData.Clear();
            rtxtData.Text += "ELEMENT: " + xElement.Name + "\r\nCONTAINS ATTRIBUTE: " +
                xAttribute.Name + "\r\nWITH VALUE: " + xAttribute.Value + "\r\n==============\r\n";

        }

     

        /// <summary>
        /// given
        /// close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// given
        /// read from xml file
        /// get all descendent elements of
        /// humanresources including a count
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnElements_Click(object sender, EventArgs e)
        {
            IEnumerable<XElement> employeeElements = xDocument.Descendants("humanresources");

            rtxtData.Clear();
            rtxtData.Text += "DOCUMENT: InData.xml CONTAINS EMPLOYEES\r\n";

            //note this loop only executes once - everything under human
            //resources is considered one element
            foreach (XElement xele in employeeElements)
            {
                rtxtData.Text += "\r\nNAME: " + xele.Name + " VALUE: " + xele.Value;
            }

            rtxtData.Text += "\r\nCOUNT: " + employeeElements.Count();
        }

        /// <summary>
        /// given
        /// read from xml file
        /// use linq to filter data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilter_Click(object sender, EventArgs e)
        {

            IEnumerable<XElement> filteredElements =
                xDocument.Descendants().Where(d => d.Name == "last");

            rtxtData.Clear();
            rtxtData.Text += "DOCUMENT: InData.xml CONTAINS LAST NAMES\r\n";

            //note this loop executes 3 times
            //one for each occurrence of the last name
            foreach (XElement xele in filteredElements)
            {
                rtxtData.Text += "\r\nNAME: " + xele.Name + " VALUE: " + xele.Value;
            }
            rtxtData.Text += "\r\nCOUNT: " + filteredElements.Count();

        }

        /// <summary>
        /// example of writing to a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWrite_Click(object sender, EventArgs e)
        {
            StreamWriter srLog = new StreamWriter("OutData.txt");

            srLog.Write(rtxtData.Text);
            srLog.Close();

        }


        /// <summary>
        /// given
        /// lambda expression followed by using results
        /// in subsequent lambda expression
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnElementLambda_Click(object sender, EventArgs e)
        {

            IEnumerable<XElement> totalEmployees =
                xDocument.Descendants().Where(d => d.Name == "employee");

            IEnumerable<XElement> highType =
                totalEmployees.Where(highTypes => int.Parse(highTypes.Element("type").Value) > 100);

            rtxtData.Clear();
            rtxtData.Text += "DOCUMENT: InData.xml CONTAINS FIRST NAMES WITH TYPE > 100 \r\n";

            foreach (XElement xele in highType)
            {
                rtxtData.Text += "\r\nNAME: " + xele.Name + " VALUE: " + xele.Value;
            }


            //note:  above can be done in one statement
            IEnumerable<XElement> singleStmt =
                xDocument.Descendants().Where(highTypeVals => int.Parse(highTypeVals.Element("type").Value) > 100);

            rtxtData.Text += "\r\n========================";
            rtxtData.Text += "\r\nDOCUMENT: InData.xml CONTAINS FIRST NAMES WITH TYPE > 100 \r\n";

            foreach (XElement xele in highType)
            {
                rtxtData.Text += "\r\nNAME: " + xele.Name + " VALUE: " + xele.Value;
            }


        }

        /// <summary>
        /// Populate XDocument object with xml file data 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileIOExample_Load(object sender, EventArgs e)
        {
            xDocument = XDocument.Load("InData.xml");
        }

        private void btnHeader_Click(object sender, EventArgs e)
        {

            XElement root = xDocument.Element("humanresources");

            //Attribute Count Check
            if (root.Attributes().Count() != 1)
            {
                rtxtData.Clear();
                rtxtData.Text = "Incorrect number of attributes";

                //throw new Exception
                //(String.Format("Incorrect number of root Attributes" + 
                //    " for file {0}\n", inputFileName));
            }

            //Date Check
            if (!DateTime.Parse(root.Attribute("date").Value)
                .Equals(DateTime.Today))
            {
                rtxtData.Clear();
                rtxtData.Text = "Date in file is not today's date";
                //throw new Exception
                //(String.Format("Incorrect date" + 
                //    " for file {0}\n", inputFileName));
            }

        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            IEnumerable<XElement> totalEmployees = xDocument.Descendants().Where(x => x.Name.Equals("employee"));

            IEnumerable<XElement> childElements = totalEmployees.Where(x => x.Elements().Nodes().Count() == 3);

            //IEnumerable<XElement> nextQuery = childElements.Where(x=> x.Value.Equals("...next validation requirement..."));
        }

        private void btnError_Click(object sender, EventArgs e)
        {
            IEnumerable<XElement> totalEmployees =
                xDocument.Descendants()
                .Where(d => d.Name == "employee");

            IEnumerable<XElement> childElements =
                totalEmployees.Where(x => x.Elements().Nodes()
                .Count() == 3);


            IEnumerable<XElement> errors =
                totalEmployees.Except(childElements);

            foreach (XElement record in errors)
            {
                rtxtData.Text += "\r\nERROR:  ";
                rtxtData.Text += "\r\nIncorrect number of child elements";
                rtxtData.Text += "\r\nFIRST NAME: " + record.Element("first");
                rtxtData.Text += "\r\nLAST NAME: " + record.Element("last");
                rtxtData.Text += "\r\nTYPE: " + record.Element("type");
                rtxtData.Text += "\r\n================\r\n";

            }


            //    IEnumerable<XElement> numericType =
            //        childElements.Where(x =>
            //           isNumeric(x.Element("type").Value, NumberStyles.Integer));

            //    IEnumerable<XElement> nonNumericError =
            //        childElements.Except(numericType);

            //    foreach (XElement record in nonNumericError)
            //    {
            //        rtxtData.Text += "\r\nERROR:  ";
            //        rtxtData.Text += "\r\nNon Numeric Data Type";
            //        rtxtData.Text += "\r\nFIRST NAME: " + record.Element("first");
            //        rtxtData.Text += "\r\nLAST NAME: " + record.Element("last");
            //        rtxtData.Text += "\r\nTYPE: " + record.Element("type");
            //        rtxtData.Text += "\r\n================\r\n";

            //    }

        }
    }

}
