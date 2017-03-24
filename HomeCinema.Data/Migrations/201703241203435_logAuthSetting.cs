namespace HomeCinema.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class logAuthSetting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityLog",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ActivityLogTypeID = c.Int(nullable: false),
                        UserName = c.String(),
                        UserId = c.Int(nullable: false),
                        Comment = c.String(),
                        DateUTC = c.DateTime(nullable: false),
                        IpAddress = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ActivityLogType", t => t.ActivityLogTypeID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ActivityLogTypeID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ActivityLogType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SystemKeyword = c.String(),
                        Name = c.String(),
                        Enabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RolePermission",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        PermissionKey = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Setting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.String(),
                        AppScope = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserPermission",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PermissionKey = c.String(),
                        Granted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserPreference",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PreferenceType = c.String(),
                        Name = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPreference", "UserId", "dbo.User");
            DropForeignKey("dbo.UserPermission", "UserId", "dbo.User");
            DropForeignKey("dbo.RolePermission", "RoleId", "dbo.Role");
            DropForeignKey("dbo.ActivityLog", "UserId", "dbo.User");
            DropForeignKey("dbo.ActivityLog", "ActivityLogTypeID", "dbo.ActivityLogType");
            DropIndex("dbo.UserPreference", new[] { "UserId" });
            DropIndex("dbo.UserPermission", new[] { "UserId" });
            DropIndex("dbo.RolePermission", new[] { "RoleId" });
            DropIndex("dbo.ActivityLog", new[] { "UserId" });
            DropIndex("dbo.ActivityLog", new[] { "ActivityLogTypeID" });
            DropTable("dbo.UserPreference");
            DropTable("dbo.UserPermission");
            DropTable("dbo.Setting");
            DropTable("dbo.RolePermission");
            DropTable("dbo.ActivityLogType");
            DropTable("dbo.ActivityLog");
        }
    }
}
