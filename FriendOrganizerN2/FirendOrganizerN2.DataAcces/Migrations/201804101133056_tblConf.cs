namespace FirendOrganizerN2.DataAcces.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblConf : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Friend", "FirstName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Friend", "FirstName", c => c.String());
        }
    }
}
