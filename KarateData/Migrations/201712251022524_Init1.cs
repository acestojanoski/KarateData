namespace KarateData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Competitors",
                c => new
                    {
                        CompetitorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CompetitorId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        EventName = c.String(),
                        EventDate = c.String(),
                        AplicationUserId = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.EventCompetitors",
                c => new
                    {
                        Event_EventId = c.Int(nullable: false),
                        Competitor_CompetitorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Event_EventId, t.Competitor_CompetitorId })
                .ForeignKey("dbo.Events", t => t.Event_EventId, cascadeDelete: true)
                .ForeignKey("dbo.Competitors", t => t.Competitor_CompetitorId, cascadeDelete: true)
                .Index(t => t.Event_EventId)
                .Index(t => t.Competitor_CompetitorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventCompetitors", "Competitor_CompetitorId", "dbo.Competitors");
            DropForeignKey("dbo.EventCompetitors", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.Events", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Competitors", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.EventCompetitors", new[] { "Competitor_CompetitorId" });
            DropIndex("dbo.EventCompetitors", new[] { "Event_EventId" });
            DropIndex("dbo.Events", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Competitors", new[] { "ApplicationUserId" });
            DropTable("dbo.EventCompetitors");
            DropTable("dbo.Events");
            DropTable("dbo.Competitors");
        }
    }
}
