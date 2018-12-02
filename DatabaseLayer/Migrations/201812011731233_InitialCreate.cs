namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.statistics",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        clickdate = c.DateTime(nullable: false),
                        shortUrl_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.urlDetails", t => t.shortUrl_id, cascadeDelete: true)
                .Index(t => t.shortUrl_id);
            
            CreateTable(
                "dbo.urlDetails",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        orignalurl = c.String(nullable: false, maxLength: 1000),
                        customurl = c.String(nullable: false, maxLength: 50),
                        createdon = c.DateTime(nullable: false),
                        clicks = c.Int(nullable: false),
                        ip = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.statistics", "shortUrl_id", "dbo.urlDetails");
            DropIndex("dbo.statistics", new[] { "shortUrl_id" });
            DropTable("dbo.urlDetails");
            DropTable("dbo.statistics");
        }
    }
}
