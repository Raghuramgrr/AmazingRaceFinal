namespace AmazingRace.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false),
                        EventName = c.String(),
                        EventLocation = c.String(nullable: false),
                        EventTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.PitStops",
                c => new
                    {
                        PitStopId = c.Int(nullable: false, identity: true),
                        PitStopName = c.String(),
                        PitStopLocation = c.String(),
                        PitStopOrder = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PitStopId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        LoginId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.LoginId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        TeamName = c.String(),
                        TeamDescription = c.String(),
                        EventName = c.String(),
                        Team_TeamId = c.Int(),
                    })
                .PrimaryKey(t => t.TeamId)
                .ForeignKey("dbo.Teams", t => t.Team_TeamId)
                .Index(t => t.Team_TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "Team_TeamId", "dbo.Teams");
            DropForeignKey("dbo.PitStops", "EventId", "dbo.Events");
            DropIndex("dbo.Teams", new[] { "Team_TeamId" });
            DropIndex("dbo.PitStops", new[] { "EventId" });
            DropTable("dbo.Teams");
            DropTable("dbo.Logins");
            DropTable("dbo.PitStops");
            DropTable("dbo.Events");
        }
    }
}
