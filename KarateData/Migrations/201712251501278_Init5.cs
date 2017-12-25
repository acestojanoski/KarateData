namespace KarateData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EventCompetitors", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.EventCompetitors", "Competitor_CompetitorId", "dbo.Competitors");
            DropIndex("dbo.Events", new[] { "ApplicationUserId" });
            DropIndex("dbo.EventCompetitors", new[] { "Event_EventId" });
            DropIndex("dbo.EventCompetitors", new[] { "Competitor_CompetitorId" });
            RenameColumn(table: "dbo.Competitors", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.Competitors", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            DropTable("dbo.Events");
            DropTable("dbo.EventCompetitors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EventCompetitors",
                c => new
                    {
                        Event_EventId = c.Int(nullable: false),
                        Competitor_CompetitorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Event_EventId, t.Competitor_CompetitorId });
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        EventName = c.String(),
                        EventDate = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EventId);
            
            RenameIndex(table: "dbo.Competitors", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            RenameColumn(table: "dbo.Competitors", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            CreateIndex("dbo.EventCompetitors", "Competitor_CompetitorId");
            CreateIndex("dbo.EventCompetitors", "Event_EventId");
            CreateIndex("dbo.Events", "ApplicationUserId");
            AddForeignKey("dbo.EventCompetitors", "Competitor_CompetitorId", "dbo.Competitors", "CompetitorId", cascadeDelete: true);
            AddForeignKey("dbo.EventCompetitors", "Event_EventId", "dbo.Events", "EventId", cascadeDelete: true);
            AddForeignKey("dbo.Events", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
