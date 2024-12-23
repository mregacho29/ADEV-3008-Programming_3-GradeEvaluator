using BITCollege_MR.Data;
using BITCollege_MR.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.UI.WebControls;
using Utility;

namespace BITCollegeService
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CollegeRegistration" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CollegeRegistration.svc or CollegeRegistration.svc.cs at the Solution Explorer and start debugging.
    /// <summary>
    /// Provides method for course registration, grade updates, and course dropping
    /// </summary>
    public class CollegeRegistration : ICollegeRegistration
	{
		/// <summary>
		/// The database context used to interact with the BITCollege database
		/// </summary>
		private BITCollege_MRContext _context;
		
		/// <summary>
		/// Initializes a new instance of the College Registration
		/// </summary>
		public CollegeRegistration()
		{ 
			_context = new BITCollege_MRContext();
		}

		/// <summary>
		/// Drops a course based on the registration ID
		/// </summary>
		/// <param name="registrationId">The Id of the registration to drop</param>
		/// <returns>if the course was successfully dropped; otherwise false</returns>
		public bool DropCourse(int registrationId)
		{
			try
			{
				var registration = GetRegistrationById(registrationId);
				if (registration != null)
				{
					_context.Registrations.Remove(registration);
					_context.SaveChanges();
					return true;
				}
				return false;
			}
			catch (Exception)
			{ 
				return false;
			}
		}







        public int RegisterCourse(int studentId, int courseId, string notes)
        {
            // Define an IQueryable<Registration> query retrieving all records from the
            // Registrations table involving the Student and Course represented by method parameter values.
            IQueryable<Registration> allRecords = _context.Registrations
                .Where(x => x.StudentId == studentId && x.CourseId == courseId);

            // Define a query to retrieve the Student record represented by the corresponding method parameter value.
            Student student = _context.Students.Find(studentId);

            // Define a query to retrieve the Course record represented by the corresponding method parameter value.
            Course course = _context.Courses.Find(courseId);

            int returnCode = 0;

            // Using the IQueryable<Registration> query completed above, further query to determine
            // if any of the Registration records involving the Student and Course represented by
            // method parameter values has a grade of null.
            IEnumerable<Registration> nullRecords = allRecords.Where(x => x.Grade == null);


            // If so, the student is not permitted to register for this course because they
            // already have an ungraded registration.
            // Set the return code value to -100.
            // this will never  to true or null
            //if (nullRecords == null)evaluate

            if (nullRecords.Count() > 0)
            {
                returnCode = -100;
            }

            // Using the Course query completed above, determine whether the course is a Mastery course
            // (Hint: Check the CourseType property using a helper method included with the BusinessRules class).
            if (BusinessRules.CourseTypeLookup(course.CourseType) == CourseType.MASTERY)
            {

                // If so, cast the Course query above to be of MasteryCourse type.
                MasteryCourse masteryCourse = (MasteryCourse)course;

                // Using the IQueryable<Registration> query completed above, determine the number of
                // registrations that have already taken place between the Student and Course in question.
                IEnumerable<Registration> notnullRecords = allRecords.Where(x => x.Grade != null);

                // If the number of registrations is greater than or equal to the MaximumAttempts value,
                // then the upcoming registration would exceed the MaximumAttempts allowed.
                // If so, set the return code value to -200.
                if (notnullRecords.Count() >= masteryCourse.MaximumAttempts)
                {
                    returnCode = -200;
                }
            }

            // select your desired code
            // right click
            // Surround with
            // then type try
            // If the above validation indicates that the registration can proceed:
            if (returnCode == 0)
            {
                try
                {
                    // Create a new Registration object
                    Registration registration = new Registration();


                    // Populate this object’s properties with values pertinent to this registration:
                    // For the StudentId, CourseId and Notes fields, use the method arguments.
                    registration.StudentId = studentId;
                    registration.CourseId = courseId;
                    registration.Notes = notes;

                    // Use predefined functionality to generate the RegistrationNumber.
                    registration.SetNextRegistrationNumber();

                    // For the RegistrationDate, use today’s date.
                    registration.RegistrationDate = DateTime.Now;
                    registration.Grade = null;

                    // Add the Registration object to the Registrations table.
                    _context.Registrations.Add(registration);

                    // Persist this new record to the database.
                    _context.SaveChanges();


                    // The student must now be charged through their OutstandingFees for the new Registration.
                    // Using the Course query above, determine the TuitionAmount of the Course.
                    // Update the Student record by adding the Adjusted TuitionAmount to the OutstandingFees property.
                    // Ensure that the student is charged the appropriate fees based on the
                    // TuitionRateAdjustment method of the Student’s GradePointState.
                    student.OutstandingFees += course.TuitionAmount
                        * student.GradePointState.TuitionRateAdjustment(student);

                    // Persist this change to the database.
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    // If the above code results in an exception, a return code of -300 will be used.
                    returnCode = -300;
                }
            }

            // Ensure the appropriate return code is returned from this routine
            // If the registration is successful, return a value of 0.
            // If an exception occurs while updating, return a value of -300.
            // If the student has exceeded the MaximumAttempts of a Mastery course, return a value of -200.
            // If the student already has an ungraded registration for this course, return a value of -100.
            return returnCode;
        }










        /// <summary>
        /// Updates the grade for a specific registration
        /// </summary>
        /// <param name="grade">the new grade</param>
        /// <param name="registrationId">The ID of the registration to update.</param>
        /// <param name="notes">Additional notes for the update</param>
        /// <returns>The updated grade if the update was successful; otherwise, null</returns>
        public double? UpdateGrade(double grade, int registrationId, string notes)
        {
            try
            {
                // Use LINQ to retrieve the Registration record from the database
                // which corresponds to the method argument
                Registration registration = _context.Registrations
                    .FirstOrDefault(r => r.RegistrationId == registrationId);

                // Check if the registration record exists
                if (registration != null)
                {
                    // Set the Grade property of the Registration record to the
                    // value of the grade argument
                    registration.Grade = grade;

                    // Modify the Notes property with the value of the method argument
                    registration.Notes = notes;

                    // Persist the changes to the database
                    _context.SaveChanges();

                    // Call the CalculateGradePointAverage method and
                    // capture the result into a local variable
                    double? newGPA = CalculateGradePointAverage(registration.StudentId);

                    // Return the result of the CalculateGradePointAverage method
                    return newGPA;
                }

                else
                {
                    // If the registration record does not exist, return null
                    return null;
                }

            }
            catch (Exception)
            {


                // Handle exceptions and return null
                return null;
            }
        }

        /// <summary>
        /// Calculates the grade point average (GPA) for a student.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>The GPA if it can be calculated; otherwise, null</returns>
        private double? CalculateGradePointAverage(int studentId)
        {
            try
            {
                // Use LINQ with lambda expressions to define an
                // IQueryable<Registration> result set
                IQueryable<Registration> registrations = _context.Registrations
                    .Where(r => r.StudentId == studentId && r.Grade != null);

                // Check if there are any registrations with a non-null grade
                if (registrations.Any())
                {
                    // Initialize variables to calculate the GPA
                    double totalGrades = 0;
                    double totalCreditHours = 0;

                    // Iterate through the result set
                    foreach (Registration record in registrations.ToList())
                    {

                        // Obtain the Grade for the registration
                        double grade = record.Grade.Value;

                        // Determine the Course Type for the registration
                        CourseType courseType = Utility.BusinessRules.CourseTypeLookup(record.Course.CourseType);

                        // Exclude any Audit courses from the GPA calculation
                        if (courseType == CourseType.AUDIT)
                        {
                            continue;
                        }

                        // Use the BusinessRules class to determine the Grade Point Value for the grade
                        double gradePointValue = Utility.BusinessRules.GradeLookup(grade, courseType);

                        // Multiply the GradePointValue by the Course's CreditHours
                        double multiplyGradePoint = gradePointValue * record.Course.CreditHours;


                        // Accumulate the total grades and count the number of records
                        totalGrades += multiplyGradePoint;
                        totalCreditHours += record.Course.CreditHours;
                    }


                    // Calculate the GPA as the average of the grades
                    double? calculateGradePointAverage;
                    if (totalCreditHours == 0)
                    {
                        // If there are no graded courses, set the calculatedGradePointAverage to null
                        calculateGradePointAverage = null;
                    }
                    else
                    {

                        // If there are valid credit hours, calculate the GPA
                        calculateGradePointAverage = totalGrades / totalCreditHours;

                        //calculateGradePointAverage = Math.Round(calculateGradePointAverage.Value, 2);

                    }



                    // Define a LINQ query to obtain the Student record
                    Student student = _context.Students.FirstOrDefault(s => s.StudentId == studentId);

                    // Set the GradePointAverage property of the Student record to the newly calculated GPA
                    if (student != null)
                    {
                        student.GradePointAverage = calculateGradePointAverage;

                        // Persist this change
                        _context.SaveChanges();
                    }
                    // Return the calculated GPA
                    return calculateGradePointAverage;
                }

                else
                {
                    // If there are no registrations with a non-null grade, return null
                    return null;
                }
            }

            catch (Exception)
            {

                return null;
            }
        }
































        /// <summary>
        /// Private method to get a registration by ID
        /// </summary>
        /// <param name="registrationId">The ID of the registration to retrieve.</param>
        /// <returns>object if found otherwise null</returns>
        private Registration GetRegistrationById(int registrationId)
		{
			return _context.Registrations.SingleOrDefault(r => r.RegistrationId == registrationId);
		}
	}
}
