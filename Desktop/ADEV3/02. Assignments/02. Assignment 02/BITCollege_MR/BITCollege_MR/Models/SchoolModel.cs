using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;

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
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
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

    }

    /// <summary>
    /// Represents the suspended state of a student
    /// </summary>
    public class SuspendedState : GradePointState
    {
        // Singleton instance
        private static SuspendedState suspendedState;

    }

    /// <summary>
    /// Represents the probation state of a student
    /// </summary>
    public class ProbationState : GradePointState
    {
        // Singleton instance
        private static ProbationState probationState;

    }

    /// <summary>
    /// Represents the regular state of a student
    /// </summary>
    public class RegularState : GradePointState
    {
        // Singleton instance
        private static RegularState regularState;

    }

    /// <summary>
    /// Represents the honours state of a student
    /// </summary>
    public class HonoursState : GradePointState
    {
        // Singleton instance
        private static HonoursState honoursState;

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