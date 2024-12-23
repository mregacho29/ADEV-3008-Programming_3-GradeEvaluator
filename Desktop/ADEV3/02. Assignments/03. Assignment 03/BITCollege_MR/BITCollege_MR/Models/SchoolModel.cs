using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;
using BITCollege_MR.Data;
using BITCollege_MR.Controllers;

namespace BITCollege_MR.Models
{
    /// <summary>
    /// Represents a student in the system
    /// </summary>
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        //foreign key name must match the name of the corresponding
        //navigation property
        [Required]
        [ForeignKey("GradePointState")]
        public int GradePointStateId { get; set; }

        //foreign key name must match the name of the corresponding
        //navigation property
        [ForeignKey("AcademicProgram")]
        public int? AcademicProgramId { get; set; }

        [Required]
        [Range(10000000, 99999999)]
        [Display(Name = "Student\nNumber")]
        public long StudentNumber { get; set; }

        [Required]
        [Display(Name = "First\nName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last\nName")]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required(ErrorMessage = "Invalid Canadian province code.")]
        [RegularExpression("^(N[BLSTU]|[AMN]B|[BQ]C|ON|PE|SK|YT)")]
        public string Province { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Grade\nPoint\nAverage")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Range(0, 4.5)]
        public double? GradePointAverage { get; set; }

        [Required]
        [Display(Name = "Fees")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public double OutstandingFees { get; set; }


        public string Notes { get; set; }


        // Read-only property derived from FirstName and LastName
        [Display(Name = "Name")]
        public string FullName 
        { 
            get
            {
                return String.Format("{0} {1}", FirstName, LastName);
            }
        }

        // Read-only property derived from Address, City, and Province
        [Display(Name = "Address")]
        public string FullAddress
        {
            get
            {
                return String.Format("{0} {1} {2}", Address, City, Province);
            }
        }

        // Navigation properties for Lazy Loading
        // represents a 1 or 0 on class diagram
        public virtual GradePointState GradePointState { get; set; }
        public virtual AcademicProgram AcademicProgram { get; set; }

        // Navigation property for related Registrations
        // represents a many relationship
        // as defined on the class diagram
        public virtual ICollection<Registration> Registrations { get; set; }







        public void ChangeState()
        {
            //no if statements
            //no recursion

            //Step 1: get an instance of the context object
            BITCollege_MRContext db = new BITCollege_MRContext();

            //Step 2: Obtain the current GradePointState object
            //GradePointState currentState = db.GradePointStates.Find(GradePointStateId);
            GradePointState currentState = db.GradePointStates.Find(GradePointStateId);

            //Step 3: Initialize a variable to track the previous state ID
            //int previousId = 0;
            int previousId = 0;

            //Step 4: Loop
            //while (previouosId != currentState.GradePointStateId)
            //    (
            //    currentState.StateChangeCheck(this);
            //previousId = currentState.GradePointStateId;
            //currentState = db.GradePointStates.Find(GradePointStateId);
            //)

            while (previousId != currentState.GradePointStateId)
            {
                currentState.StateChangeCheck(this);
                previousId = currentState.GradePointStateId;
                currentState = db.GradePointStates.Find(GradePointStateId);
            }
        }

    }

    /// <summary>
    /// Represents an academic program in the system
    /// </summary>
    public class AcademicProgram
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AcademicProgramId { get; set; }

        [Required]
        [Display(Name = "Program")]
        public string ProgramAcronym { get; set; }

        [Required]
        [Display(Name = "Program\nName")]
        public string Description { get; set; }

        // Navigation property for related Students
        // represents a many relationship
        // as defined on the class diagram
        public virtual ICollection<Student> Students { get; set; }

        // Navigation property for related Courses
        // represents a many relationship
        // as defined on the class diagram
        public virtual ICollection<Course> Courses { get; set; }
    }

    /// <summary>
    /// Represents the grade point state of a student
    /// </summary>
    public abstract class GradePointState
    {
        protected static BITCollege_MRContext db = new BITCollege_MRContext();

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int GradePointStateId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Display(Name = "Lower\nLimit")]
        public double LowerLimit { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Display(Name = "Upper\nLimit")]
        public double UpperLimit { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Display(Name = "Tuition\nRate\nFactor")]
        public double TuitionRateFactor { get; set; }

        [Display(Name = "State")]
        public string Description 
        { 
            get
            {
                return GetType().Name.Substring(0, GetType().Name.LastIndexOf("State"));
            }
        }


        // Navigation property for related Students
        // represents a many relationship
        // as defined on the class diagram
        public virtual ICollection<Student> Students { get; set; }


        /// <summary>
        /// A blueprint for StateChangeCheck
        /// </summary>
        /// <param name="student">A student object</param>
        public abstract void StateChangeCheck(Student student);

        /// <summary>
        /// A blueprint for TuitionRatedAdjustment
        /// </summary>
        /// <param name="student">A student object</param>
        /// <returns>Adjusted tuition rates</returns>
        public abstract double TuitionRateAdjustment(Student student);



    }

    /// <summary>
    /// Represents the suspended state of a student
    /// </summary>
    public class SuspendedState : GradePointState
    {
        // Singleton instance
        private static SuspendedState suspendedState;



        /// <summary>
        /// Creates and instance of Suspended State
        /// </summary>
        private SuspendedState()
        {
            LowerLimit = 0.00;
            UpperLimit = 1.00;
            TuitionRateFactor = 1.1;
        }

        /// <summary>
        /// Ensures instance of the Suspended State
        /// </summary>
        /// <returns>instance of Suspended State</returns>
        public static SuspendedState GetInstance()
        {
            if(suspendedState == null)
            {
                suspendedState = db.SuspendedStates.SingleOrDefault();

                if(suspendedState == null)
                {
                    suspendedState = new SuspendedState();
                    db.SuspendedStates.Add(suspendedState);
                    db.SaveChanges();
                }
            }
            return suspendedState;
        }

        /// <summary>
        /// Checks for a change in state
        /// </summary>
        /// <param name="student">A student object</param>
        public override void StateChangeCheck(Student student)
        {
            if (student.GradePointAverage > 1)
            {
                student.GradePointStateId = ProbationState.GetInstance().GradePointStateId;
            }

        }

        /// <summary>
        /// Adjust's the student tuition rate
        /// </summary>
        /// <param name="student">a student object</param>
        /// <returns></returns>
        public override double TuitionRateAdjustment(Student student)
        {
            // Base tuition rate using the TuitionRateFactor
            double adjustedTuition = TuitionRateFactor;


            // Check the student's GradePointAverage
            if (student.GradePointAverage < 0.75)
            {
                // Increase by 2% above the default premium
                adjustedTuition += 0.02;
            }
            if (student.GradePointAverage < 0.50)
            {
                // Increase by 5% above the default premium
                adjustedTuition += 0.03;
            }

            // Return the adjusted tuition rate
            return adjustedTuition;
        }


    }

    /// <summary>
    /// Represents the probation state of a student
    /// </summary>
    public class ProbationState : GradePointState
    {
        // Singleton instance
        private static ProbationState probationState;

        /// <summary>
        /// Creates and instance of Probation State
        /// </summary>
        private ProbationState()
        {
            LowerLimit = 1.00;
            UpperLimit = 2.00;
            TuitionRateFactor = 1.075;

        }

        /// <summary>
        /// Ensures an instance of the Probation State
        /// </summary>
        /// <returns>and instance of Probation State</returns>
        public static ProbationState GetInstance()
        {
            if(probationState == null)
            {
                probationState = db.ProbationStates.SingleOrDefault();

                if(probationState == null)
                {
                    probationState = new ProbationState();
                    db.ProbationStates.Add(probationState);
                    db.SaveChanges();
                }
            }
            return probationState;
        }


        /// <summary>
        /// Checks for a change in state
        /// </summary>
        /// <param name="student">A student object</param>
        public override void StateChangeCheck(Student student)
        {
            if (student.GradePointAverage < 1)
            {
                student.GradePointStateId = SuspendedState.GetInstance().GradePointStateId;
            }

            if (student.GradePointAverage > 2)
            {
                student.GradePointStateId = RegularState.GetInstance().GradePointStateId;
            }
        }



        /// <summary>
        /// Adjust's the student tuition rate
        /// </summary>
        /// <param name="student">a student object</param>
        /// <returns></returns>
        public override double TuitionRateAdjustment(Student student)
        {
            //Use a local variable!
            //TuitionRateFactor += .02; dont use this
            // Base tuition rate using the TuitionRateFactor
            double adjustedTuition = TuitionRateFactor;

            //enumerating data on the server side (Grade is not null)
            IQueryable<Registration> registrations = 
                db.Registrations.Where(x => x.StudentId == student.StudentId && x.Grade != null);

            // Check if the student has completed 5 or more courses
            if (registrations.Count() >= 5)
            {
                // Adjust the tuition rate for students with 5 or more completed courses
                //adjustedTuition -= .04;
                adjustedTuition += 0.035; // 3.5% increase

            }
            //be careful with else blocks!
            // Return the adjusted tuition rate
            //return 0;
            return adjustedTuition;
        }

    }

    /// <summary>
    /// Represents the regular state of a student
    /// </summary>
    public class RegularState : GradePointState
    {
        // Singleton instance
        private static RegularState regularState;


        /// <summary>
        /// Creates and instance of Regular State
        /// </summary>
        private RegularState()
        {
            LowerLimit = 2.00;
            UpperLimit = 3.70;
            TuitionRateFactor = 1.0;
        }

        /// <summary>
        /// Ensures an instance of the Regular State
        /// </summary>
        /// <returns>and instance of Regular State</returns>
        public static RegularState GetInstance()
        {
            if (regularState == null)
            {
                regularState = db.RegularStates.SingleOrDefault();

                if (regularState == null)
                {
                    regularState = new RegularState();
                    db.RegularStates.Add(regularState);
                    db.SaveChanges();
                }
            }
            return regularState;
        }

        /// <summary>
        /// Checks for a change in state
        /// </summary>
        /// <param name="student">A student object</param>
        public override void StateChangeCheck(Student student)
        {
            if (student.GradePointAverage < 2)
            {
                student.GradePointStateId = ProbationState.GetInstance().GradePointStateId;
            }

            if (student.GradePointAverage > 3.7)
            {
                student.GradePointStateId = HonoursState.GetInstance().GradePointStateId;
            }
        }

        /// <summary>
        /// Adjust's the student tuition rate
        /// </summary>
        /// <param name="student">A student object</param>
        /// <returns></returns>
        public override double TuitionRateAdjustment(Student student)
        {
            return TuitionRateFactor;
        }

    }

    /// <summary>
    /// Represents the honours state of a student
    /// </summary>
    public class HonoursState : GradePointState
    {
        // Singleton instance
        private static HonoursState honoursState;

        /// <summary>
        /// Creates and instance of Honours State
        /// </summary>
        private HonoursState()
        {
            LowerLimit = 3.70;
            UpperLimit = 4.50;
            TuitionRateFactor = 0.9;
        }

        /// <summary>
        /// Ensures an instance of the Honours State
        /// </summary>
        /// <returns>and instance of Honours State</returns>
        public static HonoursState GetInstance()
        {
            if(honoursState == null)
            {
                honoursState = db.HonoursStates.SingleOrDefault();

                if(honoursState == null)
                {
                    honoursState = new HonoursState();
                    db.HonoursStates.Add(honoursState);
                    db.SaveChanges();
                }
            }
            return honoursState;

        }

        /// <summary>
        /// Checks for a change in state
        /// </summary>
        /// <param name="student">A student object</param>
        public override void StateChangeCheck(Student student)
        {
            if (student.GradePointAverage < 3.7)
            {
                student.GradePointStateId = RegularState.GetInstance().GradePointStateId;
            }

        }

        /// <summary>
        /// Adjust's the student tuition rate
        /// </summary>
        /// <param name="student">A student object</param>
        /// <returns></returns>
        public override double TuitionRateAdjustment(Student student)
        {

            // Base tuition rate using the TuitionRateFactor
            double adjustedTuition = TuitionRateFactor;

            //enumerating data on the server side (Grade is not null)
            IQueryable<Registration> registrations =
                db.Registrations.Where(x => x.StudentId == student.StudentId && x.Grade != null);

            // Check if the student has completed 5 or more courses
            if (registrations.Count() >= 5)
            {
                // Adjust the tuition rate for students with 5 or more completed courses
                //adjustedTuition -= .04;
                // Apply an additional 5% discount
                adjustedTuition -= 0.05; 

            }

            // Check if the student's GradePointAverage is above 4.25
            if (student.GradePointAverage > 4.25)
            {
                // Apply an additional 2% discount
                adjustedTuition -= 0.02; 
            }

            // Return the adjusted tuition rate
            return adjustedTuition;
        }
    }

    /// <summary>
    /// Represents a course in the system
    /// </summary>
    public abstract class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CourseId { get; set; }

        [ForeignKey("AcademicProgram")]
        public int? AcademicProgramId { get; set; }

        [Required]
        [Display(Name = "Course\nNumber")]
        public string CourseNumber { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Display(Name = "Credit\nHours")]
        public double CreditHours { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Tuition")]
        public double TuitionAmount { get; set; }

        [Display(Name = "Course\nType")]
        public string CourseType 
        { 
            get
            {
                return GetType().Name.Substring(0, GetType().Name.LastIndexOf("CourseType"));
            }
        }

        // no additional requirement
        public string Notes { get; set; }


        // Navigation property for related AcademicProgram
        // Represents a 0..1 relationship as defined on the class diagram
        public virtual AcademicProgram AcademicProgram { get; set; }

        // Navigation property for related Registrations
        // represents a many relationship
        // as defined on the class diagram
        public virtual ICollection<Registration> Registrations { get; set; }


    }

    /// <summary>
    /// Represents a graded course in the system
    /// </summary>
    public class GradedCourse : Course
    {
        [Required]
        [Display(Name = "Assignments")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        public double AssignmentWeight { get; set; }

        [Required]
        [Display(Name = "Exams")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        public double ExamWeight { get; set; }

    }

    /// <summary>
    /// Represents a mastery course in the system
    /// </summary>
    public class MasteryCourse : Course
    {
        [Required]
        [Display(Name = "Maximum\nAttempts")]
        public int MaximumAttempts { get; set; }

    }

    /// <summary>
    /// Represents an audit course in the system
    /// </summary>
    public class AuditCourse : Course
    {
        // No additional annotation requirements for the AuditCourse Model.
    }

    /// <summary>
    /// Represents a registration in the system
    /// </summary>
    public class Registration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegistrationId { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        [Required]
        [Display(Name = "Registration\nNumber")]
        public long RegistrationNumber { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime RegistrationDate { get; set; }

        [DisplayFormat(NullDisplayText = "Ungraded", DataFormatString = "{0:F2}")]
        [Range(0,1)]
        public double? Grade { get; set; }

        // No additional requirements
        public string Notes { get; set; }

        // Navigation properties
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}