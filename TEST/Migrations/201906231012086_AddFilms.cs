namespace TEST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFilms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Films",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatorId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Created = c.DateTime(nullable: false),
                        Regisseur = c.String(nullable: false),
                        CreatorName = c.String(),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Films");
        }
    }
}
