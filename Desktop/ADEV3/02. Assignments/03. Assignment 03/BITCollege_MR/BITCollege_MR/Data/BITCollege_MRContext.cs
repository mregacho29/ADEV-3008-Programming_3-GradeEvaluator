using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BITCollege_MR.Data
{
    public class BITCollege_MRContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BITCollege_MRContext() : base("name=BITCollege_MRContext")
        {
        }

        public System.Data.Entity.DbSet<BITCollege_MR.Models.Student> Students { get; set; }

        public System.Data.Entity.DbSet<BITCollege_MR.Models.AcademicProgram> AcademicPrograms { get; set; }

        public System.Data.Entity.DbSet<BITCollege_MR.Models.GradePointState> GradePointStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_MR.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_MR.Models.Registration> Registrations { get; set; }

        public System.Data.Entity.DbSet<BITCollege_MR.Models.AuditCourse> AuditCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_MR.Models.GradedCourse> GradedCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_MR.Models.HonoursState> HonoursStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_MR.Models.MasteryCourse> MasteryCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_MR.Models.ProbationState> ProbationStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_MR.Models.RegularState> RegularStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_MR.Models.SuspendedState> SuspendedStates { get; set; }
    }
}
