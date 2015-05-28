namespace Nemesis.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkActivities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        EstimatedDuration = c.Double(nullable: false),
                        ActualDuration = c.Double(nullable: false),
                        OvertimeDuration = c.Double(nullable: false),
                        IsArchived = c.Boolean(nullable: false),
                        DoneBy_Id = c.Int(),
                        RealizedForObjective_Id = c.Int(),
                        WorkOrder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamMembers", t => t.DoneBy_Id)
                .ForeignKey("dbo.Objectives", t => t.RealizedForObjective_Id)
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrder_Id)
                .Index(t => t.DoneBy_Id)
                .Index(t => t.RealizedForObjective_Id)
                .Index(t => t.WorkOrder_Id);
            
            CreateTable(
                "dbo.TeamMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        TeamId = c.Int(),
                        IsArchived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ParentId = c.Int(),
                        Type = c.Int(nullable: false),
                        IsArchived = c.Boolean(nullable: false),
                        Leader_Id = c.Int(),
                        QualityManager_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamMembers", t => t.Leader_Id)
                .ForeignKey("dbo.Teams", t => t.ParentId)
                .ForeignKey("dbo.Users", t => t.QualityManager_Id)
                .Index(t => t.ParentId)
                .Index(t => t.Leader_Id)
                .Index(t => t.QualityManager_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.Binary(),
                        Salt = c.Binary(),
                        IsArchived = c.Boolean(nullable: false),
                        TeamMember_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamMembers", t => t.TeamMember_Id)
                .Index(t => t.TeamMember_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        IsArchived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Objectives",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShortDescription = c.String(nullable: false),
                        Description = c.String(),
                        Year = c.Int(nullable: false),
                        ObjectiveNumber = c.Int(nullable: false),
                        CompletionMeasure = c.String(),
                        QualityMeasure = c.String(),
                        Priority = c.Int(nullable: false),
                        PercentComplete = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        EstimatedTime = c.Double(nullable: false),
                        ActualTime = c.Double(nullable: false),
                        IsArchived = c.Boolean(nullable: false),
                        MonthOrdNum = c.Int(),
                        QuartalOrdNum = c.Int(),
                        WeekOrdNum = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        AssignedToTeam_Id = c.Int(),
                        CreatedBy_Id = c.Int(),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.AssignedToTeam_Id)
                .ForeignKey("dbo.TeamMembers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Objectives", t => t.Parent_Id)
                .Index(t => t.AssignedToTeam_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.WorkOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EstimatedEndDate = c.DateTime(nullable: false),
                        Document = c.String(),
                        IsArchived = c.Boolean(nullable: false),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IsArchived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AssetAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        IsArchived = c.Boolean(nullable: false),
                        AssetType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssetTypes", t => t.AssetType_Id)
                .Index(t => t.AssetType_Id);
            
            CreateTable(
                "dbo.AssetAttributeEnumItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsArchived = c.Boolean(nullable: false),
                        Attribute_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssetAttributes", t => t.Attribute_Id)
                .Index(t => t.Attribute_Id);
            
            CreateTable(
                "dbo.Assets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        PartNumber = c.String(),
                        IsArchived = c.Boolean(nullable: false),
                        Parent_Id = c.Int(),
                        Team_Id = c.Int(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assets", t => t.Parent_Id)
                .ForeignKey("dbo.Teams", t => t.Team_Id)
                .ForeignKey("dbo.AssetTypes", t => t.Type_Id)
                .Index(t => t.Parent_Id)
                .Index(t => t.Team_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.AssetTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsArchived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ObjectiveTeamMember",
                c => new
                    {
                        Objective_Id = c.Int(nullable: false),
                        TeamMember_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Objective_Id, t.TeamMember_Id })
                .ForeignKey("dbo.Objectives", t => t.Objective_Id, cascadeDelete: true)
                .ForeignKey("dbo.TeamMembers", t => t.TeamMember_Id, cascadeDelete: true)
                .Index(t => t.Objective_Id)
                .Index(t => t.TeamMember_Id);
            
            CreateTable(
                "dbo.WorkOrderTeam",
                c => new
                    {
                        WorkOrder_Id = c.Int(nullable: false),
                        Team_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkOrder_Id, t.Team_Id })
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrder_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .Index(t => t.WorkOrder_Id)
                .Index(t => t.Team_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assets", "Type_Id", "dbo.AssetTypes");
            DropForeignKey("dbo.AssetAttributes", "AssetType_Id", "dbo.AssetTypes");
            DropForeignKey("dbo.Assets", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Assets", "Parent_Id", "dbo.Assets");
            DropForeignKey("dbo.AssetAttributeEnumItems", "Attribute_Id", "dbo.AssetAttributes");
            DropForeignKey("dbo.WorkActivities", "WorkOrder_Id", "dbo.WorkOrders");
            DropForeignKey("dbo.WorkOrders", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.WorkOrderTeam", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.WorkOrderTeam", "WorkOrder_Id", "dbo.WorkOrders");
            DropForeignKey("dbo.WorkActivities", "RealizedForObjective_Id", "dbo.Objectives");
            DropForeignKey("dbo.Objectives", "Parent_Id", "dbo.Objectives");
            DropForeignKey("dbo.Objectives", "CreatedBy_Id", "dbo.TeamMembers");
            DropForeignKey("dbo.ObjectiveTeamMember", "TeamMember_Id", "dbo.TeamMembers");
            DropForeignKey("dbo.ObjectiveTeamMember", "Objective_Id", "dbo.Objectives");
            DropForeignKey("dbo.Objectives", "AssignedToTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.WorkActivities", "DoneBy_Id", "dbo.TeamMembers");
            DropForeignKey("dbo.Teams", "QualityManager_Id", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Users", "TeamMember_Id", "dbo.TeamMembers");
            DropForeignKey("dbo.Teams", "ParentId", "dbo.Teams");
            DropForeignKey("dbo.TeamMembers", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Teams", "Leader_Id", "dbo.TeamMembers");
            DropIndex("dbo.WorkOrderTeam", new[] { "Team_Id" });
            DropIndex("dbo.WorkOrderTeam", new[] { "WorkOrder_Id" });
            DropIndex("dbo.ObjectiveTeamMember", new[] { "TeamMember_Id" });
            DropIndex("dbo.ObjectiveTeamMember", new[] { "Objective_Id" });
            DropIndex("dbo.RoleUsers", new[] { "User_Id" });
            DropIndex("dbo.RoleUsers", new[] { "Role_Id" });
            DropIndex("dbo.Assets", new[] { "Type_Id" });
            DropIndex("dbo.Assets", new[] { "Team_Id" });
            DropIndex("dbo.Assets", new[] { "Parent_Id" });
            DropIndex("dbo.AssetAttributeEnumItems", new[] { "Attribute_Id" });
            DropIndex("dbo.AssetAttributes", new[] { "AssetType_Id" });
            DropIndex("dbo.WorkOrders", new[] { "Client_Id" });
            DropIndex("dbo.Objectives", new[] { "Parent_Id" });
            DropIndex("dbo.Objectives", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Objectives", new[] { "AssignedToTeam_Id" });
            DropIndex("dbo.Users", new[] { "TeamMember_Id" });
            DropIndex("dbo.Teams", new[] { "QualityManager_Id" });
            DropIndex("dbo.Teams", new[] { "Leader_Id" });
            DropIndex("dbo.Teams", new[] { "ParentId" });
            DropIndex("dbo.TeamMembers", new[] { "TeamId" });
            DropIndex("dbo.WorkActivities", new[] { "WorkOrder_Id" });
            DropIndex("dbo.WorkActivities", new[] { "RealizedForObjective_Id" });
            DropIndex("dbo.WorkActivities", new[] { "DoneBy_Id" });
            DropTable("dbo.WorkOrderTeam");
            DropTable("dbo.ObjectiveTeamMember");
            DropTable("dbo.RoleUsers");
            DropTable("dbo.AssetTypes");
            DropTable("dbo.Assets");
            DropTable("dbo.AssetAttributeEnumItems");
            DropTable("dbo.AssetAttributes");
            DropTable("dbo.Clients");
            DropTable("dbo.WorkOrders");
            DropTable("dbo.Objectives");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Teams");
            DropTable("dbo.TeamMembers");
            DropTable("dbo.WorkActivities");
        }
    }
}
