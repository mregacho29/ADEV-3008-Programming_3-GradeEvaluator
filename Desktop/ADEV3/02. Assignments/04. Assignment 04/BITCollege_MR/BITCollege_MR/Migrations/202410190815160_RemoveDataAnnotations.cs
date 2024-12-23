namespace BITCollege_MR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "CourseNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "CourseNumber", c => c.String(nullable: false));
        }
    }
}
