namespace BeerTapsAPI.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Offices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Taps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Remaining = c.Int(nullable: false),
                        TapState = c.Int(nullable: false),
                        OfficeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Offices", t => t.OfficeID, cascadeDelete: true)
                .Index(t => t.OfficeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Taps", "OfficeID", "dbo.Offices");
            DropIndex("dbo.Taps", new[] { "OfficeID" });
            DropTable("dbo.Taps");
            DropTable("dbo.Offices");
        }
    }
}
