using System;
using System.Collections.Generic;
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
            ProbationStateGradePointAvg_1();
            ProbationStateGradePointAvg_2();

            Console.ReadKey();

        }

        /// <summary>
        /// Unit test for SuspendedState
        /// </summary>
        static void SuspendedStateGradePointAvg_44()
        {

            //Set up test Student
            Student student = db.Students.Find(2); //First student in the database
            student.GradePointAverage = .44;
            student.GradePointStateId = 2; //Assuming SuspendedState has an Id of 1 (check the database)
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

            //Set up test Student
            Student student = db.Students.Find(2); //First student in the database
            student.GradePointAverage = .60;
            student.GradePointStateId = 2; //Assuming SuspendedState has an Id of 1 (check the database)
            db.SaveChanges();

            //Get an instance of the student's state
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            //call the tuition rate adjustment
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            //Output the expected and actual values
            Console.WriteLine("Expected: 1120");
            Console.WriteLine("Actual: " + tuitionRate);
        }

        static void SuspendedStateGradePointAvg_80()
        {

            //Set up test Student
            Student student = db.Students.Find(2); //First student in the database
            student.GradePointAverage = .80;
            student.GradePointStateId = 2; //Assuming SuspendedState has an Id of 1 (check the database)
            db.SaveChanges();

            //Get an instance of the student's state
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            //call the tuition rate adjustment
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            //Output the expected and actual values
            Console.WriteLine("Expected: 1100");
            Console.WriteLine("Actual: " + tuitionRate);
        }


        /// <summary>
        /// Unit test for Probation State
        /// </summary>
        static void ProbationStateGradePointAvg_1()
        {

            //Set up test Student
            Student student = db.Students.Find(2); //First student in the database
            student.GradePointAverage = 1.15;
            student.GradePointStateId = 3; //Assuming SuspendedState has an Id of 1 (check the database)
            db.SaveChanges();

            //Get an instance of the student's state
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            //call the tuition rate adjustment
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            //Output the expected and actual values
            Console.WriteLine("Expected: 1075");
            Console.WriteLine("Actual: " + tuitionRate);
        }


        /// <summary>
        /// Unit test for Probation State
        /// </summary>
        static void ProbationStateGradePointAvg_2()
        {
            // Set up test Student
            Student student = db.Students.Find(2); // First student in the database
            student.GradePointAverage = 1.15;
            student.GradePointStateId = 3; // Assuming ProbationState has an Id of 3 (check the database)
            

            db.SaveChanges();

            // Get an instance of the student's state
            GradePointState state = db.GradePointStates.Find(student.GradePointStateId);

            // Call the tuition rate adjustment
            double tuitionRate = 1000 * state.TuitionRateAdjustment(student);

            // Output the expected and actual values
            Console.WriteLine("Expected: 1035");
            Console.WriteLine("Actual: " + tuitionRate);
        }
    }
}
