using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BITCollege_MR;
using BITCollege_MR.Data;
using BITCollege_MR.Models;



namespace UnitTestBITCollege_MR
{

    internal class Program

        
    {
        private static BITCollege_MRContext db = new BITCollege_MRContext();

        static void Main(string[] args)
        {
            SuspendedStateGradePointAvg_44();
            SuspendedStateGradePointAvg_60();
            SuspendedStateGradePointAvg_80();
            ProbationStateGradePointAvg_115_3Courses();
            ProbationStateGradePointAvg_115_7Courses();
            RegularStateGradePointAvg();
            HonoursStateGradePointAvg_90();
            HonoursStateGradePointAvg_27();
            HonoursStateGradePointAvg_40();
            HonoursStateGradePointAvg_10();


            Console.ReadKey();

        }



        /// <summary>
        /// Unit test for SuspendedState
        /// </summary>
        static void SuspendedStateGradePointAvg_44()
        {

            //Set up test Student
            Student student = db.Students.Find(101); //First student in the database. Modify as needed
            student.GradePointAverage = .44;
            student.GradePointStateId = 1; //Assuming SuspendedState has an Id of 1 (check the database)
            db.SaveChanges();

            //Get an instance of the student's state
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            //call the tuition rate adjustment
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            //Output the expected and actual values
            Console.WriteLine("Expected: 1150");
            Console.WriteLine("Actual: " + tuitionRate);
        }


        static void SuspendedStateGradePointAvg_60()
        {
            // Set up test Student
            Student student = db.Students.Find(101); // First student in the database
            student.GradePointAverage = .60;
            student.GradePointStateId = SuspendedState.GetInstance().GradePointStateId; // Ensure SuspendedState is set
            db.SaveChanges();

            // Get an instance of the student's state
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            // Call the tuition rate adjustment
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            // Output the expected and actual values
            Console.WriteLine("Expected: 1120");
            Console.WriteLine("Actual: " + tuitionRate);
        }
                
        
        static void SuspendedStateGradePointAvg_80()
        {
            // Set up test Student
            Student student = db.Students.Find(101); // First student in the database
            student.GradePointAverage = .80;
            student.GradePointStateId = SuspendedState.GetInstance().GradePointStateId; // Ensure SuspendedState is set
            db.SaveChanges();

            // Get an instance of the student's state
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            // Call the tuition rate adjustment
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            // Output the expected and actual values
            Console.WriteLine("Expected: 1100");
            Console.WriteLine("Actual: " + tuitionRate);
        }








        static void ProbationStateGradePointAvg_115_3Courses()
        {
            // Set up test Student
            Student student = db.Students.Find(126); // Modify as needed
            student.GradePointAverage = 1.15;
            student.GradePointStateId = 2; // Assuming ProbationState has an Id of 2
            db.SaveChanges();

            // Ensure a valid CourseId exists
            int validCourseId = db.Courses.First().CourseId;

            // Clear existing registrations for the student
            var existingRegistrations = db.Registrations.Where(r => r.StudentId == student.StudentId);
            db.Registrations.RemoveRange(existingRegistrations);
            db.SaveChanges();

            // Simulate the student having completed 3 courses
            for (int i = 0; i < 3; i++)
            {
                db.Registrations.Add(new Registration
                {
                    Grade = 0.65, // Non-null value to count as completed
                    StudentId = student.StudentId,
                    CourseId = validCourseId,
                    RegistrationDate = DateTime.Now
                });
            }
            db.SaveChanges();

            // Get an instance of the student's state
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            // Calculate the tuition rate adjustment for a $1000 course
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            // Check if the calculated tuition is as expected
            double expectedTuition = 1000 * 1.075; // 7.5% increase

            // Output the results
            if (tuitionRate == expectedTuition)
            {
                Console.WriteLine("Test Passed: Expected tuition is " + expectedTuition + " and actual is " + tuitionRate);
            }
            else
            {
                Console.WriteLine("Test Failed: Expected " + expectedTuition + ", but got " + tuitionRate);
            }
        }


        static void ProbationStateGradePointAvg_115_7Courses()
        {
            // Set up test Student
            Student student = db.Students.Find(121); // Modify as needed
            student.GradePointAverage = 1.15;
            student.GradePointStateId = 2; // Assuming ProbationState has an Id of 2
            db.SaveChanges();

            // Ensure a valid CourseId exists
            int validCourseId = db.Courses.First().CourseId;

            // Clear existing registrations for the student
            var existingRegistrations = db.Registrations.Where(r => r.StudentId == student.StudentId);
            db.Registrations.RemoveRange(existingRegistrations);
            db.SaveChanges();

            // Simulate the student having completed 7 courses
            for (int i = 0; i < 7; i++) // Change loop count to 7
            {
                db.Registrations.Add(new Registration
                {
                    Grade = 0.65, // Non-null value to count as completed
                    StudentId = student.StudentId,
                    CourseId = validCourseId,
                    RegistrationDate = DateTime.Now
                });
            }
            db.SaveChanges();

            // Get an instance of the student's state
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            // Calculate the tuition rate adjustment for a $1000 course
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            // Check if the calculated tuition is as expected
            double expectedTuition = 1000 * 1.035; // 3.5% increase

            // Output the results
            if (tuitionRate == expectedTuition)
            {
                Console.WriteLine("Test Passed: Expected tuition is " + expectedTuition + " and actual is " + tuitionRate);
            }
            else
            {
                Console.WriteLine("Test Failed: Expected " + expectedTuition + ", but got " + tuitionRate);
            }
        }







        /// <summary>
        /// Unit test for SuspendedState
        /// </summary>
        static void RegularStateGradePointAvg()
        {

            //Set up test Student
            Student student = db.Students.Find(101); //First student in the database. Modify as needed
            student.GradePointAverage = 2.50;
            student.GradePointStateId = 3; //Assuming SuspendedState has an Id of 3 (check the database)
            db.SaveChanges();

            //Get an instance of the student's state
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            //call the tuition rate adjustment
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            //Output the expected and actual values
            Console.WriteLine("Expected: 1000");
            Console.WriteLine("Actual: " + tuitionRate);
        }






        static void HonoursStateGradePointAvg_90()
        {

            // Set up test Student
            Student student = db.Students.Find(121); // Modify as needed
            student.GradePointAverage = 3.90;
            student.GradePointStateId = 4; 
            db.SaveChanges();

            // Ensure a valid CourseId exists
            int validCourseId = db.Courses.First().CourseId;

            // Clear existing registrations for the student
            var existingRegistrations = db.Registrations.Where(r => r.StudentId == student.StudentId);
            db.Registrations.RemoveRange(existingRegistrations);
            db.SaveChanges();

            // Simulate the student having completed 7 courses
            for (int i = 0; i < 3; i++) 
            {
                db.Registrations.Add(new Registration
                {
                    Grade = 0.65, // Non-null value to count as completed
                    StudentId = student.StudentId,
                    CourseId = validCourseId,
                    RegistrationDate = DateTime.Now
                });
            }
            db.SaveChanges();

            //Get an instance of the student's state
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            //call the tuition rate adjustment
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            //Output the expected and actual values
            Console.WriteLine("Expected: 900");
            Console.WriteLine("Actual: " + tuitionRate);

        }


        static void HonoursStateGradePointAvg_27()
        {

            // Set up test Student
            Student student = db.Students.Find(121); // Modify as needed
            student.GradePointAverage = 4.27;
            student.GradePointStateId = 4;
            db.SaveChanges();

            // Ensure a valid CourseId exists
            int validCourseId = db.Courses.First().CourseId;

            // Clear existing registrations for the student
            var existingRegistrations = db.Registrations.Where(r => r.StudentId == student.StudentId);
            db.Registrations.RemoveRange(existingRegistrations);
            db.SaveChanges();

            // Simulate the student having completed 7 courses
            for (int i = 0; i < 4; i++)
            {
                db.Registrations.Add(new Registration
                {
                    Grade = 0.65, // Non-null value to count as completed
                    StudentId = student.StudentId,
                    CourseId = validCourseId,
                    RegistrationDate = DateTime.Now
                });
            }
            db.SaveChanges();

            //Get an instance of the student's state
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            //call the tuition rate adjustment
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            //Output the expected and actual values
            Console.WriteLine("Expected: 880");
            Console.WriteLine("Actual: " + tuitionRate);

        }

        static void HonoursStateGradePointAvg_40()
        {

            // Set up test Student
            Student student = db.Students.Find(121); // Modify as needed
            student.GradePointAverage = 4.40;
            student.GradePointStateId = 4;
            db.SaveChanges();

            // Ensure a valid CourseId exists
            int validCourseId = db.Courses.First().CourseId;

            // Clear existing registrations for the student
            var existingRegistrations = db.Registrations.Where(r => r.StudentId == student.StudentId);
            db.Registrations.RemoveRange(existingRegistrations);
            db.SaveChanges();

            // Simulate the student having completed 7 courses
            for (int i = 0; i < 7; i++)
            {
                db.Registrations.Add(new Registration
                {
                    Grade = 0.65, // Non-null value to count as completed
                    StudentId = student.StudentId,
                    CourseId = validCourseId,
                    RegistrationDate = DateTime.Now
                });
            }
            db.SaveChanges();

            //Get an instance of the student's state
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            //call the tuition rate adjustment
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            //Output the expected and actual values
            Console.WriteLine("Expected: 830");
            Console.WriteLine("Actual: " + tuitionRate);

        }



        static void HonoursStateGradePointAvg_10()
        {

            // Set up test Student
            Student student = db.Students.Find(121); // Modify as needed
            student.GradePointAverage = 4.10;
            student.GradePointStateId = 4;
            db.SaveChanges();

            // Ensure a valid CourseId exists
            int validCourseId = db.Courses.First().CourseId;

            // Clear existing registrations for the student
            var existingRegistrations = db.Registrations.Where(r => r.StudentId == student.StudentId);
            db.Registrations.RemoveRange(existingRegistrations);
            db.SaveChanges();

            // Simulate the student having completed 7 courses
            for (int i = 0; i < 7; i++)
            {
                db.Registrations.Add(new Registration
                {
                    Grade = 0.65, // Non-null value to count as completed
                    StudentId = student.StudentId,
                    CourseId = validCourseId,
                    RegistrationDate = DateTime.Now
                });
            }
            db.SaveChanges();

            //Get an instance of the student's state
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            //call the tuition rate adjustment
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            //Output the expected and actual values
            Console.WriteLine("Expected: 850");
            Console.WriteLine("Actual: " + tuitionRate);

        }
    }
}
