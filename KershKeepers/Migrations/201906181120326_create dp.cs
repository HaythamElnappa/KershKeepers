namespace KershKeepers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        AreaId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AreaId)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.CityId);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        ProviderId = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 300),
                        Email = c.String(nullable: false, maxLength: 300),
                        Password = c.String(nullable: false, maxLength: 300),
                        Phone = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Image = c.String(maxLength: 300),
                        Type = c.String(nullable: false, maxLength: 50),
                        WorkStartTime = c.DateTime(nullable: false),
                        WorkEndTime = c.DateTime(nullable: false),
                        RegisterDate = c.DateTime(nullable: false, storeType: "date"),
                        IsActivated = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        AreaId = c.Int(),
                    })
                .PrimaryKey(t => t.ProviderId)
                .ForeignKey("dbo.Areas", t => t.AreaId)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        MealId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        Image = c.String(maxLength: 300),
                        Description = c.String(),
                        ExecutionTime = c.Decimal(precision: 18, scale: 2),
                        Available = c.Boolean(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        ProviderId = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.MealId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Providers", t => t.ProviderId)
                .Index(t => t.CategoryId)
                .Index(t => t.ProviderId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Image = c.String(maxLength: 300),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ProviderId = c.String(maxLength: 50),
                        MessageBody = c.String(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        IsOpened = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Providers", t => t.ProviderId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ProviderId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        RegisterDate = c.DateTime(nullable: false, storeType: "date"),
                        IsDeleted = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.String(nullable: false, maxLength: 50),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ProviderId = c.String(nullable: false, maxLength: 50),
                        Date = c.DateTime(nullable: false),
                        Status = c.String(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, storeType: "money"),
                        Feedback = c.String(),
                        Type = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProviderId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        MealId = c.Int(nullable: false),
                        ProviderId = c.String(maxLength: 50),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.MealId })
                .ForeignKey("dbo.Meals", t => t.MealId, cascadeDelete: true)
                .ForeignKey("dbo.Providers", t => t.ProviderId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MealId)
                .Index(t => t.ProviderId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderId = c.String(nullable: false, maxLength: 50),
                        MealId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Rate = c.Int(),
                    })
                .PrimaryKey(t => new { t.OrderId, t.MealId })
                .ForeignKey("dbo.Meals", t => t.MealId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.MealId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "MealId", "dbo.Meals");
            DropForeignKey("dbo.Carts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Carts", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Carts", "MealId", "dbo.Meals");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Messages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Meals", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Meals", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Providers", "AreaId", "dbo.Areas");
            DropForeignKey("dbo.Areas", "CityId", "dbo.Cities");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.OrderDetails", new[] { "MealId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Carts", new[] { "ProviderId" });
            DropIndex("dbo.Carts", new[] { "MealId" });
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "ProviderId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Messages", new[] { "ProviderId" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropIndex("dbo.Meals", new[] { "ProviderId" });
            DropIndex("dbo.Meals", new[] { "CategoryId" });
            DropIndex("dbo.Providers", new[] { "AreaId" });
            DropIndex("dbo.Areas", new[] { "CityId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Carts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Orders");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Messages");
            DropTable("dbo.Categories");
            DropTable("dbo.Meals");
            DropTable("dbo.Providers");
            DropTable("dbo.Cities");
            DropTable("dbo.Areas");
        }
    }
}
