namespace TEST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAtr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Films", "Created_post", c => c.DateTime(nullable: false));
            DropColumn("dbo.Films", "CreatorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Films", "CreatorId", c => c.Guid(nullable: false));
            DropColumn("dbo.Films", "Created_post");
        }
    }
}
