using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BITCollege_MR.Models;
using BITCollege_MR.Data;
using System.Xml;
using System.Globalization;
using System.Runtime.Remoting.Contexts;
using System.Text.RegularExpressions;

namespace BITCollegeWindows
{
    /// <summary>
    /// Batch:  This class provides functionality that will validate
    /// and process incoming xml files.
    /// </summary>
    public class Batch
    {

        private BITCollege_MRContext db = new BITCollege_MRContext();

        ServiceReference1.CollegeRegistrationClient service =
            new ServiceReference1.CollegeRegistrationClient();


        private String inputFileName;
        private String logFileName;
        private String logData;


        /// <summary>
        /// Filters erroneous records and records the details of that record
        /// </summary>
        /// <param name="beforeQuery">the previous query result set</param>
        /// <param name="afterQuery">the current query result set</param>
        /// <param name="message">the error message</param>
        private void ProcessErrors(IEnumerable<XElement> beforeQuery, IEnumerable<XElement> afterQuery, String message)
        {
            IEnumerable<XElement> errors = beforeQuery.Except(afterQuery);

            foreach (XElement item in errors)
            {
                logData += "\r\n-------ERROR-------";
                logData += "\r\nFile: " + inputFileName;
                logData += "\r\nProgram: " + item.Element("program")?.Value;
                logData += "\r\nStudent Number: " + item.Element("student_no")?.Value;
                logData += "\r\nCourse Number: " + item.Element("course_no")?.Value;
                logData += "\r\nRegistration Number: " + item.Element("registration_no")?.Value;
                logData += "\r\nType: " + item.Element("type")?.Value;
                logData += "\r\nGrade: " + item.Element("grade")?.Value;
                logData += "\r\nNotes: " + item.Element("notes")?.Value;
                logData += "\r\nNode Count: " + item.Nodes().Count();
                logData += "\r\nErrorMessage: " + message;
                logData += "\r\n-------------------";
            }
        }



        /// <summary>
        /// Processes the header of the input XML file.
        /// This method verifies the attributes of the XML file's root element. If any of the
        /// attributes produce an error, the file is NOT to be processed.
        /// </summary>
        /// <exception cref="Exception">
        /// Thrown when any of the following validation fails:
        /// - The root element does not have exactly 3 attributes.
        /// - The date attribute is not equal to today's date.
        /// - The program attribute does not match any value in the AcademicPrograms table.
        /// - The checksum attribute does not match the sum of all student_no elements.
        /// </exception>
        private void ProcessHeader()
        {
            // Load the XML document
            XDocument xdoc = XDocument.Load(inputFileName);

            XElement root = xdoc.Root;

            // Validate the number of attributes
            if (root.Attributes().Count() != 3)
            {
                throw new Exception("The root element must have exactly 3 attributes.");
            }

            // Validate the date attribute
            DateTime today = DateTime.Today;
            XAttribute dateAttribute = root.Attribute("date");

            if (dateAttribute == null || dateAttribute.Value != today.ToString("yyyy-MM-dd"))
            {
                throw new Exception("The date attribute must be equal to today's date.");
            }



            //The program attribute must match one of the academic program acronym values within
            //the AcademicPrograms table of your BITCollege_FLContext database
            string programAcronym = root.Attribute("program").Value;
            AcademicProgram program = db.AcademicPrograms
                                        .Where(x => x.ProgramAcronym == programAcronym)
                                        .SingleOrDefault();

            if (program == null)
            {
                throw new Exception("The program attribute must match an academic program acronym value.");
            }

            int checksum = int.Parse(root.Attribute("checksum").Value);
            int sumOfStudentNos = root.Descendants("student_no").Sum(x => int.Parse(x.Value));

            if (checksum != sumOfStudentNos)
            {
                throw new Exception($"The checksum attribute ({checksum}) does not match the sum of all student_no elements ({sumOfStudentNos}).");
            }

        }



        /// <summary>
        /// Process and validates the contents of a transmission
        /// </summary>
        private void ProcessDetails()
        {
            // Load the XML document
            XDocument xdoc = XDocument.Load(inputFileName);
            

            // Define an IEnumerable<XElement> LINQ-to-XML query to get transaction elements
            IEnumerable<XElement> transactions = xdoc.Descendants().Elements("transaction");


            // Round 1: Validate that each transaction element has 7 child elements
            IEnumerable<XElement> validChildElements = transactions.Where(x => x.Elements().Count() == 7);
            ProcessErrors(transactions, validChildElements, "Node counts is not 7.");

            // Round 2: Validate that the program element matches the program attribute of the root element
            IEnumerable<XElement> validProgramElements = validChildElements.Where(x => x.Element("program").Value ==
                                                            xdoc.Root.Attribute("program").Value);
            ProcessErrors(validChildElements, validProgramElements, "Transaction Program does not match root program");

            // Round 3: Validate that the type element within each transaction element is numeric
            IEnumerable<XElement> validTypeElements = validProgramElements.Where(x => Utility.Numeric.IsNumeric(x.Element("type").Value, NumberStyles.Integer));
            ProcessErrors(validProgramElements, validTypeElements, "Type element must be numeric.");

            // Round 4: Validate that the grade element is numeric or has the value of '*'
            IEnumerable<XElement> validGradeElements = validTypeElements.Where(x => Utility.Numeric.IsNumeric(x.Element("grade").Value, NumberStyles.Float) || x.Element("grade").Value == "*");
            ProcessErrors(validTypeElements, validGradeElements, "Grade element must be numeric or have the value of '*'.");

            // Round 5: Validate that the type element is 1 or 2
            IEnumerable<XElement> validTypeValues = validGradeElements.Where(x => x.Element("type").Value == "1" || x.Element("type").Value == "2");
            ProcessErrors(validGradeElements, validTypeValues, "Type element must have a value of 1 or 2.");

            // Round 6: Validate that the grade element for type 1 is '*' and for type 2 is between 0 and 100 inclusive
            IEnumerable<XElement> validGradeValues = validTypeValues.Where(x =>
                (x.Element("type").Value == "1" && x.Element("grade").Value == "*") ||
                (x.Element("type").Value == "2" && double.TryParse(x.Element("grade").Value, out double grade) && grade >= 0 && grade <= 100));
            ProcessErrors(validTypeValues, validGradeValues, "Grade element must be '*' for type 1 and between 0 and 100 inclusive for type 2.");

            // Round 7: Validate that the student_no element exists in the database
            IEnumerable<long> validStudentNumbers = db.Students.Select(s => s.StudentNumber).ToList();
            IEnumerable<XElement> validStudents = validGradeValues.Where(t => validStudentNumbers.Contains(long.Parse(t.Element("student_no").Value)));
            ProcessErrors(validGradeValues, validStudents, "Student number must exist in the database.");

            // Round 8: Validate that the course_no element is '*' for type 2 or exists in the database for type 1
            IEnumerable<string> validCourseNumbers = db.Courses.Select(c => c.CourseNumber).ToList();
            IEnumerable<XElement> validCourses = validStudents.Where(x =>
                (x.Element("type").Value == "2" && x.Element("course_no").Value == "*") ||
                (x.Element("type").Value == "1" && validCourseNumbers.Contains(x.Element("course_no").Value)));
            ProcessErrors(validStudents, validCourses, "Course number must be '*' for type 2 or exist in the database for type 1.");

            // Round 9: Validate that the registration_no element is '*' for type 1 or exists in the database for type 2
            IEnumerable<long> validRegistrationNumbers = db.Registrations.Select(r => r.RegistrationNumber).ToList();
            IEnumerable<XElement> validRegistrations = validCourses.Where(x =>
                (x.Element("type").Value == "1" && x.Element("registration_no").Value == "*") ||
                (x.Element("type").Value == "2" && validRegistrationNumbers.Contains(long.Parse(x.Element("registration_no").Value))));
            ProcessErrors(validCourses, validRegistrations, "Registration number must be '*' for type 1 or exist in the database for type 2.");

            // Call the ProcessTransactions method with the error-free result set
            ProcessTransactions(validRegistrations);
        }

























        /// <summary>
        /// Processes all valid transaction records.
        /// </summary>
        /// <param name="transactionRecords">A collection of transaction elements to be processed.</param>
        private void ProcessTransactions(IEnumerable<XElement> transactionRecords)
        {
            foreach (var transaction in transactionRecords)
            {
                
                {
                    // Extract values from the XElement
                    string type = transaction.Element("type")?.Value;
                    string studentNo = transaction.Element("student_no")?.Value;
                    string courseNo = transaction.Element("course_no")?.Value;
                    string registrationNo = transaction.Element("registration_no")?.Value;
                    string grade = transaction.Element("grade")?.Value;
                    string notes = transaction.Element("notes")?.Value;

                    //// Log the extracted values for debugging
                    //logData += $"\r\nExtracted values: type={type}, studentNo={studentNo}, courseNo={courseNo}, registrationNo={registrationNo}, grade={grade}, notes={notes}";

                    if (type == null || studentNo == null || notes == null)
                    {
                        logData += "\r\nTransaction missing required elements.";
                        continue;
                    }

                    if (type == "1") // Registration
                    {
                     
                            int StudentNumber = int.Parse(studentNo);
                            Course course = db.Courses.Where(x => x.CourseNumber == courseNo).SingleOrDefault();
                            Student student = db.Students.Where(x => x.StudentNumber == StudentNumber).SingleOrDefault();

                            // Use the WCF Service to register the student into the specified course
                            int result = service.RegisterCourse(student.StudentId, course.CourseId, notes);

                            // Check the result to determine if the operation was successful
                            if (result == 0) // Assuming 0 indicates success
                            {
                                logData += $"\r\nFile: {inputFileName}\r\nSuccessful registration for student {studentNo} in course {courseNo}. Notes: {notes}\n";
                            }
                            else
                            {
                                logData += $"\r\nFile: {inputFileName}\r\nUnsuccessful registration for student {studentNo} in course {courseNo}. Error code: {result}. Error: {Utility.BusinessRules.RegisterError(result)}\n";
                            }
              
                    }
                    else if (type == "2") // Grading
                    {
                        if (registrationNo == null || grade == null)
                        {
                            logData += "\r\nGrading transaction missing required elements.";
                            continue;
                        }

                        // Convert grade to a value between 0 and 1
                        double gradeValue = double.Parse(grade) / 100.0;
                        int registrationId = int.Parse(registrationNo);

                        Registration registration = db.Registrations.Where(x => x.RegistrationNumber == registrationId).SingleOrDefault();


                        // Use the WCF Service to update the student's grade
                        double? result = service.UpdateGrade(gradeValue, registration.RegistrationId, notes);

                        // Check the result to determine if the operation was successful
                        if (result.HasValue) // Non-null indicates success
                        {
                            logData += $"\r\nFile: {inputFileName}\r\nSuccessful grade update for student {studentNo} in course {courseNo}. Grade: {grade}. Notes: {notes}\n";
                        }
                        else
                        {
                            logData += $"\r\nFile: {inputFileName}\r\nUnsuccessful grade update for student {studentNo} in course {courseNo}. Error: Grade update failed.\n";
                        }


                    }
                }
                
            }
        }






        /// <summary>
        /// Writes the contents of the log data to a file /// </summary>
        /// <returns>the contents of the log data</returns> 
        public String WriteLogData()
            {
                StreamWriter writer = new StreamWriter(logFileName, true); writer.WriteLine(logData);
                writer.Close();

                string capturedLog = logData;
                logData = string.Empty;
                logFileName = string.Empty;
                

                // Return the local variable containing the captured logging data to the calling routine
                return capturedLog;
            }







            /// <summary>
            /// Must process Transmission for Assignment 7
            /// </summary>
            /// <param name="programAcronym">Represents the academic Program</param>
            public void ProcessTransmission(String programAcronym)
            {
                inputFileName = DateTime.Now.Year + "-" + DateTime.Now.DayOfYear +
                                "-" + programAcronym + ".xml";

                logFileName = "LOG " + inputFileName.Replace("xml", "txt");

                ////debugging
                //logData += $"\r\nInput file name: {inputFileName}";


            // Check if the input file exists
            if (File.Exists(inputFileName))
                {
                    try
                    {

                    //// Log the content of the XML file
                    //logData += $"\r\nXML file content:\n{File.ReadAllText(inputFileName)}";


                    // Call the ProcessHeader and ProcessDetails methods
                        ProcessHeader();
                        ProcessDetails();
                    }
                    catch (Exception ex)
                    {
                        logData += $"Exception occurred: {ex.Message}\n";
                    }
                }
                else
                {
                    logData += $"File does not exist: {inputFileName}\n";
                }
            }
        }
    } 

